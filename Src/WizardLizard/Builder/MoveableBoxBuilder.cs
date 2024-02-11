// Decompiled with JetBrains decompiler
// Type: GameManager.MoveableBoxBuilder
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A0EDF889-C5AF-43C9-BC7D-E86C362A4222
// Assembly location: C:\Users\Admin\Desktop\RE\GameManager\GameManager.exe

using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace GameManager
{
  public class MoveableBoxBuilder : IBuilder
  {
    private GameObject gameObject;

    public void BuildGameObject(Vector2 position)
    {
      GameObject gameObject = new GameObject();
      gameObject.AddComponent((Component) new SpriteRenderer(gameObject, "Crate01", 1f));
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
