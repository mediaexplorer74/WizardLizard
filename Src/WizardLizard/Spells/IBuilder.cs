// GameManager.IBuilder

using Microsoft.Xna.Framework;

#nullable disable
namespace GameManager
{
  internal interface IBuilder
  {
    GameObject GetResult();

    void BuildGameObject(Vector2 position);

    void BuildGameObject(Vector2 position, int frequency);

    void BuildGameObject(Vector2 position, int frequency, string spriteName);

    void BuildGameObject(Vector2 position, int width, int height);

    void BuildGameObject(Vector2 position, string spriteName);

    void BuildGameObject(Vector2 position, int width, int height, string creator);
  }
}
