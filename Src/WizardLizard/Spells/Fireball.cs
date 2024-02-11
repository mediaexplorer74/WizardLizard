// GameManager.Fireball

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

#nullable disable
namespace GameManager
{
  public class Fireball : 
    Component,
    IUpdateable,
    ILoadable,
    IAnimateable,
    ICollisionEnter,
    ICollisionExit
  {
    private SoundEffect fireballSound;
    private Transform transform;
    private int dmg = 3;
    private Animator animator;
    private int speed = 400;
    private Vector2 mousePosition;
    private MouseState mouseState = Mouse.GetState();
    private Vector2 fireballPos;

    public Fireball(GameObject gameObject)
      : base(gameObject)
    {
      this.animator = (Animator) this.GameObject.GetComponent("Animator");
      this.transform = gameObject.Transform;
    }

    public void LoadContent(ContentManager content)
    {
      this.fireballSound = content.Load<SoundEffect>("FireballShoot");
      this.fireballSound.Play();
      this.CreateAnimations();
      foreach (GameObject gameObject in Game1.GameObjects)
      {
        if (gameObject.GetComponent("Aimer") != null)
          this.mousePosition = gameObject.Transform.Position;
      }
      this.fireballPos = new Vector2(this.transform.Position.X, this.transform.Position.Y);
    }

    public void OnAnimationDone(string animationName)
    {
    }

    public void OnCollisionEnter(Collider other)
    {
      if (other.GameObject.GetComponent("Goblin") != null)
      {
        Goblin component = (Goblin) other.GameObject.GetComponent("Goblin");
        Game1.ObjectsToRemove.Add(this.GameObject);
        int dmg = this.dmg;
        component.TakeDamage(dmg);
      }
      if (other.GameObject.GetComponent("Orc") != null)
      {
        Orc component = (Orc) other.GameObject.GetComponent("Orc");
        Game1.ObjectsToRemove.Add(this.GameObject);
        int dmg = this.dmg;
        component.TakeDamage(dmg);
      }
      if (other.GameObject.GetComponent("Archer") != null)
      {
        Archer component = (Archer) other.GameObject.GetComponent("Archer");
        Game1.ObjectsToRemove.Add(this.GameObject);
        int dmg = this.dmg;
        component.TakeDamage(dmg);
      }
      if (other.GameObject.GetComponent("SolidPlatform") == null && other.GameObject.GetComponent("Door") == null && other.GameObject.GetComponent("Lever") == null && other.GameObject.GetComponent("MoveableBox") == null && other.GameObject.GetComponent("NonSolidPlatform") == null)
        return;
      Game1.ObjectsToRemove.Add(this.GameObject);
    }

    public void OnCollisionExit(Collider other)
    {
    }

    public void Update()
    {
      this.animator.PlayAnimation(nameof (Fireball));
      Vector2 zero = Vector2.Zero;
      Vector2 vector2 = this.mousePosition - this.fireballPos;
      vector2.Normalize();
      this.transform.Translate(vector2 * Game1.DeltaTime * (float) this.speed);
      if ((double) this.transform.Position.X <= 1600.0 && (double) this.transform.Position.X >= 0.0 && (double) this.transform.Position.Y <= 900.0 && (double) this.transform.Position.Y >= 0.0)
        return;
      Game1.ObjectsToRemove.Add(this.GameObject);
    }

    public void CreateAnimations()
    {
      this.animator.CreateAnimation(nameof (Fireball), new Animation(4, 0, 0, 50, 50, 32f, Vector2.Zero));
      this.animator.PlayAnimation(nameof (Fireball));
    }
  }
}
