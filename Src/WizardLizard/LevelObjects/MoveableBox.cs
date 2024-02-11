// GameManager.MoveableBox

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

#nullable disable
namespace GameManager
{
  public class MoveableBox : 
    Component,
    ILoadable,
    IUpdateable,
    IAnimateable,
    ICollisionEnter,
    ICollisionExit
  {
    private Transform transform;
    private bool platformEdge;
    private Vector2 velocity;
    private Animator animator;
    private int speed = 300;

    public MoveableBox(GameObject gameObject)
      : base(gameObject)
    {
      this.animator = (Animator) this.GameObject.GetComponent("Animator");
      this.transform = gameObject.Transform;
    }

    public void LoadContent(ContentManager content)
    {
    }

    public void Update()
    {
      this.platformEdge = false;
      Vector2 vector2 = new Vector2(0.0f, 0.0f);
      this.velocity.Y += 0.05f * 5f;
      if ((double) this.velocity.Y > 10.0)
        this.velocity.Y = 10f;
      this.transform.Translate((vector2 + this.velocity) * Game1.DeltaTime * (float) this.speed);
    }

    public void OnAnimationDone(string animationName)
    {
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
            if ((double) this.velocity.Y > 0.0)
              this.velocity.Y = 0.0f;
          }
          else
          {
            this.GameObject.Transform.Position = this.GameObject.Transform.Position with
            {
              X = (float) (other.CollisionBox.X + other.CollisionBox.Width)
            };
            this.platformEdge = true;
          }
        }
        else if (component.CollisionBox.Intersects(other.TopLine) && component.CollisionBox.Intersects(other.LeftLine))
        {
          if (width > height)
          {
            this.GameObject.Transform.Position = this.GameObject.Transform.Position with
            {
              Y = (float) (other.CollisionBox.Y - component.CollisionBox.Height)
            };
            if ((double) this.velocity.Y > 0.0)
              this.velocity.Y = 0.0f;
          }
          else
          {
            this.GameObject.Transform.Position = this.GameObject.Transform.Position with
            {
              X = (float) (other.CollisionBox.X - component.CollisionBox.Width)
            };
            this.platformEdge = true;
          }
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
          {
            this.GameObject.Transform.Position = this.GameObject.Transform.Position with
            {
              X = (float) (other.CollisionBox.X - component.CollisionBox.Width)
            };
            this.platformEdge = true;
          }
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
          {
            this.GameObject.Transform.Position = this.GameObject.Transform.Position with
            {
              X = (float) (other.CollisionBox.X + other.CollisionBox.Width)
            };
            this.platformEdge = true;
          }
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
          this.platformEdge = true;
        }
        else if (component.CollisionBox.Intersects(other.RightLine))
        {
          this.GameObject.Transform.Position = this.GameObject.Transform.Position with
          {
            X = (float) (other.CollisionBox.X + other.CollisionBox.Width)
          };
          this.platformEdge = true;
        }
      }
      if (other.GameObject.GetComponent("Player") == null && other.GameObject.GetComponent("Goblin") == null && other.GameObject.GetComponent("Orc") == null)
        return;
      Collider component1 = (Collider) this.GameObject.GetComponent("Collider");
      int top3 = component1.CollisionBox.Top;
      Rectangle collisionBox4 = other.CollisionBox;
      int top4 = collisionBox4.Top;
      int y1 = Math.Max(top3, top4);
      collisionBox4 = component1.CollisionBox;
      int left3 = collisionBox4.Left;
      Rectangle collisionBox5 = other.CollisionBox;
      int left4 = collisionBox5.Left;
      int x1 = Math.Max(left3, left4);
      collisionBox5 = component1.CollisionBox;
      int right3 = collisionBox5.Right;
      Rectangle collisionBox6 = other.CollisionBox;
      int right4 = collisionBox6.Right;
      int width1 = Math.Min(right3, right4) - x1;
      collisionBox6 = component1.CollisionBox;
      int height1 = Math.Min(collisionBox6.Bottom, other.CollisionBox.Bottom) - y1;
      Rectangle rectangle1 = new Rectangle(x1, y1, width1, height1);
      if (this.platformEdge || !other.CollisionBox.Intersects(component1.LeftLine) && !other.CollisionBox.Intersects(component1.RightLine) || width1 >= height1)
        return;
      Vector2 position = this.GameObject.Transform.Position;
      if ((double) other.GameObject.Transform.Position.X > (double) position.X)
        position.X -= (float) width1;
      else
        position.X += (float) width1;
      this.GameObject.Transform.Position = position;
    }

    public void OnCollisionExit(Collider other)
    {
    }
  }
}
