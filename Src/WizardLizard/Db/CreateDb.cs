// GameManager.Db.CreateDb

using Microsoft.Data.Sqlite;//System.Data.SQLite;
using System.IO;

#nullable disable
namespace GameManager.Db
{
  public class CreateDb
  {
    public void CreateDatabase()
    {
      if (File.Exists("data.db"))
        return;

      //RnD
      //SqliteConnection.CreateFile("data.db");

      SqliteConnection connection = new SqliteConnection("Data source = data.db");
      connection.Open();
      using (SqliteDataReader sqLiteDataReader = 
                new SqliteCommand(
                    "SELECT * FROM sqlite_master WHERE type='table'", 
                    connection).ExecuteReader())
      {
        if (sqLiteDataReader.Read())
          return;
        new SqliteCommand(
            "\r\n                CREATE TABLE spell(ID primary key, primaryspell integer not null, secondaryspell integer not null, shield integer not null);\r\n                CREATE TABLE pet(ID primary key, health integer not null);\r\n                CREATE TABLE character (ID integer primary key,spellID integer not null, level integer not null, petID integer not null,  name string not null, health integer not null, FOREIGN key(spellID) REFERENCES spell(ID), FOREIGN key(PetID) REFERENCES pet(ID));", connection).ExecuteNonQuery();
      }
    }
  }
}
