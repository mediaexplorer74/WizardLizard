// GameManager.Animation

using Microsoft.Xna.Framework;

#nullable disable
namespace GameManager
{
  public class Animation
  {
    private float fps;
    private Vector2 offset;
    private Rectangle[] rectangles;

    public float Fps => this.fps;

    public Vector2 Offset => this.offset;

    public Rectangle[] Rectangles => this.rectangles;

    public Animation(
      int frames,
      int yPos,
      int xStratFrame,
      int widht,
      int height,
      float fps,
      Vector2 offset)
    {
      this.rectangles = new Rectangle[frames];
      for (int index = 0; index < frames; ++index)
        this.rectangles[index] = new Rectangle(index * widht + xStratFrame, yPos, widht, height);
      this.fps = fps;
      this.offset = offset;
    }
  }
}
