// Decompiled with JetBrains decompiler
// Type: GameManager.PlatformBuilder
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A0EDF889-C5AF-43C9-BC7D-E86C362A4222
// Assembly location: C:\Users\Admin\Desktop\RE\GameManager\GameManager.exe

using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace GameManager
{
  public class PlatformBuilder : IBuilder
  {
    private GameObject gameObject;

    public void BuildGameObject(Vector2 position)
    {
    }

    public void BuildGameObject(Vector2 position, string spriteName)
    {
      GameObject gameObject = new GameObject();
      gameObject.AddComponent((Component) new SpriteRenderer(gameObject, spriteName, 1f));
      gameObject.Transform.Position = position;
      gameObject.AddComponent((Component) new SolidPlatform(gameObject));
      gameObject.AddComponent((Component) new Collider(gameObject));
      this.gameObject = gameObject;
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
      GameObject gameObject = new GameObject();
      gameObject.Transform.Position = position;
      gameObject.AddComponent((Component) new SolidPlatform(gameObject));
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
