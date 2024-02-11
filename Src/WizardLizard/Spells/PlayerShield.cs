// GameManager.PlayerShield

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

#nullable disable
namespace GameManager
{
  public class PlayerShield : 
    Component,
    IUpdateable,
    ILoadable,
    IAnimateable,
    ICollisionEnter,
    ICollisionExit
  {
    private Animator animator;
    private Transform transform;
    private const float visible = 10f;
    private float countdown = 10f;

    public PlayerShield(GameObject gameObject)
      : base(gameObject)
    {
      this.animator = (Animator) this.GameObject.GetComponent("Animator");
      this.transform = gameObject.Transform;
    }

    public void Update()
    {
      this.animator.PlayAnimation("ShieldAnimation");
      Vector2 zero = Vector2.Zero;
      this.transform.Position = new Vector2(Game1.PlayerPos.X - 36f, Game1.PlayerPos.Y - 50f);
      foreach (GameObject gameObject in Game1.GameObjects)
      {
        if (gameObject.GetComponent("Morph") != null)
          Game1.Instance.RemoveGameObject(this.GameObject);
      }
      if ((double) this.countdown > 0.0)
        this.countdown -= Game1.DeltaTime;
      if ((double) this.countdown > 0.0)
        return;
      this.countdown = 0.0f;
      Game1.Instance.RemoveGameObject(this.GameObject);
      this.countdown = 10f;
    }

    public void LoadContent(ContentManager content) => this.CreateAnimations();

    public void OnAnimationDone(string animationName)
    {
    }

    public void OnCollisionEnter(Collider other)
    {
      if (other.GameObject.GetComponent("Arrow") == null)
        return;
      Game1.Instance.RemoveGameObject(this.GameObject);
    }

    public void OnCollisionExit(Collider other)
    {
    }

    public void CreateAnimations()
    {
      this.animator.CreateAnimation("ShieldAnimation", 
          new Animation(6, 0, 0, 144, 200, 6f, Vector2.Zero));

      this.animator.PlayAnimation("ShieldAnimation");
    }
  }
}
