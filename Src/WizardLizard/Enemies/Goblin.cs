// GameManager.Goblin

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;

#nullable disable
namespace GameManager
{
  public class Goblin : 
    Component,
    ILoadable,
    IUpdateable,
    IAnimateable,
    ICollisionEnter,
    ICollisionExit
  {
    private SoundEffect chaseSound;
    private SoundEffect dieSound;
    private SoundEffect attackSound;
    private SoundEffect hitSound;
    private Director director;
    private Vector2 centering = new Vector2(0.0f, 0.0f);
    private Transform transform;
    private Animator animator;
    private Vector2 goblinPos;
    private float speed = 150f;
    private Vector2 velocity;
    private bool goblinCanBeHit;
    private int health;
    private string direction;
    private bool playSoundOne = true;
    private bool dying;
    private bool attacking;
    private bool attack;
    private int chanceToSpawnHealth = 50;
    private GameObject gameObject;

    public Goblin(GameObject gameObject)
      : base(gameObject)
    {
      this.gameObject = gameObject;
      this.animator = (Animator) this.GameObject.GetComponent("Animator");
      this.transform = this.GameObject.Transform;
      this.health = 5;
      this.direction = "Left";
    }

    public void LoadContent(ContentManager content)
    {
      this.chaseSound = content.Load<SoundEffect>("GoblinChaseSound");
      this.dieSound = content.Load<SoundEffect>("GoblinDieSound3");
      this.attackSound = content.Load<SoundEffect>("SwordSlice");
      this.hitSound = content.Load<SoundEffect>("GoblinHitSound");
      this.animator.CreateAnimation("RunLeft", new Animation(12, 0, 0, 58, 90, 16f, Vector2.Zero));
      this.animator.CreateAnimation("RunRight", new Animation(12, 90, 0, 58, 90, 16f, Vector2.Zero));
      this.animator.CreateAnimation("IdleLeft", new Animation(5, 180, 0, 53, 90, 8f, Vector2.Zero));
      this.animator.CreateAnimation("IdleRight", new Animation(5, 270, 0, 53, 90, 8f, Vector2.Zero));
      this.animator.CreateAnimation("DieLeft", new Animation(7, 360, 0, 95, 90, 12f, Vector2.Zero));
      this.animator.CreateAnimation("DieRight", new Animation(7, 450, 0, 95, 90, 12f, Vector2.Zero));
      this.animator.CreateAnimation("AttackLeft", new Animation(9, 540, 0, 70, 90, 16f, Vector2.Zero));
      this.animator.CreateAnimation("AttackRight", new Animation(9, 630, 0, 70, 90, 16f, Vector2.Zero));
      this.animator.PlayAnimation("Idle" + this.direction);
    }

    public void Update()
    {
      if (this.dying)
        return;
      this.goblinCanBeHit = true;
      this.goblinPos = new Vector2(this.transform.Position.X + this.centering.X, this.transform.Position.Y + this.centering.Y);
      this.direction = (double) this.goblinPos.X <= (double) Game1.PlayerPos.X ? "Right" : "Left";
      double num = Math.Sqrt(((double) this.goblinPos.X - (double) Game1.PlayerPos.X) * ((double) this.goblinPos.X - (double) Game1.PlayerPos.X) + ((double) this.goblinPos.Y - (double) Game1.PlayerPos.Y) * ((double) this.goblinPos.Y - (double) Game1.PlayerPos.Y));
      double xdistance = Math.Sqrt(((double) this.goblinPos.X - (double) Game1.PlayerPos.X) * ((double) this.goblinPos.X - (double) Game1.PlayerPos.X));
      if (!this.attack)
      {
        if (num >= 400.0)
          this.Idle();
        if (num < 400.0)
        {
          this.Chase(xdistance);
          if (num < 400.0 && this.playSoundOne)
          {
            this.chaseSound.Play();
            this.playSoundOne = false;
          }
        }
        if (num > 400.0)
          this.playSoundOne = true;
        if (num < 10.0)
        {
          this.Attack();
          this.attackSound.Play();
        }
      }
      if (this.attacking && this.animator.AnimationName == "Attack" + this.direction && 4 <= this.animator.CurrentIndex && this.animator.CurrentIndex <= 6)
      {
        this.director = new Director((IBuilder) new AttackFieldBuilder());
        if (this.direction == "Right")
        {
          Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(this.transform.Position.X + 44f, this.transform.Position.Y + 48f), 27, 16, nameof (Goblin)));
          this.attacking = false;
        }
        else if (this.direction == "Left")
        {
          Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(this.transform.Position.X, this.transform.Position.Y + 48f), 27, 16, nameof (Goblin)));
          this.attacking = false;
        }
      }
      if (this.health > 0)
        return;
      this.dieSound.Play();
      this.animator.PlayAnimation("Die" + this.direction);
      this.dying = true;
      if (new Random().Next(0, 101) > this.chanceToSpawnHealth)
        return;
      this.director = new Director((IBuilder) new HealthGlobeBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(this.transform.Position.X, this.transform.Position.Y)));
    }

    public void Idle()
    {
      this.animator.PlayAnimation(nameof (Idle) + this.direction);
      Vector2 vector2 = new Vector2(0.0f, 0.0f);
      this.velocity.Y += 0.05f * 5f;
      if ((double) this.velocity.Y > 10.0)
        this.velocity.Y = 10f;
      this.transform.Translate((vector2 + this.velocity) * Game1.DeltaTime * this.speed);
    }

    public void Attack()
    {
      this.animator.PlayAnimation(nameof (Attack) + this.direction);
      this.attacking = true;
      this.attack = true;
    }

    public void Chase(double xdistance)
    {
      Vector2 vector2_1 = new Vector2(0.0f, 0.0f);
      Vector2 vector2_2 = new Vector2(this.transform.Position.X + this.centering.X, this.transform.Position.Y + this.centering.Y);
      if (xdistance > 2.0)
      {
        if ((double) Game1.PlayerPos.X > (double) vector2_2.X)
        {
          ++vector2_1.X;
          this.animator.PlayAnimation("Run" + this.direction);
        }
        if ((double) Game1.PlayerPos.X < (double) vector2_2.X)
        {
          --vector2_1.X;
          this.animator.PlayAnimation("Run" + this.direction);
        }
      }
      this.velocity.Y += 0.05f * 5f;
      if ((double) this.velocity.Y > 10.0)
        this.velocity.Y = 10f;
      Vector2 vector2_3 = vector2_1 + this.velocity;
      this.transform.Translate(vector2_3 * Game1.DeltaTime * this.speed);
      if ((double) vector2_3.X != 0.0)
        return;
      this.animator.PlayAnimation("Idle" + this.direction);
    }

    public void TakeDamage(int dmg)
    {
      if (!this.goblinCanBeHit)
        return;
      this.health -= dmg;
      if (this.health >= 1)
        this.hitSound.Play();
      this.goblinCanBeHit = false;
    }

    public void OnAnimationDone(string animationName)
    {
      if (animationName == "AttackLeft" || animationName == "AttackRight")
      {
        this.attacking = false;
        this.attack = false;
      }
      if (!(animationName == "DieLeft") && !(animationName == "DieRight"))
        return;
      Game1.ObjectsToRemove.Add(this.GameObject);
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
