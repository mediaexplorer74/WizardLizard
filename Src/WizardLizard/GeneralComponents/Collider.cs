// GameManager.Collider

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

#nullable disable
namespace GameManager
{
  public class Collider : Component, IDrawable, ILoadable, IUpdateable
  {
    private SpriteRenderer spriterenderer;
    private Texture2D texture2D;
    private List<Collider> otherColliders = new List<Collider>();
    private bool doCollisionChecks;
    private Rectangle topLine;
    private Rectangle bottomLine;
    private Rectangle rightLine;
    private Rectangle leftLine;
    private int width;
    private int height;

    public Collider(GameObject gameObject)
      : base(gameObject)
    {
      Game1.Instance.Colliders.Add(this);
    }

    public Collider(GameObject gameObject, int width, int height)
      : base(gameObject)
    {
      Game1.Instance.Colliders.Add(this);
      this.width = width;
      this.height = height;
    }

    public Rectangle CollisionBox
    {
      get
      {
        return this.spriterenderer != null ? new Rectangle((int) ((double) this.GameObject.Transform.Position.X - (double) this.spriterenderer.Offset.X), (int) ((double) this.GameObject.Transform.Position.Y - (double) this.spriterenderer.Offset.Y), this.spriterenderer.Rectangle.Width, this.spriterenderer.Rectangle.Height) : new Rectangle((int) this.GameObject.Transform.Position.X, (int) this.GameObject.Transform.Position.Y, this.width, this.height);
      }
    }

    public bool DoCollisionChecks
    {
      set => this.doCollisionChecks = value;
    }

    public Rectangle TopLine
    {
      get => this.topLine;
      set => this.topLine = value;
    }

    public Rectangle BottomLine
    {
      get => this.bottomLine;
      set => this.bottomLine = value;
    }

    public Rectangle RightLine
    {
      get => this.rightLine;
      set => this.rightLine = value;
    }

    public Rectangle LeftLine
    {
      get => this.leftLine;
      set => this.leftLine = value;
    }

    public void LoadContent(ContentManager content)
    {
      if (this.GameObject.GetComponent("SpriteRenderer") != null)
        this.spriterenderer = (SpriteRenderer) this.GameObject.GetComponent("SpriteRenderer");
      this.texture2D = content.Load<Texture2D>("CollisionTexture");
    }

    public void Update() => this.CheckCollision();

    public void Draw(SpriteBatch spriteBatch)
    {
      this.TopLine = new Rectangle(this.CollisionBox.X, this.CollisionBox.Y, this.CollisionBox.Width, 1);
      this.BottomLine = new Rectangle(this.CollisionBox.X, this.CollisionBox.Y + this.CollisionBox.Height, this.CollisionBox.Width, 1);
      this.RightLine = new Rectangle(this.CollisionBox.X + this.CollisionBox.Width, this.CollisionBox.Y, 1, this.CollisionBox.Height);
      this.LeftLine = new Rectangle(this.CollisionBox.X, this.CollisionBox.Y, 1, this.CollisionBox.Height);
    }

    private void CheckCollision()
    {
      foreach (Collider collider in Game1.Instance.Colliders)
      {
        if (collider != this && collider != null)
        {
          if (this.CollisionBox.Intersects(collider.CollisionBox))
          {
            if (!this.otherColliders.Contains(collider))
            {
              this.otherColliders.Add(this);
              this.GameObject.OnCollisionEnter(collider);
            }
          }
          else if (this.otherColliders.Contains(this))
          {
            this.otherColliders.Remove(this);
            this.GameObject.OnCollisionExit(collider);
          }
        }
      }
    }
  }
}
