// GameManager.AttackField

using Microsoft.Xna.Framework.Content;

#nullable disable
namespace GameManager
{
  public class AttackField : Component, ILoadable, IUpdateable, ICollisionEnter, ICollisionExit
  {
    private Transform transform;
    private string attacker;
    private float timer;
    private int deadline;
    private bool hit;

    public string Attacker
    {
      get => this.attacker;
      set => this.attacker = value;
    }

    public AttackField(GameObject gameObject, string attacker)
      : base(gameObject)
    {
      this.Attacker = attacker;
      this.transform = gameObject.Transform;
    }

    public void LoadContent(ContentManager content)
    {
      this.timer = 0.0f;
      if (this.Attacker == "Orc")
        this.deadline = 0;
      if (this.Attacker == "Goblin")
        this.deadline = 0;
      if (!(this.Attacker == "Player"))
        return;
      this.deadline = 0;
    }

    public void OnCollisionEnter(Collider other)
    {
      if (this.hit)
        return;
      if (this.Attacker == "Player")
      {
        if (other.GameObject.GetComponent("Orc") != null)
        {
          ((Orc) other.GameObject.GetComponent("Orc")).TakeDamage(1);
          Game1.ObjectsToRemove.Add(this.GameObject);
          this.hit = true;
        }
        if (other.GameObject.GetComponent("Goblin") != null)
        {
          ((Goblin) other.GameObject.GetComponent("Goblin")).TakeDamage(1);
          Game1.ObjectsToRemove.Add(this.GameObject);
          this.hit = true;
        }
        if (other.GameObject.GetComponent("Archer") != null)
        {
          ((Archer) other.GameObject.GetComponent("Archer")).TakeDamage(1);
          Game1.ObjectsToRemove.Add(this.GameObject);
          this.hit = true;
        }
      }
      if (!(this.Attacker == "Orc") && !(this.Attacker == "Goblin") || other.GameObject.GetComponent("Player") == null)
        return;
      Player component = (Player) other.GameObject.GetComponent("Player");
      if (this.Attacker == "Orc")
      {
        component.PlayerHit(4);
        Game1.ObjectsToRemove.Add(this.GameObject);
        this.hit = true;
      }
      else
      {
        if (!(this.Attacker == "Goblin"))
          return;
        component.PlayerHit(1);
        Game1.ObjectsToRemove.Add(this.GameObject);
        this.hit = true;
      }
    }

    public void OnCollisionExit(Collider other)
    {
    }

    public void Update()
    {
      this.timer += Game1.DeltaTime;
      if ((double) this.deadline > (double) this.timer)
        return;
      Game1.ObjectsToRemove.Add(this.GameObject);
    }
  }
}
