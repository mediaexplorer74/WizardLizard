// GameManager.Companion

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;

#nullable disable
namespace GameManager
{
  public class Companion : 
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
    private bool hasJumped;
    private static bool companionControle;
    private static bool roar;
    private bool shiftControle;
    private int speed = 200;
    private bool canInteract;
    private bool haveInteracted;
    private Lever lastknownLever;
    private bool fly;

    public static bool CompanionControle
    {
      get => Companion.companionControle;
      set => Companion.companionControle = value;
    }

    public static bool Roar
    {
      get => Companion.roar;
      set => Companion.roar = value;
    }

    public Companion(GameObject gameObject)
      : base(gameObject)
    {
      this.animator = (Animator) this.GameObject.GetComponent("Animator");
      this.transform = gameObject.Transform;
      Companion.companionControle = false;
      this.shiftControle = true;
      this.canInteract = false;
      this.haveInteracted = true;
      Companion.roar = false;
    }

    public void LoadContent(ContentManager content)
    {
      this.animator.CreateAnimation("Left", new Animation(1, 0, 0, 67, 50, 1f, Vector2.Zero));
      this.animator.CreateAnimation("Right", new Animation(1, 0, 1, 67, 50, 1f, Vector2.Zero));
      this.animator.PlayAnimation("Left");
    }

    public void OnAnimationDone(string animationName) => this.animator.PlayAnimation(animationName);

    public void ControlePet(KeyboardState keyState, Vector2 translation)
    {
      this.speed = 200;
      if (keyState.IsKeyDown(Keys.W) && !this.hasJumped)
      {
        translation.Y -= 10f;
        this.velocity.Y = -10f;
        this.hasJumped = true;
      }
      this.velocity.Y += 0.15f * 5f;
      if (keyState.IsKeyDown(Keys.D))
        translation += new Vector2(1f, 0.0f);
      if (keyState.IsKeyDown(Keys.A))
        translation += new Vector2(-1f, 0.0f);
      if (keyState.IsKeyDown(Keys.S))
        translation += new Vector2(0.0f, 1f);
      if (keyState.IsKeyDown(Keys.R) && !Companion.roar)
        Companion.roar = true;
      if (keyState.IsKeyUp(Keys.R) && Companion.roar)
        Companion.roar = false;
      translation += this.velocity;
      if (keyState.IsKeyUp(Keys.Space))
        this.shiftControle = true;
      if (keyState.IsKeyDown(Keys.Space) && this.shiftControle)
      {
        Companion.companionControle = false;
        this.shiftControle = false;
      }
      if (keyState.IsKeyDown(Keys.E) && this.canInteract && this.haveInteracted)
      {
        if (this.lastknownLever != null)
          this.lastknownLever.Interaction(this.GameObject);
        this.haveInteracted = false;
        this.canInteract = false;
      }
      if (keyState.IsKeyUp(Keys.E))
        this.haveInteracted = true;
      this.transform.Translate(translation * Game1.DeltaTime * (float) this.speed);
      if ((double) translation.X > 0.0)
        this.animator.PlayAnimation("Right");
      if ((double) translation.X >= 0.0)
        return;
      this.animator.PlayAnimation("Left");
    }

    public void FollowPlayer(Vector2 translation)
    {
      this.speed = 200;
      Vector2 vector2_1 = new Vector2(Game1.PlayerPos.X + 20f, Game1.PlayerPos.Y + 50f);
      Vector2 vector2_2 = new Vector2(this.transform.Position.X, this.transform.Position.Y);
      Vector2 vector2_3 = new Vector2();
      Vector2 vector2_4 = Game1.PlayerPos - vector2_2;
      if ((double) vector2_2.X + 1.0 < (double) vector2_1.X)
        translation = new Vector2(1f, 0.0f);
      else if ((double) vector2_2.X - 1.0 > (double) vector2_1.X)
        translation = new Vector2(-1f, 0.0f);
      if (Player.HasJumped && !this.hasJumped)
      {
        translation.Y -= 5f;
        this.velocity.Y = -5f;
        this.hasJumped = true;
      }
      else if ((double) vector2_1.Y + 50.0 < (double) vector2_2.Y && !this.hasJumped)
      {
        translation.Y -= 5f;
        this.velocity.Y = -5f;
        this.hasJumped = true;
      }
      if (!this.fly)
      {
        this.velocity.Y += 0.05f * 5f;
        translation += this.velocity;
      }
      if ((double) vector2_4.Length() > 50.0)
      {
        this.fly = true;
        vector2_4.Normalize();
        translation += vector2_4;
      }
      else if ((double) vector2_4.Length() > 20.0)
        this.fly = false;
      this.transform.Translate(translation * Game1.DeltaTime * (float) this.speed);
      if ((double) translation.X > 0.0)
        this.animator.PlayAnimation("Right");
      if ((double) translation.X >= 0.0)
        return;
      this.animator.PlayAnimation("Left");
    }

    public void Update()
    {
      KeyboardState state = Keyboard.GetState();
      Vector2 zero = Vector2.Zero;
      if (Companion.companionControle)
        this.ControlePet(state, zero);
      else
        this.FollowPlayer(zero);
    }

    public void OnCollisionEnter(Collider other)
    {
      if (other.GameObject.GetComponent("Lever") != null)
        this.canInteract = true;
      if (!this.fly && other.GameObject.GetComponent("SolidPlatform") != null)
      {
        Collider component = (Collider) this.GameObject.GetComponent("Collider");
        int y = Math.Max(component.CollisionBox.Top, other.CollisionBox.Top);
        Rectangle collisionBox = component.CollisionBox;
        int left1 = collisionBox.Left;
        collisionBox = other.CollisionBox;
        int left2 = collisionBox.Left;
        int x = Math.Max(left1, left2);
        collisionBox = component.CollisionBox;
        int right1 = collisionBox.Right;
        collisionBox = other.CollisionBox;
        int right2 = collisionBox.Right;
        int width = Math.Min(right1, right2) - x;
        collisionBox = component.CollisionBox;
        int bottom1 = collisionBox.Bottom;
        collisionBox = other.CollisionBox;
        int bottom2 = collisionBox.Bottom;
        int height = Math.Min(bottom1, bottom2) - y;
        Rectangle rectangle = new Rectangle(x, y, width, height);
        collisionBox = component.CollisionBox;
        if (collisionBox.Intersects(other.TopLine))
        {
          collisionBox = component.CollisionBox;
          if (collisionBox.Intersects(other.RightLine))
          {
            if (width > height)
            {
              this.GameObject.Transform.Position = this.GameObject.Transform.Position with
              {
                Y = (float) (other.CollisionBox.Y - component.CollisionBox.Height)
              };
              this.hasJumped = false;
              if ((double) this.velocity.Y > 0.0)
              {
                this.velocity.Y = 0.0f;
                goto label_33;
              }
              else
                goto label_33;
            }
            else
            {
              this.GameObject.Transform.Position = this.GameObject.Transform.Position with
              {
                X = (float) (other.CollisionBox.X + other.CollisionBox.Width)
              };
              goto label_33;
            }
          }
        }
        collisionBox = component.CollisionBox;
        if (collisionBox.Intersects(other.TopLine))
        {
          collisionBox = component.CollisionBox;
          if (collisionBox.Intersects(other.LeftLine))
          {
            if (width > height)
            {
              this.GameObject.Transform.Position = this.GameObject.Transform.Position with
              {
                Y = (float) (other.CollisionBox.Y - component.CollisionBox.Height)
              };
              this.hasJumped = false;
              if ((double) this.velocity.Y > 0.0)
              {
                this.velocity.Y = 0.0f;
                goto label_33;
              }
              else
                goto label_33;
            }
            else
            {
              this.GameObject.Transform.Position = this.GameObject.Transform.Position with
              {
                X = (float) (other.CollisionBox.X - component.CollisionBox.Width)
              };
              goto label_33;
            }
          }
        }
        collisionBox = component.CollisionBox;
        if (collisionBox.Intersects(other.BottomLine))
        {
          collisionBox = component.CollisionBox;
          if (collisionBox.Intersects(other.LeftLine))
          {
            if (width > height)
            {
              this.GameObject.Transform.Position = this.GameObject.Transform.Position with
              {
                Y = (float) (other.CollisionBox.Y + other.CollisionBox.Height)
              };
              this.velocity.Y = 0.0f;
              goto label_33;
            }
            else
            {
              this.GameObject.Transform.Position = this.GameObject.Transform.Position with
              {
                X = (float) (other.CollisionBox.X - component.CollisionBox.Width)
              };
              goto label_33;
            }
          }
        }
        collisionBox = component.CollisionBox;
        if (collisionBox.Intersects(other.BottomLine))
        {
          collisionBox = component.CollisionBox;
          if (collisionBox.Intersects(other.RightLine))
          {
            if (width > height)
            {
              this.GameObject.Transform.Position = this.GameObject.Transform.Position with
              {
                Y = (float) (other.CollisionBox.Y + other.CollisionBox.Height)
              };
              this.velocity.Y = 0.0f;
              goto label_33;
            }
            else
            {
              this.GameObject.Transform.Position = this.GameObject.Transform.Position with
              {
                X = (float) (other.CollisionBox.X + other.CollisionBox.Width)
              };
              goto label_33;
            }
          }
        }
        collisionBox = component.CollisionBox;
        if (collisionBox.Intersects(other.TopLine))
        {
          this.GameObject.Transform.Position = this.GameObject.Transform.Position with
          {
            Y = (float) (other.CollisionBox.Y - component.CollisionBox.Height)
          };
          this.hasJumped = false;
          this.velocity.Y = 0.0f;
        }
        else
        {
          collisionBox = component.CollisionBox;
          if (collisionBox.Intersects(other.BottomLine))
          {
            this.GameObject.Transform.Position = this.GameObject.Transform.Position with
            {
              Y = (float) (other.CollisionBox.Y + other.CollisionBox.Height)
            };
            this.velocity.Y = 0.0f;
          }
          else
          {
            collisionBox = component.CollisionBox;
            if (collisionBox.Intersects(other.LeftLine))
            {
              this.GameObject.Transform.Position = this.GameObject.Transform.Position with
              {
                X = (float) (other.CollisionBox.X - component.CollisionBox.Width)
              };
            }
            else
            {
              collisionBox = component.CollisionBox;
              if (collisionBox.Intersects(other.RightLine))
                this.GameObject.Transform.Position = this.GameObject.Transform.Position with
                {
                  X = (float) (other.CollisionBox.X + other.CollisionBox.Width)
                };
            }
          }
        }
      }
label_33:
      if (other.GameObject.GetComponent("Lever") == null)
        return;
      this.canInteract = true;
      this.lastknownLever = (Lever) other.GameObject.GetComponent("Lever");
    }

    public void OnCollisionExit(Collider other)
    {
    }
  }
}
