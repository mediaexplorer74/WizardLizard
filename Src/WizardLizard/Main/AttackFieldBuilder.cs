// GameManager.AttackFieldBuilder

using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace GameManager
{
  public class AttackFieldBuilder : IBuilder
  {
    private GameObject gameObject;

    public void BuildGameObject(Vector2 position) => throw new NotImplementedException();

    public void BuildGameObject(Vector2 position, string spriteName)
    {
      throw new NotImplementedException();
    }

    public void BuildGameObject(Vector2 position, int frequency)
    {
      throw new NotImplementedException();
    }

    public void BuildGameObject(Vector2 position, int frequency, string spriteName)
    {
      throw new NotImplementedException();
    }

    public void BuildGameObject(Vector2 position, int width, int height)
    {
      throw new NotImplementedException();
    }

    public void BuildGameObject(Vector2 position, int width, int height, string creator)
    {
      GameObject gameObject = new GameObject();
      gameObject.Transform.Position = position;
      gameObject.AddComponent((Component) new AttackField(gameObject, creator));
      gameObject.AddComponent((Component) new Collider(gameObject, width, height));
      this.gameObject = gameObject;
    }

    public GameObject GetResult() => this.gameObject;
  }
}
