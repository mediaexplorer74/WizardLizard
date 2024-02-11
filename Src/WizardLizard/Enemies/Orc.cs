// GameManager.Orc

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;

#nullable disable
namespace GameManager
{
  public class Orc : Component, ILoadable, IUpdateable, IAnimateable, ICollisionEnter, ICollisionExit
  {
    private SoundEffect chaseSound;
    private SoundEffect attackSound;
    private SoundEffect hitSound;
    private SoundEffect dieSound;
    private bool playSoundOne = true;
    private bool playSoundTwo = true;
    private Director director;
    private Transform transform;
    private Animator animator;
    private float speed = 200f;
    private Vector2 orcPos;
    private Vector2 velocity;
    private bool orcCanBeHit;
    private bool attacking;
    private bool attack;
    private bool dying;
    private string direction = "Left";
    private Vector2 centering = new Vector2(0.0f, 0.0f);
    private int health;
    private int chanceToSpawnHealth = 50;

    public Orc(GameObject gameObject)
      : base(gameObject)
    {
      this.animator = (Animator) this.GameObject.GetComponent("Animator");
      this.transform = this.GameObject.Transform;
      this.health = 15;
    }

    public void LoadContent(ContentManager content)
    {
      this.chaseSound = content.Load<SoundEffect>("OgreChasePlayer");
      this.attackSound = content.Load<SoundEffect>("OgreAttack");
      this.dieSound = content.Load<SoundEffect>("OgreDieSound2");
      this.hitSound = content.Load<SoundEffect>("OgreHitSound");
      this.CreateAnimations();
    }

    public void Update()
    {
      this.orcCanBeHit = true;
      this.orcPos = new Vector2(this.transform.Position.X + this.centering.X, this.transform.Position.Y + this.centering.X);
      double num = Math.Sqrt(((double) this.orcPos.X - (double) Game1.PlayerPos.X) * ((double) this.orcPos.X - (double) Game1.PlayerPos.X) + ((double) this.orcPos.Y - (double) Game1.PlayerPos.Y) * ((double) this.orcPos.Y - (double) Game1.PlayerPos.Y));
      double xdistance = Math.Sqrt(((double) this.orcPos.X - (double) Game1.PlayerPos.X) * ((double) this.orcPos.X - (double) Game1.PlayerPos.X));
      if (!this.dying)
      {
        if (!this.attack)
        {
          if (num <= 500.0)
          {
            this.Chase(xdistance);
            if (num <= 500.0 && this.playSoundOne)
            {
              this.chaseSound.Play();
              this.playSoundOne = false;
            }
          }
          if (num <= 100.0)
            this.Attack();
          else
            this.Idle();
        }
        if (num > 500.0)
          this.playSoundOne = true;
        if (this.attacking && this.animator.AnimationName == "Attack" + this.direction && 15 <= this.animator.CurrentIndex && this.animator.CurrentIndex <= 19)
        {
          this.director = new Director((IBuilder) new AttackFieldBuilder());
          if (this.direction == "Right")
          {
            Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(this.transform.Position.X + 180f, this.transform.Position.Y + 98f), 90, 122, nameof (Orc)));
            this.attacking = false;
          }
          else if (this.direction == "Left")
          {
            Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(this.transform.Position.X + 15f, this.transform.Position.Y + 98f), 90, 122, nameof (Orc)));
            this.attacking = false;
          }
        }
      }
      if (this.health > 0)
        return;
      this.animator.PlayAnimation("Die" + this.direction);
      if (this.playSoundTwo)
      {
        this.dieSound.Play();
        this.playSoundTwo = false;
      }
      this.dying = true;
    }

    public void TakeDamage(int dmg)
    {
      if (!this.orcCanBeHit)
        return;
      this.health -= dmg;
      if (this.health >= 1)
        this.hitSound.Play();
      this.orcCanBeHit = false;
    }

    public void Idle()
    {
      Vector2 vector2 = new Vector2(0.0f, 0.0f);
      this.velocity.Y += 0.05f * 5f;
      if ((double) this.velocity.Y > 10.0)
        this.velocity.Y = 10f;
      this.transform.Translate((vector2 + this.velocity) * Game1.DeltaTime * this.speed);
    }

    public void Chase(double xdistance)
    {
      Vector2 vector2 = new Vector2(0.0f, 0.0f);
      if (xdistance > 15.0)
      {
        if ((double) Game1.PlayerPos.X > (double) this.orcPos.X)
        {
          ++vector2.X;
          this.direction = "Right";
          this.animator.PlayAnimation("Walk" + this.direction);
        }
        if ((double) Game1.PlayerPos.X < (double) this.orcPos.X)
        {
          --vector2.X;
          this.direction = "Left";
          this.animator.PlayAnimation("Walk" + this.direction);
        }
      }
      if ((double) vector2.X == 0.0)
        this.animator.PlayAnimation("Idle" + this.direction);
      this.velocity.Y += 0.05f * 5f;
      if ((double) this.velocity.Y > 10.0)
        this.velocity.Y = 10f;
      this.transform.Translate((vector2 + this.velocity) * Game1.DeltaTime * this.speed);
    }

    public void Attack()
    {
      if (this.attacking)
        return;
      this.animator.PlayAnimation(nameof (Attack) + this.direction);
      this.attack = true;
      this.attacking = true;
    }

    public void CreateAnimations()
    {
      this.animator.CreateAnimation("IdleRight", new Animation(8, 0, 0, 145, 150, 10f, Vector2.Zero));
      this.animator.CreateAnimation("IdleLeft", new Animation(8, 0, 1160, 145, 150, 10f, Vector2.Zero));
      this.animator.CreateAnimation("AttackLeft", new Animation(22, 150, 0, 271, 220, 22f, new Vector2(50f, 0.0f)));
      this.animator.CreateAnimation("AttackRight", new Animation(22, 370, 0, 271, 220, 22f, new Vector2(-100f, 0.0f)));
      this.animator.CreateAnimation("WalkLeft", new Animation(11, 590, 0, 181, 160, 11f, Vector2.Zero));
      this.animator.CreateAnimation("WalkRight", new Animation(11, 590, 1991, 181, 160, 11f, Vector2.Zero));
      this.animator.CreateAnimation("DieLeft", new Animation(9, 750, 0, 275, 200, 9f, Vector2.Zero));
      this.animator.CreateAnimation("DieRight", new Animation(9, 750, 2475, 275, 200, 9f, Vector2.Zero));
      this.animator.PlayAnimation("IdleLeft");
    }

    public void OnAnimationDone(string animationName)
    {
      if (animationName == "Attack" + this.direction)
      {
        this.attackSound.Play();
        this.attacking = false;
        this.attack = false;
      }
      if (animationName == "Die" + this.direction)
      {
        Game1.Instance.RemoveGameObject(this.GameObject);
        if (new Random().Next(0, 101) <= this.chanceToSpawnHealth)
        {
          this.director = new Director((IBuilder) new HealthGlobeBuilder());
          Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(this.transform.Position.X, this.transform.Position.Y)));
        }
      }
      if (this.dying)
        return;
      this.animator.PlayAnimation("Idle" + this.direction);
    }

    public void OnCollisionEnter(Collider other)
    {
      if (other.GameObject.GetComponent("SolidPlatform") == null)
        return;
      Collider component = (Collider) this.GameObject.GetComponent("Collider");
      this.centering = new Vector2((float) (component.CollisionBox.Width / 2), (float) (component.CollisionBox.Height / 2));
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
      {
        this.GameObject.Transform.Position = this.GameObject.Transform.Position with
        {
          X = (float) (other.CollisionBox.X - component.CollisionBox.Width)
        };
      }
      else
      {
        if (!component.CollisionBox.Intersects(other.RightLine))
          return;
        this.GameObject.Transform.Position = this.GameObject.Transform.Position with
        {
          X = (float) (other.CollisionBox.X + other.CollisionBox.Width)
        };
      }
    }

    public void OnCollisionExit(Collider other)
    {
    }
  }
}
