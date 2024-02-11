// GameManager.Archer

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;

#nullable disable
namespace GameManager
{
  public class Archer : 
    Component,
    ILoadable,
    IUpdateable,
    IAnimateable,
    ICollisionEnter,
    ICollisionExit
  {
    private SoundEffect shootSound;
    private SoundEffect dieSound;
    private SoundEffect hitSound;
    private Transform transform;
    private Animator animator;
    private Director director;
    private Vector2 archerPos;
    private const float delay = 3f;
    private float countdown = 3f;
    private Vector2 velocity;
    private int speed = 100;
    private bool archerCanBeHit;
    private int health;
    private bool shooting;
    private string Direction;
    private bool dying;
    private int chanceToSpawnHealth = 50;

    public float Delay { get; set; }

    public Archer(GameObject gameObject)
      : base(gameObject)
    {
      this.animator = (Animator) this.GameObject.GetComponent("Animator");
      this.transform = gameObject.Transform;
      this.health = 3;
      this.Direction = "Left";
      this.shooting = false;
    }

    public void LoadContent(ContentManager content)
    {
      this.shootSound = content.Load<SoundEffect>("ArcherShoot");
      this.dieSound = content.Load<SoundEffect>("GoblinDieSound3");
      this.hitSound = content.Load<SoundEffect>("GoblinHitSound");
      this.animator.CreateAnimation("DieLeft", new Animation(7, 0, 0, 92, 90, 12f, Vector2.Zero));
      this.animator.CreateAnimation("DieRight", new Animation(7, 90, 0, 92, 90, 12f, Vector2.Zero));
      this.animator.CreateAnimation("AttackLeft", new Animation(9, 180, 0, 77, 90, 8f, new Vector2(3f, 0.0f)));
      this.animator.CreateAnimation("AttackRight", new Animation(9, 270, 0, 77, 90, 8f, new Vector2(3f, 0.0f)));
      this.animator.CreateAnimation("IdleLeft", new Animation(5, 360, 0, 78, 90, 8f, Vector2.Zero));
      this.animator.CreateAnimation("IdleRight", new Animation(5, 450, 0, 78, 90, 8f, Vector2.Zero));
      this.animator.PlayAnimation("IdleLeft");
    }

    public void OnAnimationDone(string animationName)
    {
      if (animationName.Contains("Left") && !this.dying)
        this.animator.PlayAnimation("IdleLeft");
      else if (animationName.Contains("Right") && !this.dying)
        this.animator.PlayAnimation("IdleRight");
      if (!(animationName == "DieLeft") && !(animationName == "DieRight"))
        return;
      Game1.ObjectsToRemove.Add(this.GameObject);
    }

    public void OnCollisionEnter(Collider other)
    {
      if (other.GameObject.GetComponent("SolidPlatform") != null)
      {
        Collider component = (Collider) this.GameObject.GetComponent("Collider");
        int top1 = component.CollisionBox.Top;
        Rectangle collisionBox1 = other.CollisionBox;
        int top2 = collisionBox1.Top;
        int y = Math.Max(top1, top2);
        collisionBox1 = component.CollisionBox;
        int left1 = collisionBox1.Left;
        Rectangle collisionBox2 = other.CollisionBox;
        int left2 = collisionBox2.Left;
        int x = Math.Max(left1, left2);
        collisionBox2 = component.CollisionBox;
        int right1 = collisionBox2.Right;
        Rectangle collisionBox3 = other.CollisionBox;
        int right2 = collisionBox3.Right;
        int width = Math.Min(right1, right2) - x;
        collisionBox3 = component.CollisionBox;
        int height = Math.Min(collisionBox3.Bottom, other.CollisionBox.Bottom) - y;
        Rectangle rectangle = new Rectangle(x, y, width, height);
        if (component.CollisionBox.Intersects(other.TopLine) && component.CollisionBox.Intersects(other.RightLine))
        {
          if (width > height)
          {
            this.GameObject.Transform.Position = this.GameObject.Transform.Position with
            {
              Y = (float) (other.CollisionBox.Y - component.CollisionBox.Height)
            };
            this.velocity.Y = 0.0f;
          }
          else
            this.GameObject.Transform.Position = this.GameObject.Transform.Position with
            {
              X = (float) (other.CollisionBox.X + other.CollisionBox.Width)
            };
        }
        else if (component.CollisionBox.Intersects(other.TopLine) && component.CollisionBox.Intersects(other.LeftLine))
        {
          if (width > height)
          {
            this.GameObject.Transform.Position = this.GameObject.Transform.Position with
            {
              Y = (float) (other.CollisionBox.Y - component.CollisionBox.Height)
            };
            this.velocity.Y = 0.0f;
          }
          else
            this.GameObject.Transform.Position = this.GameObject.Transform.Position with
            {
              X = (float) (other.CollisionBox.X - component.CollisionBox.Width)
            };
        }
        else if (component.CollisionBox.Intersects(other.BottomLine) && component.CollisionBox.Intersects(other.LeftLine))
        {
          if (width > height)
          {
            this.GameObject.Transform.Position = this.GameObject.Transform.Position with
            {
              Y = (float) (other.CollisionBox.Y + other.CollisionBox.Height)
            };
            this.velocity.Y = 0.0f;
          }
          else
            this.GameObject.Transform.Position = this.GameObject.Transform.Position with
            {
              X = (float) (other.CollisionBox.X - component.CollisionBox.Width)
            };
        }
        else if (component.CollisionBox.Intersects(other.BottomLine) && component.CollisionBox.Intersects(other.RightLine))
        {
          if (width > height)
          {
            this.GameObject.Transform.Position = this.GameObject.Transform.Position with
            {
              Y = (float) (other.CollisionBox.Y + other.CollisionBox.Height)
            };
            this.velocity.Y = 0.0f;
          }
          else
            this.GameObject.Transform.Position = this.GameObject.Transform.Position with
            {
              X = (float) (other.CollisionBox.X + other.CollisionBox.Width)
            };
        }
        else if (component.CollisionBox.Intersects(other.TopLine))
        {
          this.GameObject.Transform.Position = this.GameObject.Transform.Position with
          {
            Y = (float) (other.CollisionBox.Y - component.CollisionBox.Height)
          };
          this.velocity.Y = 0.0f;
        }
        else if (component.CollisionBox.Intersects(other.BottomLine))
        {
          this.GameObject.Transform.Position = this.GameObject.Transform.Position with
          {
            Y = (float) (other.CollisionBox.Y + other.CollisionBox.Height)
          };
          this.velocity.Y = 0.0f;
        }
        else if (component.CollisionBox.Intersects(other.LeftLine))
          this.GameObject.Transform.Position = this.GameObject.Transform.Position with
          {
            X = (float) (other.CollisionBox.X - component.CollisionBox.Width)
          };
        else if (component.CollisionBox.Intersects(other.RightLine))
          this.GameObject.Transform.Position = this.GameObject.Transform.Position with
          {
            X = (float) (other.CollisionBox.X + other.CollisionBox.Width)
          };
      }
      if (other.GameObject.GetComponent("NonSolidPlatform") == null)
        return;
      Collider component1 = (Collider) this.GameObject.GetComponent("Collider");
      if (!component1.CollisionBox.Intersects(other.TopLine) || (double) this.velocity.Y <= 0.0)
        return;
      int num1 = Math.Max(component1.CollisionBox.Top, other.CollisionBox.Top);
      Rectangle collisionBox4 = component1.CollisionBox;
      int left3 = collisionBox4.Left;
      collisionBox4 = other.CollisionBox;
      int left4 = collisionBox4.Left;
      int num2 = Math.Max(left3, left4);
      Rectangle collisionBox5 = component1.CollisionBox;
      int right3 = collisionBox5.Right;
      collisionBox5 = other.CollisionBox;
      int right4 = collisionBox5.Right;
      if (Math.Min(right3, right4) - num2 <= Math.Min(component1.CollisionBox.Bottom, other.CollisionBox.Bottom) - num1 || component1.CollisionBox.Y + component1.CollisionBox.Height - 20 >= other.TopLine.Y)
        return;
      this.GameObject.Transform.Position = this.GameObject.Transform.Position with
      {
        Y = (float) (other.CollisionBox.Y - component1.CollisionBox.Height)
      };
      this.velocity.Y = 0.0f;
    }

    public void OnCollisionExit(Collider other)
    {
    }

    public void TakeDamage(int dmg)
    {
      if (!this.archerCanBeHit)
        return;
      this.health -= dmg;
      if (this.health >= 1)
        this.hitSound.Play();
      this.archerCanBeHit = false;
    }

    public void Update()
    {
      this.archerCanBeHit = true;
      Vector2 vector2 = new Vector2(0.0f, 0.0f);
      this.velocity.Y += 0.05f * 5f;
      if ((double) this.velocity.Y > 10.0)
        this.velocity.Y = 10f;
      this.transform.Translate((vector2 + this.velocity) * Game1.DeltaTime * (float) this.speed);
      if (this.dying)
        return;
      this.archerPos = new Vector2(this.transform.Position.X, this.transform.Position.Y);
      this.Direction = (double) this.archerPos.X <= (double) Game1.PlayerPos.X ? "Right" : "Left";

      if (Math.Sqrt(((double) this.archerPos.X - (double) Game1.PlayerPos.X)
          * ((double) this.archerPos.X - (double) Game1.PlayerPos.X) 
          + ((double) this.archerPos.Y - (double) Game1.PlayerPos.Y) 
          * ((double) this.archerPos.Y - (double) Game1.PlayerPos.Y)) < 800.0)
      {
        if (this.Timer())
        {
          this.shooting = true;
          this.animator.PlayAnimation("Attack" + this.Direction);
        }
        if (this.animator.CurrentIndex >= 5 && this.shooting 
                    && this.animator.AnimationName.Contains("Attack"))
        {
          this.shooting = false;
          this.shoot();
        }
      }
      if (this.health > 0)
        return;
      this.dieSound.Play();
      this.animator.PlayAnimation("Die" + this.Direction);
      this.dying = true;
      if (new Random().Next(0, 101) > this.chanceToSpawnHealth)
        return;
      this.director = new Director((IBuilder) new HealthGlobeBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(this.transform.Position.X, 
          this.transform.Position.Y)));
    }

    public void shoot()
    {
      this.shootSound.Play();
      this.director = new Director((IBuilder) new ArrowBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(this.transform.Position.X
          + 50f, this.transform.Position.Y + 10f)));
    }

    public bool Timer()
    {
      if ((double) this.countdown > 0.0)
      {
        this.countdown -= Game1.DeltaTime;
        return false;
      }
      this.countdown = 3f;
      return true;
    }
  }
}
