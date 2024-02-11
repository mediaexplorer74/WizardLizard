// GameManager.ILoadable

using Microsoft.Xna.Framework.Content;

#nullable disable
namespace GameManager
{
  internal interface ILoadable
  {
    void LoadContent(ContentManager content);
  }
}
