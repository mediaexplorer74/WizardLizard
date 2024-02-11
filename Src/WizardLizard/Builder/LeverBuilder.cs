// Decompiled with JetBrains decompiler
// Type: GameManager.LeverBuilder
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A0EDF889-C5AF-43C9-BC7D-E86C362A4222
// Assembly location: C:\Users\Admin\Desktop\RE\GameManager\GameManager.exe

using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace GameManager
{
  public class LeverBuilder : IBuilder
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
      GameObject gameObject = new GameObject();
      gameObject.AddComponent((Component) new SpriteRenderer(gameObject, "LeverSpriteSheet", 1f));
      gameObject.AddComponent((Component) new Animator(gameObject));
      gameObject.AddComponent((Component) new Collider(gameObject));
      gameObject.Transform.Position = position;
      gameObject.AddComponent((Component) new Lever(gameObject, frequency));
      this.gameObject = gameObject;
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
