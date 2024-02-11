// GameManager.Transform

using Microsoft.Xna.Framework;

#nullable disable
namespace GameManager
{
  public class Transform : Component
  {
    private Vector2 position;

    public Vector2 Position
    {
      get => this.position;
      set => this.position = value;
    }

    public Transform(GameObject gameObject, Vector2 position)
      : base(gameObject)
    {
      this.position = position;
    }

    public void Translate(Vector2 translation) => this.position += translation;
  }
}
