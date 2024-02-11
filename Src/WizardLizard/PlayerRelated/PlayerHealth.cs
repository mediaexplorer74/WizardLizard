// GameManager.PlayerHealth

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

#nullable disable
namespace GameManager
{
  public class PlayerHealth : Component, IUpdateable, ILoadable, IAnimateable
  {
    private Animator animator;

    public PlayerHealth(GameObject gameObject)
      : base(gameObject)
    {
      this.animator = (Animator) this.GameObject.GetComponent("Animator");
    }

    public void LoadContent(ContentManager content) => this.CreateAnimations();

    public void OnAnimationDone(string animationName)
    {
    }

    public void Update()
    {
      if (Player.Health == 6)
        this.animator.PlayAnimation("Healthbar6");
      if (Player.Health == 5)
        this.animator.PlayAnimation("Healthbar5");
      if (Player.Health == 4)
        this.animator.PlayAnimation("Healthbar4");
      if (Player.Health == 3)
        this.animator.PlayAnimation("Healthbar3");
      if (Player.Health == 2)
        this.animator.PlayAnimation("Healthbar2");
      if (Player.Health == 1)
        this.animator.PlayAnimation("Healthbar1");
      if (Player.Health != 0)
        return;
      this.animator.PlayAnimation("Healthbar0");
    }

    public void CreateAnimations()
    {
      this.animator.CreateAnimation("Healthbar6", new Animation(1, 0, 0, 190, 30, 6f, Vector2.Zero));
      this.animator.CreateAnimation("Healthbar5", new Animation(1, 31, 0, 190, 30, 6f, Vector2.Zero));
      this.animator.CreateAnimation("Healthbar4", new Animation(1, 62, 0, 190, 30, 6f, Vector2.Zero));
      this.animator.CreateAnimation("Healthbar3", new Animation(1, 93, 0, 190, 30, 6f, Vector2.Zero));
      this.animator.CreateAnimation("Healthbar2", new Animation(1, 124, 0, 190, 30, 6f, Vector2.Zero));
      this.animator.CreateAnimation("Healthbar1", new Animation(1, 155, 0, 190, 30, 6f, Vector2.Zero));
      this.animator.CreateAnimation("Healthbar0", new Animation(1, 186, 0, 190, 30, 6f, Vector2.Zero));
      this.animator.PlayAnimation("Healthbar6");
    }
  }
}
