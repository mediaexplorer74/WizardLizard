// GameManager.Arrow

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

#nullable disable
namespace GameManager
{
  public class Arrow : 
    Component,
    IUpdateable,
    ILoadable,
    IAnimateable,
    ICollisionEnter,
    ICollisionExit
  {
    private float speed = 1216f;
    private Transform transform;
    private Animator animator;
    private Vector2 arrowPos;
    private Vector2 translation;

    public Arrow(GameObject gameObject)
      : base(gameObject)
    {
      this.animator = (Animator) this.GameObject.GetComponent("Animator");
      this.transform = gameObject.Transform;
    }

    public void Update()
    {
      this.translation.Normalize();
      this.transform.Translate(this.translation * Game1.DeltaTime * this.speed);
      if ((double) this.transform.Position.X <= 1600.0 && (double) this.transform.Position.X >= 0.0 && (double) this.transform.Position.Y <= 900.0 && (double) this.transform.Position.Y >= 0.0)
        return;
      Game1.ObjectsToRemove.Add(this.GameObject);
    }

    public void LoadContent(ContentManager content)
    {
      this.arrowPos = new Vector2(this.transform.Position.X, this.transform.Position.Y);
      this.translation = Game1.PlayerPos - this.arrowPos;
    }

    public void OnAnimationDone(string animationName)
    {
    }

    public void OnCollisionEnter(Collider other)
    {
      if (other.GameObject.GetComponent("Player") != null)
      {
        Player component = (Player) other.GameObject.GetComponent("Player");
        Game1.ObjectsToRemove.Add(this.GameObject);
        component.PlayerHit(1);
      }
      if (other.GameObject.GetComponent("PlayerShield") != null)
        Game1.ObjectsToRemove.Add(this.GameObject);
      if (other.GameObject.GetComponent("SolidPlatform") == null)
        return;
      Game1.ObjectsToRemove.Add(this.GameObject);
    }

    public void OnCollisionExit(Collider other)
    {
    }
  }
}
