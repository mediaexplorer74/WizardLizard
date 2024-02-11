// GameManager.Component


#nullable disable
namespace GameManager
{
  public abstract class Component
  {
    private GameObject gameObject;

    public Component(GameObject gameObject) => this.gameObject = gameObject;

    public Component()
    {
    }

    public GameObject GameObject
    {
      get => this.gameObject;
      private set => this.gameObject = value;
    }
  }
}
