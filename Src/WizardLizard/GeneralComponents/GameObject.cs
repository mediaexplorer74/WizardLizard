// GameManager.GameObject

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

#nullable disable
namespace GameManager
{
  public class GameObject : Component, IAnimateable
  {
    private Transform transform;
    private List<Component> components = new List<Component>();

    internal Transform Transform => this.transform;

    public List<Component> Components
    {
      get => this.components;
      set => this.components = value;
    }

    public GameObject()
    {
      this.transform = new Transform(this, Vector2.Zero);
      this.AddComponent((Component) this.transform);
    }

    public void AddComponent(Component component) => this.components.Add(component);

    public Component GetComponent(string component)
    {
      foreach (Component component1 in this.components)
      {
        if (component1.GetType().Name == component)
          return component1;
      }
      return (Component) null;
    }

    public T GetComponent<T>() where T : Component
    {
      T component1 = default (T);
      foreach (Component component2 in this.components)
      {
        if (component2 is T component3)
          return component3;
      }
      return component1;
    }

    public void LoadContent(ContentManager content)
    {
      foreach (Component component in this.components)
      {
        if (component is ILoadable)
          (component as ILoadable).LoadContent(content);
      }
    }

    public void Update()
    {
      foreach (Component component in this.components)
      {
        if (component is IUpdateable)
          (component as IUpdateable).Update();
      }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      foreach (Component component in this.components)
      {
        if (component is IDrawable)
          (component as IDrawable).Draw(spriteBatch);
      }
    }

    public void OnAnimationDone(string animationName)
    {
      foreach (Component component in this.components)
      {
        if (component is IAnimateable)
          (component as IAnimateable).OnAnimationDone(animationName);
      }
    }

    public void OnCollisionEnter(Collider other)
    {
      foreach (Component component in this.components)
      {
        if (component is ICollisionEnter)
          (component as ICollisionEnter).OnCollisionEnter(other);
      }
    }

    public void OnCollisionExit(Collider other)
    {
      foreach (Component component in this.components)
      {
        if (component is ICollisionExit)
          (component as ICollisionExit).OnCollisionExit(other);
      }
    }
  }
}
