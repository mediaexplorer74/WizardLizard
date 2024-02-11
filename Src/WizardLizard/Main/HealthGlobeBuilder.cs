// GameManager.HealthGlobeBuilder

using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace GameManager
{
  public class HealthGlobeBuilder : IBuilder
  {
    private GameObject gameObject;

    public void BuildGameObject(Vector2 position)
    {
      GameObject gameObject = new GameObject();
      gameObject.AddComponent((Component) new SpriteRenderer(gameObject, "HealthGlobe", 1f));
      gameObject.AddComponent((Component) new Collider(gameObject));
      gameObject.Transform.Position = position;
      gameObject.AddComponent((Component) new HealthGlobe(gameObject));
      this.gameObject = gameObject;
    }

    public void BuildGameObject(Vector2 position, string spriteName)
    {
    }

    public void BuildGameObject(Vector2 position, int frequency)
    {
    }

    public void BuildGameObject(Vector2 position, int frequency, string spriteName)
    {
      throw new NotImplementedException();
    }

    public void BuildGameObject(Vector2 position, int width, int height)
    {
    }

    public void BuildGameObject(Vector2 position, int width, int height, string creator)
    {
      throw new NotImplementedException();
    }

    public GameObject GetResult() => this.gameObject;
  }
}
