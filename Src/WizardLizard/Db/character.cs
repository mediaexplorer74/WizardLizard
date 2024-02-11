// GameManager.Db.character


#nullable disable
namespace GameManager.Db
{
  internal class character : TableRow
  {
    public string name { get; set; }

    public int health { get; set; }

    public int Level { get; set; }

    public int PetID { get; set; }

    public int spellID { get; set; }
  }
}
