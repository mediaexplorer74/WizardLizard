// GameManager.NonSolidPlatformBuilder

using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace GameManager
{
  public class NonSolidPlatformBuilder : IBuilder
  {
    private GameObject gameObject;

    public void BuildGameObject(Vector2 position)
    {
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
      GameObject gameObject = new GameObject();
      gameObject.Transform.Position = position;
      gameObject.AddComponent((Component) new NonSolidPlatform(gameObject));
      gameObject.AddComponent((Component) new Collider(gameObject, width, height));
      this.gameObject = gameObject;
    }

    public void BuildGameObject(Vector2 position, int width, int height, string creator)
    {
      throw new NotImplementedException();
    }

    public GameObject GetResult() => this.gameObject;
  }
}
