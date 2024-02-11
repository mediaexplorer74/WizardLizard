// Decompiled with JetBrains decompiler
// Type: GameManager.Director
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A0EDF889-C5AF-43C9-BC7D-E86C362A4222
// Assembly location: C:\Users\Admin\Desktop\RE\GameManager\GameManager.exe

using Microsoft.Xna.Framework;

#nullable disable
namespace GameManager
{
  internal class Director
  {
    private IBuilder builder;

    public Director(IBuilder builder) => this.builder = builder;

    public GameObject Construct(Vector2 position)
    {
      this.builder.BuildGameObject(position);
      return this.builder.GetResult();
    }

    public GameObject Construct(Vector2 position, int frequency)
    {
      this.builder.BuildGameObject(position, frequency);
      return this.builder.GetResult();
    }

    public GameObject Construct(Vector2 position, int frequency, string spriteName)
    {
      this.builder.BuildGameObject(position, frequency, spriteName);
      return this.builder.GetResult();
    }

    public GameObject Construct(Vector2 position, int width, int height)
    {
      this.builder.BuildGameObject(position, width, height);
      return this.builder.GetResult();
    }

    public GameObject Construct(Vector2 position, string spriteName)
    {
      this.builder.BuildGameObject(position, spriteName);
      return this.builder.GetResult();
    }

    public GameObject Construct(Vector2 position, int width, int height, string creator)
    {
      this.builder.BuildGameObject(position, width, height, creator);
      return this.builder.GetResult();
    }
  }
}
