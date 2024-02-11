// GameManager.AimerBuilder

using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace GameManager
{
  public class AimerBuilder : IBuilder
  {
    private GameObject gameObject;

    public void BuildGameObject(Vector2 position)
    {
      GameObject gameObject = new GameObject();
      gameObject.AddComponent((Component) new SpriteRenderer(gameObject, "Aim", 1f));
      gameObject.Transform.Position = new Vector2();
      gameObject.AddComponent((Component) new Aimer(gameObject));
      this.gameObject = gameObject;
    }

    public void BuildGameObject(Vector2 position, string spriteName)
    {
      //throw new NotImplementedException();
    }

    public void BuildGameObject(Vector2 position, int frequency)
    {
      //throw new NotImplementedException();
    }

    public void BuildGameObject(Vector2 position, int frequency, string spriteName)
    {
      //throw new NotImplementedException();
    }

    public void BuildGameObject(Vector2 position, int width, int height)
    {
      //throw new NotImplementedException();
    }

    public void BuildGameObject(Vector2 position, int width, int height, string creator)
    {
      //throw new NotImplementedException();
    }

    public GameObject GetResult() => this.gameObject;
  }
}
