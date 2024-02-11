// GameManager.NonSolidPlatform

using Microsoft.Xna.Framework.Content;

#nullable disable
namespace GameManager
{
  public class NonSolidPlatform : 
    Component,
    ILoadable,
    IUpdateable,
    IAnimateable,
    ICollisionEnter,
    ICollisionExit
  {
    private Transform transform;
    private Animator animator;

    public NonSolidPlatform(GameObject gameObject)
      : base(gameObject)
    {
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
