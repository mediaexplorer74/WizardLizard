// GameManager.MiniGoblin

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

#nullable disable
namespace GameManager
{
  public class MiniGoblin : 
    Component,
    ILoadable,
    IUpdateable,
    IAnimateable,
    ICollisionEnter,
    ICollisionExit
  {
    private Transform transform;
    private Animator animator;
    private Vector2 miniGoblinPos;
    private Vector2 companionPos;
    private float speed;
    private Vector2 distToCompanion;

    public MiniGoblin(GameObject gameObject)
      : base(gameObject)
    {
      this.animator = (Animator) this.GameObject.GetComponent("Animator");
      this.transform = this.GameObject.Transform;
      this.speed = 200f;
    }

    public void Update()
    {
      Vector2 vector2 = Vector2.Zero;
      this.miniGoblinPos = new Vector2(this.transform.Position.X, this.transform.Position.Y);
      foreach (GameObject gameObject in Game1.GameObjects)
      {
        if (gameObject.GetComponent("Companion") != null)
          this.companionPos = gameObject.Transform.Position;
      }
      this.distToCompanion = this.miniGoblinPos - this.companionPos;
      if (Companion.Roar && (double) this.distToCompanion.Length() < 300.0)
      {
        if ((double) this.companionPos.X > (double) this.transform.Position.X)
          vector2 = new Vector2(-1f, 0.0f);
        else if ((double) this.companionPos.X < (double) this.transform.Position.X)
          vector2 = new Vector2(1f, 0.0f);
      }
      this.transform.Translate(vector2 * Game1.DeltaTime * this.speed);
    }

    public void LoadContent(ContentManager content)
    {
    }

    public void OnAnimationDone(string animationName)
    {
    }

    public void OnCollisionEnter(Collider other)
    {
    }

    public void OnCollisionExit(Collider other)
    {
    }
  }
}
