// GameManager.Player

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;

#nullable disable
namespace GameManager
{
  public class Player : 
    Component,
    ILoadable,
    IUpdateable,
    IAnimateable,
    ICollisionEnter,
    ICollisionExit
  {
    private SoundEffect jumpSound;
    private SoundEffect attackSound;
    private SoundEffect hitSound;
    private SoundEffect dieSound;
    private Vector2 velocity;
    private Transform transform;
    private Animator animator;
    private bool shiftControle;
    private int speed = 200;
    private static int health;
    private static bool hasJumped;
    private bool dying;
    private bool fireball = true;
    private bool lightning = true;
    private bool shield = true;
    private bool attack = true;
    private bool attacking;
    private Director director;
    private bool canInteract;
    private bool haveInteracted = true;
    private bool playerCanBeHit;
    private bool shooting;
    private Lever lastknownLever;
    private string direction;
    private Vector2 centering = new Vector2(0.0f, 0.0f);
    private const float fireballCooldown = 3f;
    private float fireballCountdown = 3f;
    private const float lightningstrikCooldown = 3f;
    private float lightningstrikeCountdown = 3f;
    private const float shieldCooldown = 10f;
    private float shieldCountdown = 10f;

    public float Delay { get; set; }

    public static int Health
    {
      get => Player.health;
      set => Player.health = value;
    }

    public static bool HasJumped
    {
      get => Player.hasJumped;
      set => Player.hasJumped = value;
    }

    public Player(GameObject gameObject)
      : base(gameObject)
    {
      this.animator = (Animator) this.GameObject.GetComponent("Animator");
      this.transform = gameObject.Transform;
      Player.health = 6;
      Player.hasJumped = true;
      this.shiftControle = true;
      this.direction = "Right";
    }

    public void LoadContent(ContentManager content)
    {
      this.jumpSound = content.Load<SoundEffect>("PlayerJump");
      this.attackSound = content.Load<SoundEffect>("PlayerAttack");
      this.hitSound = content.Load<SoundEffect>("PlayerHit");
      this.dieSound = content.Load<SoundEffect>("PlayerDies");
      this.animator.CreateAnimation("IdleRight", new Animation(1, 0, 22, 64, 100, 1f, Vector2.Zero));
      this.animator.CreateAnimation("IdleLeft", new Animation(1, 100, 24, 64, 100, 1f, Vector2.Zero));
      this.animator.CreateAnimation("DieRight", new Animation(8, 0, 0, 109, 100, 16f, new Vector2(22f, 0.0f)));
      this.animator.CreateAnimation("DieLeft", new Animation(8, 100, 0, 109, 100, 16f, new Vector2(24f, 0.0f)));
      this.animator.CreateAnimation("DeadRight", new Animation(1, 0, 763, 109, 100, 1f, new Vector2(22f, 0.0f)));
      this.animator.CreateAnimation("DeadLeft", new Animation(1, 100, 763, 109, 100, 1f, new Vector2(24f, 0.0f)));
      this.animator.CreateAnimation("AttackLeft", new Animation(19, 200, 0, 110, 119, 57f, new Vector2(22f, 0.0f)));
      this.animator.CreateAnimation("AttackRight", new Animation(19, 319, 0, 110, 119, 57f, new Vector2(37f, 0.0f)));
      this.animator.CreateAnimation("CastFireRight", new Animation(13, 438, 0, 83, 112, 26f, new Vector2(13f, 0.0f)));
      this.animator.CreateAnimation("CastFireLeft", new Animation(13, 550, 0, 83, 112, 26f, new Vector2(13f, 0.0f)));
      this.animator.CreateAnimation("CastLightRight", new Animation(13, 662, 0, 83, 112, 26f, new Vector2(13f, 0.0f)));
      this.animator.CreateAnimation("CastLightLeft", new Animation(13, 774, 0, 83, 112, 26f, new Vector2(13f, 0.0f)));
      this.animator.CreateAnimation("RunRight", new Animation(23, 886, 0, 107, 100, 46f, new Vector2(0.0f, 0.0f)));
      this.animator.CreateAnimation("RunLeft", new Animation(23, 986, 0, 107, 100, 46f, new Vector2(0.0f, 0.0f)));
      this.animator.PlayAnimation("IdleRight");
    }

    public void Update()
    {
      if (this.dying)
        return;
      this.playerCanBeHit = true;
      this.PlayerController(Keyboard.GetState(), Vector2.Zero, Mouse.GetState());
      Game1.PlayerPos = new Vector2(this.transform.Position.X + this.centering.X, this.transform.Position.Y + this.centering.Y);
      if (this.attacking && this.animator.CurrentIndex >= 14)
      {
        this.director = new Director((IBuilder) new AttackFieldBuilder());
        if (this.direction == "Right")
        {
          Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(this.transform.Position.X + 37f, this.transform.Position.Y + 8f), 38, 72, nameof (Player)));
          this.attacking = false;
        }
        if (this.direction == "Left")
        {
          Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(this.transform.Position.X - 22f, this.transform.Position.Y + 8f), 38, 72, nameof (Player)));
          this.attacking = false;
        }
      }
      if (Player.Health > 0)
        return;
      this.dieSound.Play();
      this.animator.PlayAnimation("Die" + this.direction);
      this.dying = true;
    }

    public void OnAnimationDone(string animationName)
    {
      if (animationName == "Die" + this.direction)
      {
        this.animator.PlayAnimation("Dead" + this.direction);
      }
      else
      {
        if (this.dying)
          return;
        this.animator.PlayAnimation("Idle" + this.direction);
        if (!(animationName == "Attack" + this.direction))
          return;
        this.attacking = false;
      }
    }

    private void MeleeAttack(MouseState mouseState)
    {
      if (mouseState.LeftButton == ButtonState.Pressed && this.attack && !this.shooting)
      {
        this.attackSound.Play();
        this.attack = false;
        this.animator.PlayAnimation("Attack" + this.direction);
        this.attacking = true;
      }
      if (mouseState.LeftButton != ButtonState.Released)
        return;
      this.attack = true;
    }

    private void Interact(KeyboardState keyState)
    {
      if (keyState.IsKeyDown(Keys.E) && this.canInteract && this.haveInteracted)
      {
        if (this.lastknownLever != null)
          this.lastknownLever.Interaction(this.GameObject);
        this.haveInteracted = false;
        this.canInteract = false;
      }
      if (!keyState.IsKeyUp(Keys.E))
        return;
      this.haveInteracted = true;
    }

    private void CreateShield(KeyboardState keyState)
    {
      foreach (GameObject gameObject in Game1.GameObjects)
      {
        if (gameObject.GetComponent("PlayerShield") != null)
          this.shieldCountdown = 10f;
      }
      if ((double) this.shieldCountdown > 0.0)
        this.shieldCountdown -= Game1.DeltaTime;
      if ((double) this.shieldCountdown > 0.0)
        return;
      if (keyState.IsKeyDown(Keys.Q) && this.shield)
      {
        this.director = new Director((IBuilder) new PlayerShieldBuilder());
        Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(this.transform.Position.X, this.transform.Position.Y)));
        this.shield = false;
      }
      if (!keyState.IsKeyUp(Keys.Q))
        return;
      this.shield = true;
    }

    private void ShootLighting(KeyboardState keyState, MouseState mouseState)
    {
      if ((double) this.lightningstrikeCountdown > 0.0)
        this.lightningstrikeCountdown -= Game1.DeltaTime;
      if ((double) this.lightningstrikeCountdown <= 0.0)
      {
        this.lightningstrikeCountdown = 0.0f;
        if (keyState.IsKeyDown(Keys.R) && this.lightning && !this.shooting)
        {
          this.shooting = true;
          this.animator.PlayAnimation("CastLight" + this.direction);
        }
        if (keyState.IsKeyUp(Keys.R))
          this.lightning = true;
      }
      if (this.animator.CurrentIndex < 12 || !this.shooting || !(this.animator.AnimationName == "CastLight" + this.direction))
        return;
      this.director = new Director((IBuilder) new LightningStrikeBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2((float) (mouseState.X - 51), -956f)));
      this.lightning = false;
      this.shooting = false;
      this.lightningstrikeCountdown = 3f;
    }

    private void ShootFireball(MouseState mouseState)
    {
      if ((double) this.fireballCountdown > 0.0)
        this.fireballCountdown -= Game1.DeltaTime;
      if ((double) this.fireballCountdown <= 0.0)
      {
        this.fireballCountdown = 0.0f;
        if (mouseState.RightButton == ButtonState.Pressed && this.fireball && !this.shooting)
        {
          this.shooting = true;
          this.animator.PlayAnimation("CastFire" + this.direction);
        }
        if (mouseState.RightButton == ButtonState.Released)
          this.fireball = true;
      }
      if (this.animator.CurrentIndex < 12 || !this.shooting || !(this.animator.AnimationName == "CastFire" + this.direction))
        return;
      this.director = new Director((IBuilder) new FireballBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(this.transform.Position.X + 10f, this.transform.Position.Y + 20f)));
      this.shooting = false;
      this.fireball = false;
      this.fireballCountdown = 3f;
    }

    private void ShiftToCompanion(KeyboardState keyState)
    {
      if (keyState.IsKeyDown(Keys.Space) && this.shiftControle)
      {
        Companion.CompanionControle = true;
        this.shiftControle = false;
      }
      if (!keyState.IsKeyUp(Keys.Space))
        return;
      this.shiftControle = true;
    }

    private void Jump(KeyboardState keyState, Vector2 translation)
    {
      if (!keyState.IsKeyDown(Keys.W) || Player.hasJumped)
        return;
      this.jumpSound.Play();
      translation.Y -= 5f;
      this.velocity.Y = -5f;
      Player.hasJumped = true;
    }

    private void PlayerController(
      KeyboardState keyState,
      Vector2 translation,
      MouseState mouseState)
    {
      if (!Companion.CompanionControle)
      {
        if (this.animator.AnimationName != "CastFire" + this.direction && this.animator.AnimationName != "CastLight" + this.direction && this.animator.AnimationName != "Attack" + this.direction)
        {
          if (keyState.IsKeyDown(Keys.D))
          {
            translation += new Vector2(1f, 0.0f);
            this.direction = "Right";
            this.animator.PlayAnimation("Run" + this.direction);
          }
          if (keyState.IsKeyDown(Keys.A))
          {
            translation += new Vector2(-1f, 0.0f);
            this.direction = "Left";
            this.animator.PlayAnimation("Run" + this.direction);
          }
          if ((double) translation.X == 0.0)
            this.animator.PlayAnimation("Idle" + this.direction);
        }
        this.Jump(keyState, translation);
        this.ShiftToCompanion(keyState);
        this.ShootFireball(mouseState);
        this.ShootLighting(keyState, mouseState);
        this.Interact(keyState);
        this.MeleeAttack(mouseState);
      }
      this.velocity.Y += 0.05f * 5f;
      translation += this.velocity;
      if ((double) this.velocity.Y > 10.0)
        this.velocity.Y = 10f;
      this.transform.Translate(translation * Game1.DeltaTime * (float) this.speed);
    }

    private void MorphPlayer(KeyboardState keyState)
    {
      if (keyState.IsKeyUp(Keys.F))
        Morph.HasMorphed = false;
      if (!keyState.IsKeyDown(Keys.F) || Morph.HasMorphed)
        return;
      Morph.HasMorphed = true;
      this.director = new Director((IBuilder) new MorphBuilder());
      Game1.Instance.AddGameObject(this.director.Construct(this.transform.Position));
      Game1.Instance.RemoveGameObject(this.GameObject);
      foreach (GameObject gameObject in Game1.GameObjects)
      {
        if (gameObject.GetComponent("Companion") != null)
          Game1.Instance.RemoveGameObject(gameObject);
      }
    }

    public void PlayerHit(int dmg)
    {
      if (!this.playerCanBeHit)
        return;
      Player.Health -= dmg;
      if (!this.dying && Player.health >= 1)
        this.hitSound.Play();
      this.playerCanBeHit = false;
    }

    public void OnCollisionEnter(Collider other)
    {
      if (other.GameObject.GetComponent("SolidPlatform") != null)
      {
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
            Player.hasJumped = false;
            if ((double) this.velocity.Y > 0.0)
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
            Player.hasJumped = false;
            if ((double) this.velocity.Y > 0.0)
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
          Player.hasJumped = false;
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
      if (other.GameObject.GetComponent("Lever") != null)
      {
        this.canInteract = true;
        this.lastknownLever = (Lever) other.GameObject.GetComponent("Lever");
      }
      if (other.GameObject.GetComponent("NonSolidPlatform") == null)
        return;
      Collider component1 = (Collider) this.GameObject.GetComponent("Collider");
      if (!component1.CollisionBox.Intersects(other.TopLine) || (double) this.velocity.Y <= 0.0)
        return;
      int num1 = Math.Max(component1.CollisionBox.Top, other.CollisionBox.Top);
      int num2 = Math.Max(component1.CollisionBox.Left, other.CollisionBox.Left);
      int num3 = Math.Min(component1.CollisionBox.Right, other.CollisionBox.Right) - num2;
      Rectangle collisionBox = component1.CollisionBox;
      int bottom1 = collisionBox.Bottom;
      collisionBox = other.CollisionBox;
      int bottom2 = collisionBox.Bottom;
      int num4 = Math.Min(bottom1, bottom2) - num1;
      if (num3 <= num4 || component1.CollisionBox.Y + component1.CollisionBox.Height - 20 >= other.TopLine.Y)
        return;
      this.GameObject.Transform.Position = this.GameObject.Transform.Position with
      {
        Y = (float) (other.CollisionBox.Y - component1.CollisionBox.Height)
      };
      Player.hasJumped = false;
      this.velocity.Y = 0.0f;
    }

    public void OnCollisionExit(Collider other)
    {
    }
  }
}
