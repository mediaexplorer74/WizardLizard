// GameManager.Db.Connection

using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Linq;
using System.Reflection;
using System.Text;

#nullable disable
namespace GameManager.Db
{
  public class Connection : IDisposable
  {
    private SqliteConnection con = new SqliteConnection("Data source = data.db");

    public DataTable GetData(SqliteCommand mySqLiteCommand)
    {
      DataSet dataSet = new DataSet();
      SqliteConnection sqLiteConnection = new SqliteConnection("Data source=data.db");
      SqliteDataAdapter sqLiteDataAdapter = new SqliteDataAdapter();
      mySqLiteCommand.Connection = sqLiteConnection;
      //RnD
      //sqLiteDataAdapter.SelectCommand = mySqLiteCommand;
      
     sqLiteConnection.Open();
     //RnD
     // sqLiteDataAdapter.Fill(dataSet);
      sqLiteConnection.Close();
      return dataSet.Tables[0];
    }

    public void OpenCon() => this.con.Open();

    public void ModifyData(SqliteCommand myLiteCommand)
    {
      SqliteConnection sqLiteConnection = new SqliteConnection("Data source=data.db");
      myLiteCommand.Connection = sqLiteConnection;
      sqLiteConnection.Open();
      myLiteCommand.ExecuteNonQuery();
      sqLiteConnection.Close();
    }

    public List<T> GetAllRows<T>() where T : TableRow, new()
    {
      SqliteCommand sqLiteCommand = new SqliteCommand("SELECT * FROM " + typeof (T).Name, this.con);
      List<T> allRows = new List<T>();
      using (SqliteDataReader reader = sqLiteCommand.ExecuteReader())
      {
        while (reader.Read())
        {
          T target = new T();
          this.FillPropertiesFromRow<T>(target, reader);
          allRows.Add(target);
        }
      }
      return allRows;
    }

    public T GetRow<T>(int ID) where T : TableRow, new()
    {
      using (SqliteCommand sqLiteCommand = new SqliteCommand(
          "SELECT * FROM " + typeof (T).Name + " WHERE ID = " + (object) ID, this.con))
      {
        using (SqliteDataReader reader = sqLiteCommand.ExecuteReader())
        {
          if (!reader.Read())
            return default (T);
          T target = new T();
          this.FillPropertiesFromRow<T>(target, reader);
          return target;
        }
      }
    }

    public List<T> FindRowsWhere<T>(string column, object value) where T : TableRow, new()
    {
      using (SqliteCommand sqLiteCommand = new SqliteCommand(
          "SELECT * FROM " + typeof (T).Name + " WHERE " + column + " = @Val;", this.con))
      {
        sqLiteCommand.Parameters.AddWithValue("@Val", value);
        List<T> rowsWhere = new List<T>();
        using (SqliteDataReader reader = sqLiteCommand.ExecuteReader())
        {
          while (reader.Read())
          {
            T target = new T();
            this.FillPropertiesFromRow<T>(target, reader);
            rowsWhere.Add(target);
          }
        }
        return rowsWhere;
      }
    }

    public int InsertRow<T>(T obj) where T : TableRow, new()
    {
      IEnumerable<PropertyInfo> source = 
                ((IEnumerable<PropertyInfo>) typeof (T).GetProperties())
                .Where<PropertyInfo>((System.Func<PropertyInfo, bool>) (a => a.Name != "ID"));
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("INSERT INTO ").Append(typeof (T).Name);
      if (source.Count<PropertyInfo>() > 0)
      {
        stringBuilder.Append(" (").Append(string.Join(",", 
            source.Select<PropertyInfo, string>(
                (System.Func<PropertyInfo, string>) (a => a.Name)))).Append(")");
        stringBuilder.Append(" VALUES (").Append(string.Join(",", 
            source.Select<PropertyInfo, string>(
                (System.Func<PropertyInfo, string>) (a => "@" + a.Name)))).Append(");");
      }
      else
        stringBuilder.Append(" DEFAULT VALUES;");
      using (SqliteCommand sqLiteCommand = new SqliteCommand(stringBuilder.ToString(), this.con))
      {
        foreach (PropertyInfo propertyInfo in source)
        {
          object obj1 = propertyInfo.GetValue((object) obj, (object[]) null) ?? (object) DBNull.Value;
          sqLiteCommand.Parameters.AddWithValue("@" + propertyInfo.Name, obj1);
        }
        sqLiteCommand.ExecuteNonQuery();
      }
      using (SqliteCommand sqLiteCommand = new SqliteCommand("SELECT last_insert_rowid()", this.con))
        return Convert.ToInt32(sqLiteCommand.ExecuteScalar());
    }

    public void UpdateRow<T>(T obj) where T : TableRow, new()
    {
      PropertyInfo[] properties = typeof (T).GetProperties();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("UPDATE ").Append(typeof (T).Name).Append(" SET ");
      IEnumerable<PropertyInfo> source = ((IEnumerable<PropertyInfo>) properties)
                .Where<PropertyInfo>((System.Func<PropertyInfo, bool>) (a => a.Name != "ID"));
      stringBuilder.Append(string.Join(",", source.Select<PropertyInfo, string>(
          (System.Func<PropertyInfo, string>) (a => a.Name + "=@" + a.Name))));
      stringBuilder.Append(" WHERE ID = ").Append(obj.ID).Append(";");
      using (SqliteCommand sqLiteCommand = new SqliteCommand(stringBuilder.ToString(), this.con))
      {
        foreach (PropertyInfo propertyInfo in source)
          sqLiteCommand.Parameters.AddWithValue("@" + propertyInfo.Name, 
              propertyInfo.GetValue((object) obj, (object[]) null));
        sqLiteCommand.ExecuteNonQuery();
      }
    }

    public void InsertOrUpdateRow<T>(T obj) where T : TableRow, new()
    {
      if ((object) this.GetRow<T>(obj.ID) != null)
        this.UpdateRow<T>(obj);
      else
        this.InsertRow<T>(obj);
    }

    public void DeleteRow<T>(int id) where T : TableRow, new()
    {
      using (SqliteCommand sqLiteCommand = new SqliteCommand(
          "DELETE FROM " + typeof (T).Name + " WHERE ID = " + (object) id, this.con))
        sqLiteCommand.ExecuteNonQuery();
    }

    public void DeleteAllRows<T>() where T : TableRow, new()
    {
      using (SqliteCommand sqLiteCommand = new SqliteCommand(
          "DELETE FROM " + typeof (T).Name, this.con))
        sqLiteCommand.ExecuteNonQuery();
    }

    private void FillPropertiesFromRow<T>(T target, SqliteDataReader reader)
    {
      foreach (PropertyInfo property in typeof (T).GetProperties())
      {
        object obj1 = reader[property.Name];
        if (obj1 != DBNull.Value)
        {
          object obj2 = Convert.ChangeType(obj1, property.PropertyType);
          property.SetValue((object) target, obj2, (object[]) null);
        }
      }
    }

    public void Dispose() => this.con.Dispose();
  }
}
