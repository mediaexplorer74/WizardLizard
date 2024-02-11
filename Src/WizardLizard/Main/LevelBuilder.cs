// GameManager.LevelBuilder

using Microsoft.Xna.Framework;
using System.Collections.Generic;

#nullable disable
namespace GameManager
{
  public class LevelBuilder
  {
    private Director director;
    private List<GameObject> gameObjects;
    private Dictionary<int, GameObject> spawnList;

    public LevelBuilder()
    {
      this.gameObjects = Game1.GameObjects;
      this.spawnList = Game1.spawnList;
    }

    public void LevelOne()
    {
      GameObject gameObject = new GameObject();
      gameObject.AddComponent((Component) new SpriteRenderer(gameObject, "TutorialScreen", 1f));
      gameObject.Transform.Position = new Vector2(0.0f, 0.0f);
      Game1.ObjectToAdd.Add(gameObject);
      this.director = new Director((IBuilder) new AimerBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(0.0f, 0.0f)));
      this.director = new Director((IBuilder) new PlayerBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(100f, 675f)));
      this.director = new Director((IBuilder) new PlayerHealthBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(10f, 10f)));
      this.director = new Director((IBuilder) new CompanionBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(100f, 675f)));
      this.director = new Director((IBuilder) new PlatformBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(0.0f, 850f), 1600, 100));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(0.0f, 0.0f), 1288, 48));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(0.0f, 0.0f), 27, 850));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(221f, 183f), 28, 700));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(452f, 48f), 28, 634));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(479f, 304f), 95, 48));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(695f, 632f), 30, 300));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(695f, 632f), 518, 48));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1185f, 632f), 28, 156));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(988f, 768f), 28, 156));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1570f, 0.0f), 30, 672));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1373f, 672f), 250, 50));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1342f, 185f), 30, 126));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1342f, 311f), 300, 48));
      this.director = new Director((IBuilder) new NonSolidPlatformBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(26f, 700f), 193, 20));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(26f, 555f), 193, 20));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(26f, 405f), 193, 20));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(26f, (float) byte.MaxValue), 193, 20));
      this.director = new Director((IBuilder) new MoveableBoxBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(435f, 675f)));
      this.director = new Director((IBuilder) new DoorBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1375f, 705f), 1, "MagicDoor30x150"));
      this.director = new Director((IBuilder) new LeverBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(780f, 800f), 1));
      this.director = new Director((IBuilder) new ArcherBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(490f, 120f)));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1450f, 120f)));
      this.director = new Director((IBuilder) new GoblinBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1100f, 475f)));
    }

    public void LevelTwo()
    {
      GameObject gameObject = new GameObject();
      gameObject.AddComponent((Component) new SpriteRenderer(gameObject, "Level01", 1f));
      gameObject.Transform.Position = new Vector2(0.0f, 0.0f);
      Game1.ObjectToAdd.Add(gameObject);
      this.director = new Director((IBuilder) new AimerBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(0.0f, 0.0f)));
      this.director = new Director((IBuilder) new PlayerBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(100f, 675f)));
      this.director = new Director((IBuilder) new PlayerHealthBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(10f, 10f)));
      this.director = new Director((IBuilder) new LeverBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(70f, 270f), 1));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1160f, 143f), 51));
      this.director = new Director((IBuilder) new DoorBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(970f, 655f), 1));
      this.director = new Director((IBuilder) new CompanionBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(100f, 675f)));
      this.director = new Director((IBuilder) new ArcherBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(315f, 375f)));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1360f, 275f)));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1150f, 100f)));
      this.director = new Director((IBuilder) new GoblinBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1183f, 460f)));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(680f, 705f)));
      this.director = new Director((IBuilder) new OrcBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1300f, 675f)));
      this.director = new Director((IBuilder) new MoveableBoxBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(800f, 720f)));
      this.director = new Director((IBuilder) new PlatformBuilder());

      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(0.0f, 850f), 1600, 100));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(0.0f, 0.0f), 60, 850));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(966f, 608f), 386, 48));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1352f, 560f), 194, 48));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1498f, 240f), 48, 480));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1546f, 240f), 50, 50));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1596f, 0.0f), 50, 240));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(190f, 488f), 196, 48));
      
            this.spawnList.Add(51, this.director.Construct(new Vector2(482f, 535f), "MagicPlatform"));
      this.director = new Director((IBuilder) new NonSolidPlatformBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1350f, 240f), 148, 40));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1150f, 195f), 200, 45));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1360f, 395f), 138, 30));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(60f, 320f), 178, 30));
    }

    public void LevelThree()
    {
      GameObject gameObject = new GameObject();
      gameObject.AddComponent((Component) new SpriteRenderer(gameObject, "Level02New", 1f));
      gameObject.Transform.Position = new Vector2(0.0f, 0.0f);
      Game1.ObjectToAdd.Add(gameObject);
      this.director = new Director((IBuilder) new AimerBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(2f, 50f)));
      this.director = new Director((IBuilder) new PlayerBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(2f, 20f)));
      this.director = new Director((IBuilder) new CompanionBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(2f, 20f)));
      this.director = new Director((IBuilder) new PlayerHealthBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(10f, 10f)));
      this.director = new Director((IBuilder) new PlatformBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(0.0f, 0.0f), 1600, 48));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(0.0f, 850f), 1600, 50));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(0.0f, 150f), 548, 50));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(0.0f, 200f), 50, 650));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1550f, 48f), 50, 650));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1350f, 200f), 200, 50));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1350f, 250f), 50, 100));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(300f, 400f), 1048, 50));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(350f, 300f), 50, 50));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(350f, 350f), 100, 50));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(900f, 350f), 100, 50));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(950f, 550f), 250, 50));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1150f, 450f), 50, 100));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(50f, 500f), 150, 200));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(200f, 600f), 50, 50));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(200f, 650f), 150, 50));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(300f, 700f), 100, 50));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(350f, 750f), 100, 50));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(50f, 800f), 200, 50));
      this.director = new Director((IBuilder) new MoveableBoxBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1046f, 730f)));
      this.director = new Director((IBuilder) new DoorBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(320f, 200f), 1));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(420f, 800f), 2, "MagicDoor30x50"));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1550f, 700f), 3, "MagicDoor30x150"));
      this.director = new Director((IBuilder) new LeverBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(50f, 750f), 1));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1400f, 150f), 2));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1000f, 500f), 3));
      this.director = new Director((IBuilder) new ArcherBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1350f, 125f)));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(950f, 480f)));
      this.director = new Director((IBuilder) new GoblinBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(970f, 780f)));
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(100f, 425f)));
      this.director = new Director((IBuilder) new OrcBuilder());
      Game1.ObjectToAdd.Add(this.director.Construct(new Vector2(1300f, 451f)));
    }
  }
}
