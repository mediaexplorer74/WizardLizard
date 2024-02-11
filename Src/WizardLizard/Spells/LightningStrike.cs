// GameManager.LightningStrike

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

#nullable disable
namespace GameManager
{
  public class LightningStrike : 
    Component,
    IUpdateable,
    ILoadable,
    IAnimateable,
    ICollisionEnter,
    ICollisionExit
  {
    private SoundEffect lightningSound;
    private Transform transform;
    private int speed = 2000;
    private Animator animator;

    public LightningStrike(GameObject gameObject)
      : base(gameObject)
    {
      this.animator = (Animator) this.GameObject.GetComponent("Animator");
      this.transform = gameObject.Transform;
    }

    public void LoadContent(ContentManager content)
    {
      this.lightningSound = content.Load<SoundEffect>("LightningStrikeShoot");
      this.lightningSound.Play();
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
        component.TakeDamage(6);
      }
      if (other.GameObject.GetComponent("Orc") != null)
      {
        Orc component = (Orc) other.GameObject.GetComponent("Orc");
        Game1.ObjectsToRemove.Add(this.GameObject);
        component.TakeDamage(6);
      }
      if (other.GameObject.GetComponent("Archer") != null)
      {
        Archer component = (Archer) other.GameObject.GetComponent("Archer");
        Game1.ObjectsToRemove.Add(this.GameObject);
        component.TakeDamage(6);
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
      this.transform.Translate(new Vector2(0.0f, 1f) * Game1.DeltaTime * (float) this.speed);
    }
  }
}
