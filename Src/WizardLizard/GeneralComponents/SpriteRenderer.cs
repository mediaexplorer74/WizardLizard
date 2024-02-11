// GameManager.SpriteRenderer

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace GameManager
{
  public class SpriteRenderer : Component, IDrawable, ILoadable
  {
    private Color color;
    private Rectangle rectangle;
    private Texture2D sprite;
    private Vector2 offset;
    private string spriteName;
    private float layerDepth;

    public Rectangle Rectangle
    {
      get => this.rectangle;
      set => this.rectangle = value;
    }

    public Vector2 Offset
    {
      get => this.offset;
      set => this.offset = value;
    }

    public Texture2D Sprite
    {
      get => this.sprite;
      set => this.sprite = value;
    }

    public Color Color
    {
      get => this.color;
      set => this.color = value;
    }

    public SpriteRenderer(GameObject gameObject, string spriteName, float layerDepth)
      : base(gameObject)
    {
      this.spriteName = spriteName;
      this.layerDepth = layerDepth;
    }

    public void LoadContent(ContentManager content)
    {
      this.sprite = content.Load<Texture2D>(this.spriteName);
      this.rectangle = new Rectangle(0, 0, this.sprite.Width, this.sprite.Height);
      this.color = Color.White;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(this.sprite, this.GameObject.Transform.Position, new Rectangle?(this.rectangle), this.color, 0.0f, this.offset, 1f, SpriteEffects.None, 1f);
    }
  }
}
