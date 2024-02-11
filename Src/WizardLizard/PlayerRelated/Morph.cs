// GameManager.Morph

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

#nullable disable
namespace GameManager
{
  public class Morph : 
    Component,
    ILoadable,
    IUpdateable,
    IAnimateable,
    ICollisionEnter,
    ICollisionExit
  {
    private Transform transform;
    private Animator animator;
    private Vector2 velocity;
    private Director director;
    private bool hasJumped;
    private static bool hasMorphed = false;
    private static bool derp = true;
    private int speed = 1;

    public static bool HasMorphed
    {
      get => Morph.hasMorphed;
      set => Morph.hasMorphed = value;
    }

    public static bool Derp
    {
      get => Morph.derp;
      set => Morph.derp = value;
    }

    public Morph(GameObject gameObject)
      : base(gameObject)
    {
      this.animator = (Animator) this.GameObject.GetComponent("Animator");
      this.transform = gameObject.Transform;
      Morph.HasMorphed = true;
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

    public void Update()
    {
      KeyboardState state = Keyboard.GetState();
      Vector2 zero = Vector2.Zero;
      this.speed = 200;
      Vector2 vector2 = zero + this.velocity;
      if ((double) this.transform.Position.Y >= 200.0)
        this.hasJumped = false;
      if (state.IsKeyDown(Keys.W) && !this.hasJumped)
      {
        vector2.Y -= 5f;
        this.velocity.Y = -5f;
        this.hasJumped = true;
      }
      if (this.hasJumped)
        this.velocity.Y += 0.05f * 5f;
      if (!this.hasJumped)
        this.velocity.Y = 0.0f;
      if (state.IsKeyDown(Keys.D))
        vector2 += new Vector2(1f, 0.0f);
      if (state.IsKeyDown(Keys.A))
        vector2 += new Vector2(-1f, 0.0f);
      if (state.IsKeyDown(Keys.S))
        vector2 += new Vector2(0.0f, 1f);
      if (state.IsKeyUp(Keys.F))
        Morph.HasMorphed = false;
      if (state.IsKeyDown(Keys.F) && !Morph.HasMorphed)
      {
        Morph.HasMorphed = true;
        this.director = new Director((IBuilder) new PlayerBuilder());
        Game1.Instance.AddGameObject(this.director.Construct(this.transform.Position));
        this.director = new Director((IBuilder) new CompanionBuilder());
        Game1.Instance.AddGameObject(this.director.Construct(this.transform.Position));
        Game1.Instance.RemoveGameObject(this.GameObject);
      }
      this.transform.Translate(vector2 * Game1.DeltaTime * (float) this.speed);
      Game1.PlayerPos = this.transform.Position;
    }
  }
}
