// GameManager.Door

using Microsoft.Xna.Framework.Content;

#nullable disable
namespace GameManager
{
  public class Door : 
    Component,
    ILoadable,
    IUpdateable,
    IAnimateable,
    ICollisionEnter,
    ICollisionExit
  {
    private Animator animator;
    private Transform transform;
    private int frequency;

    public int Frequency
    {
      get => this.frequency;
      set => this.frequency = value;
    }

    public Door(GameObject gameObject, int frequency)
      : base(gameObject)
    {
      this.Frequency = frequency;
      this.animator = (Animator) this.GameObject.GetComponent("Animator");
      this.transform = gameObject.Transform;
    }

    public void LoadContent(ContentManager content)
    {
    }

    public void Update()
    {
    }

    public void OnAnimationDone(string animationName)
    {
    }

    public void OnCollisionEnter(Collider other)
    {
    }

    public void OnCollisionExit(Collider other)
    {
    }
  }
}
