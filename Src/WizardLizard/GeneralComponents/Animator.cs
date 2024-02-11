// GameManager.Animator

using Microsoft.Xna.Framework;
using System.Collections.Generic;

#nullable disable
namespace GameManager
{
  public class Animator : Component, IUpdateable
  {
    private SpriteRenderer spriteRenderer;
    private int currentIndex;
    private float timeElapsed;
    private float fps;
    private string animationName;
    private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
    private Rectangle[] rectangles;
    private GameObject gameObject;

    public int CurrentIndex
    {
      get => this.currentIndex;
      set => this.currentIndex = value;
    }

    public string AnimationName
    {
      get => this.animationName;
      set => this.animationName = value;
    }

    public Animator(GameObject gameObject)
      : base(gameObject)
    {
      this.gameObject = gameObject;
      this.animations = new Dictionary<string, Animation>();
      this.fps = 8f;
      this.spriteRenderer = (SpriteRenderer) gameObject.GetComponent("SpriteRenderer");
    }

    public void CreateAnimation(string name, Animation animation)
    {
      this.animations.Add(name, animation);
    }

    public void PlayAnimation(string animationName)
    {
      if (!(this.AnimationName != animationName))
        return;
      this.rectangles = this.animations[animationName].Rectangles;
      this.spriteRenderer.Rectangle = this.rectangles[0];
      this.spriteRenderer.Offset = this.animations[animationName].Offset;
      this.AnimationName = animationName;
      this.fps = this.animations[animationName].Fps;
      this.timeElapsed = 0.0f;
      this.CurrentIndex = 0;
    }

    public void Update()
    {
      this.timeElapsed += Game1.DeltaTime;
      this.CurrentIndex = (int) ((double) this.timeElapsed * (double) this.fps);
      if (this.CurrentIndex > this.rectangles.Length - 1)
      {
        this.GameObject.OnAnimationDone(this.AnimationName);
        this.timeElapsed = 0.0f;
        this.CurrentIndex = 0;
      }
      this.spriteRenderer.Rectangle = this.rectangles[this.CurrentIndex];
    }
  }
}
