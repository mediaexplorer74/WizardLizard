// GameManager.Button

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#nullable disable
namespace GameManager
{
  public class Button
  {
    private Texture2D texture;
    private Vector2 position;
    private Rectangle rectangle;
    private string spriteBlack;
    private string spriteRed;
    private int width;
    private int height;
    public bool isClicked;

    public Button(
      Texture2D newTexture,
      Vector2 newPosition,
      string spirteOff,
      string spriteOn,
      int newWidth,
      int newHeight)
    {
      this.texture = newTexture;
      this.position = newPosition;
      this.spriteBlack = spirteOff;
      this.spriteRed = spriteOn;
      this.width = newWidth;
      this.height = newHeight;
    }

    public void Update(ContentManager content, MouseState mouse)
    {
      this.rectangle = new Rectangle((int) this.position.X, (int) this.position.Y, this.width, this.height);
      if (new Rectangle(mouse.X, mouse.Y, 1, 1).Intersects(this.rectangle))
      {
        this.texture = content.Load<Texture2D>(this.spriteRed);
        if (mouse.LeftButton != ButtonState.Pressed)
          return;
        this.isClicked = true;
      }
      else
      {
        this.texture = content.Load<Texture2D>(this.spriteBlack);
        this.isClicked = false;
      }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(this.texture, this.rectangle, Color.White);
    }
  }
}
