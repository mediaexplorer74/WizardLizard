// GameManager.Aimer

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

#nullable disable
namespace GameManager
{
  public class Aimer : Component, IUpdateable, ILoadable
  {
    private Transform transform;
    private Animator animator;

    public Aimer(GameObject gameObject)
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
      MouseState state = Mouse.GetState();
      this.transform.Position = new Vector2((float) state.X, (float) state.Y);
    }
  }
}
