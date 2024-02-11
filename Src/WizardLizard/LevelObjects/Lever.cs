// GameManager.Lever

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

#nullable disable
namespace GameManager
{
  public class Lever : 
    Component,
    ILoadable,
    IUpdateable,
    IAnimateable,
    ICollisionEnter,
    ICollisionExit
  {
    private SoundEffect leverSound;
    private SoundEffect magicDoorSound;
    private Transform transform;
    private Animator animator;
    private int frequency;

    public int Frequency
    {
      get => this.frequency;
      set => this.frequency = value;
    }

    public Lever(GameObject gameObject, int frequency)
      : base(gameObject)
    {
      this.Frequency = frequency;
      this.animator = (Animator) this.GameObject.GetComponent("Animator");
      this.transform = gameObject.Transform;
    }

    public void LoadContent(ContentManager content)
    {
      this.leverSound = content.Load<SoundEffect>("LeverUsed");
      this.magicDoorSound = content.Load<SoundEffect>("MagicDoorSound");
      this.CreateAnimations();
    }

    public void CreateAnimations()
    {
      this.animator.CreateAnimation("Idle", new Animation(1, 0, 0, 150, 52, 1f, Vector2.Zero));
      this.animator.CreateAnimation("Activated", new Animation(4, 0, 0, 150, 52, 8f, Vector2.Zero));
      this.animator.PlayAnimation("Idle");
    }

    public void Update()
    {
    }

    public void Interaction(GameObject target)
    {
      Collider component = (Collider) target.GetComponent("Collider");
      if (!((Collider) this.GameObject.GetComponent("Collider")).CollisionBox.Intersects(component.CollisionBox))
        return;
      if (this.frequency > 50)
      {
        if (!Game1.spawnList.ContainsKey(this.frequency))
          return;
        this.animator.PlayAnimation("Activated");
        this.leverSound.Play();
        Game1.ObjectToAdd.Add(Game1.spawnList[this.frequency]);
        Game1.spawnList.Remove(this.frequency);
      }
      else
      {
        foreach (GameObject gameObject in Game1.GameObjects)
        {
          if (gameObject.GetComponent("Door") != null && ((Door) gameObject.GetComponent("Door")).Frequency == this.Frequency)
          {
            this.animator.PlayAnimation("Activated");
            this.magicDoorSound.Play();
            this.leverSound.Play();
            Game1.ObjectsToRemove.Add(gameObject);
          }
        }
      }
    }

    public void OnAnimationDone(string animationName) => this.animator.PlayAnimation("Idle");

    public void OnCollisionEnter(Collider other)
    {
    }

    public void OnCollisionExit(Collider other)
    {
    }
  }
}
