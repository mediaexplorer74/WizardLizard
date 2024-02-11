// GameManager.SmallMoveableBoxBuilder

using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace GameManager
{
  public class SmallMoveableBoxBuilder : IBuilder
  {
    private GameObject gameObject;

    public void BuildGameObject(Vector2 position)
    {
      GameObject gameObject = new GameObject();
      gameObject.AddComponent((Component) new SpriteRenderer(gameObject, "Crate02", 1f));
      gameObject.Transform.Position = position;
      gameObject.AddComponent((Component) new SolidPlatform(gameObject));
      gameObject.AddComponent((Component) new MoveableBox(gameObject));
      gameObject.AddComponent((Component) new Collider(gameObject));
      this.gameObject = gameObject;
    }

    public void BuildGameObject(Vector2 position, string spriteName)
    {
      throw new NotImplementedException();
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
