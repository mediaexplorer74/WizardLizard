// GameManager.HealthGlobe

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

#nullable disable
namespace GameManager
{
  public class HealthGlobe : Component, IUpdateable, ICollisionEnter, ILoadable
  {
    private SoundEffect healthUpSound;
    private Transform transform;
    private Animator animator;
    private bool canHeal;

    public HealthGlobe(GameObject gameObject)
      : base(gameObject)
    {
      this.animator = (Animator) this.GameObject.GetComponent("Animator");
      this.transform = gameObject.Transform;
      this.canHeal = true;
    }

    public void LoadContent(ContentManager content)
    {
      this.healthUpSound = content.Load<SoundEffect>("HealthUpSound");
    }

    public void OnCollisionEnter(Collider other)
    {
      if (other.GameObject.GetComponent("Player") == null || !this.canHeal)
        return;
      this.canHeal = false;
      ++Player.Health;
      this.healthUpSound.Play();
      Game1.Instance.RemoveGameObject(this.GameObject);
    }

    public void Update() => this.canHeal = true;
  }
}
