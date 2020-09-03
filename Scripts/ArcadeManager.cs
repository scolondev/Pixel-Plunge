using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Globalization;


//TODO LIST:
//World Themes actually having an effect on levels, this requires us to have a lot of levels in the first place.
public class ArcadeManager : MonoBehaviour
{
    #region singleton

    public static ArcadeManager instance;
    public ArcadeData arcadeData;
  
    
    // Start is called before the first frame update
    public void Awake()
    {
        instance = this;
    }
    #endregion

    //The text for when the next world is generating
    public Text nextWorldText;
    
    //All the pannels
    public GameObject[] panels;
    
    //Sprites for the gemstones
    public Sprite[] suffixSprites;

    //Progress through the current world
    public int progress = 0; 
    
    //Difficulty for scaling health
    public float difficulty = 1f;

    //The World you picked
    [HideInInspector]
    public int worldChoice = 0; 
    
    //Current displayed world
    [HideInInspector]
    public int displayedWorld = 0;

    //Dungeon GameObject for destroying and transitioning dugeons
    [HideInInspector]
    public GameObject dungeon; 

    //Finding the player for putting them in the start room
    [HideInInspector]
    public GameObject player; 

    //UI Map
    public GameObject map;
    
    //Levels in that map
    public GameObject[] levels;

    //Whether we are in a level or in the menu.
    public bool isMenu = true;
    
    //List of all the worlds
    private List<World> worlds = new List<World>();  
    private RandomHandler handler;
    
    public void Start()
    {
        handler = Entropy.instance.GetHandler("arcade");
        LoadWorlds();
    }

    #region SavingLoading
    //Saving Worlds
 
    public void SaveWorlds()
    {
        arcadeData.worldData.worlds = worlds;
        arcadeData.SaveData();
    }

    //Load the current data of all the worlds
    public void LoadWorlds()
    {
        arcadeData.ReadData();
        worlds = new List<World>(ArcadeData.instance.worldData.worlds);
        //If for some reason there's no data, instance worlds.  
        //This should ONLY happen the first time you boot up the game.
        if (worlds.Count < 1)
        {
            for (int i = 0; i < 7; i++)
            {
                worlds.Add(new World());
                handler.NewSeed();

                worlds[i].seed = handler.rand_seed;
                worlds[i] = GenerateWorld();
            }

        }

        UpdateWorlds();

        System.DateTime nextWorld = new System.DateTime(
            arcadeData.worldData.nextWorld[0],
            arcadeData.worldData.nextWorld[1],
            arcadeData.worldData.nextWorld[2],
            arcadeData.worldData.nextWorld[3],
            arcadeData.worldData.nextWorld[4],
            arcadeData.worldData.nextWorld[5]
        );

        Debug.Log(nextWorld);

        if (System.DateTime.Now > nextWorld)
        {
            Debug.Log("New World");
            AddWorld();
        }

        if (isMenu == true)
        {
            for (int i = 0; i < worlds.Count; i++)
            {
                DisplayWorld(i);
            }
            nextWorldText.text = "The next world will arrive at: " + nextWorld.ToLocalTime() + " GMT+00";
        }

        if (isMenu == false)
        {
            StartLevel();
        }
    }
    #endregion
    
    #region AddingShiftingUpdatingWorlds
    public void ShiftWorlds()
    {
        //Shift Every World's Data to the Left by 1.
        for (int i = 0; i < worlds.Count - 1; i++)
        {
            //Loading Data but shifting i + 1 to get the next one
            if (worlds[i + 1].seed != 0)
            {
                worlds[i].seed = worlds[i + 1].seed;
                handler.OverrideSeed(worlds[i].seed);
            }

            worlds[i] = GenerateWorld();

            DisplayWorld(i);
        }
    }

    //Get a level's icon
    public void GetLevelIcon(UI_Level level, int world, int levelIndex)
    {
        level.icon.sprite = worlds[world].levels[levelIndex].icon;
        level.backing.sprite = worlds[world].levels[levelIndex].backing;
    }

    //Add a New World to the Arcade
    public void UpdateWorlds()
    {
        for (int i = 0; i < worlds.Count; i++)
        {
            //Loading Data
            handler.OverrideSeed(worlds[i].seed);
            worlds[i] = GenerateWorld();
        }
    }

    //Adds a new world
    public void AddWorld()
    {
        ShiftWorlds();

        handler.NewSeed();
        worlds[worlds.Count - 1] = GenerateWorld();
        DisplayWorld(worlds.Count - 1);

        ArcadeData.instance.worldData.nextWorld = new int[] {
            System.DateTime.Now.Year,
            System.DateTime.Now.Month,
            System.DateTime.Now.AddDays(1).Day,
            System.DateTime.Now.Hour,
            System.DateTime.Now.Minute,
            System.DateTime.Now.Second
        };

        SaveWorlds();
    }
    #endregion
    
    //Potential Namings of Worlds
    private List<string> possiblePrefixes = new List<string> {
        "Flawed","Perfect","Tiny","Giant","Cracked","Shiny","Dull","Dirty","Pristine"};
    private List<string> possibleSuffixes = new List<string> {
        "Amethyst","Aquamarine","Diamond","Emerald","Turquoise","Opal","Pearl","Peridot","Ruby","Sapphire"};
    private List<string> setPieces = new List<string> {
        "None","Gelatinous","Sadness"};

    //Colors of the different gems
    private List<Color> suffixColors = new List<Color>
    {
        new Color32(202,2,242,255), //Amethyst
        new Color32(2,130,242,255), //Aquamarine
        new Color32(222,240,255,255), //Diamond 
        new Color32(66,255,82,255), //Emerald
        new Color32(66,255,170,255), //Turquoise
        new Color32(230,155,148,255), //Opal
        new Color32(247,255,222,255), //Pearl
        new Color32(129,255,107,255), //Peridot
        new Color32(252,47,40,255), //Ruby
        new Color32(40,90,252,255) //Sapphire
    };

    #region Generation
    
    //Display the World
    public void DisplayWorld(int index)
    {
        //Get Icon of Levels
        for (int i = 0; i < worlds[index].levels.Length; i++)
        {
            GetLevelIcon(levels[i].GetComponent<UI_Level>(), index, i);
        }

        UI_World myWorld = panels[index].GetComponent<UI_World>();

        myWorld.worldName.text = worlds[index].name;
        myWorld.worldBacking.color = suffixColors[possibleSuffixes.IndexOf(worlds[index].suffix)];
        myWorld.iconBacking.color = myWorld.worldBacking.color;
        myWorld.worldIcon.sprite = suffixSprites[possibleSuffixes.IndexOf(worlds[index].suffix)];

    }

    //Getting a random theming of a new world.
    public string GetRandomSetPiece()
    {
        return setPieces[handler.randomValue(0, setPieces.Count)];
    }

    public Level[] possibleLevels;
    public Level GenerateLevel(Level[] pool)
    {
        //Grab the Total
        float top = 0;
        float sum = 0;
        for (int i = 0; i < pool.Length; i++)
        {
            sum += pool[i].weight;
        }

        int rand = handler.randomValue(0, (int)sum);
   
        for(int i = 0; i < pool.Length; i++)
        {
            top += pool[i].weight;
            if(top >= rand)
            {
                return pool[i];
            }
        }
        Debug.LogError("Level Not Rolled");
        return null;
    }
    
    //Generate World
    public World GenerateWorld()
    {
        World world = new World();

        //Getting the name of this world
        int newSuffix = handler.randomValue(0, possibleSuffixes.Count);
        int newPrefix = handler.randomValue(0, possiblePrefixes.Count);

        world.prefix = possiblePrefixes[newPrefix];
        world.suffix = possibleSuffixes[newSuffix];
        world.name = world.prefix + " " + world.suffix;

        //Instancing Levels
        world.levelCount = handler.randomValue(world.minLevels, world.maxLevels);
        world.levels = new Level[world.levelCount];

        world.seed = handler.rand_seed;

        string setPiece = GetRandomSetPiece();
        world.setPiece = setPiece;

        int nextIndex = 0;
        int maxLevels = 3;

        for (int i = 0; i < world.levels.Length; i++)
        {
            //Initializing the Level
            Level newLevel = GenerateLevel(possibleLevels);
            if(setPiece != "None")
            {
                while (newLevel.setpiece != world.setPiece || newLevel.setpiece_index != nextIndex)
                {
                    newLevel = GenerateLevel(possibleLevels);
                }
            }

            nextIndex += 1;
            if(nextIndex >= maxLevels)
            {
                nextIndex = 0;
            }

            world.levels[i] = newLevel;
        }

        return world;
    }
    #endregion
    public void UpdateDifficulty()
    {
        difficulty = ((progress) * 0.3f) + 1f;
    }

    #region WorldLevels
    //Starting a World
    public void StartWorld()
    {
        worldChoice = displayedWorld;
        progress = 0;

        PlayerPrefs.SetInt("arcade_choice", worldChoice);

        SceneManager.LoadScene("Arcade");
    }

    //Starting a level
    public void StartLevel()
    {
        worldChoice = PlayerPrefs.GetInt("arcade_choice", 0);
        NextWorldLevel();
    }

    //When you complete a level
    public void CompleteWorldLevel()
    {
        progress += 1;
        UpdateDifficulty();
        StartLevel();
    }

    //Progress to the next World Level, by destroying the dungeon object and creating a new one, we don't have to load another scene.
    //Preventing headache from having to save more data.
    public void NextWorldLevel()
    {
        dungeon = GameObject.FindGameObjectWithTag("dungeon").gameObject;
        if (dungeon)
        {
            Destroy(dungeon.gameObject);
        }


        player = GameObject.FindGameObjectWithTag("player").gameObject;


        GameObject newLevel = Instantiate(worlds[worldChoice].levels[progress].prefab, transform.position, transform.rotation);

        StartCoroutine("SpawnPlayer", 0.5f);

    
       
    }

    public IEnumerator SpawnPlayer(float time)
    {
        yield return new WaitForSeconds(time);
        if (GameObject.FindGameObjectWithTag("dungeon_start"))
        {
            GameManager.instance.levelStart();
        }
    }
    #endregion
    
    #region WorldMap
    //Displaying the World Map
    public void DisplayWorldMap(int slot)
    {
        displayedWorld = slot;
        for (int i = 0; i < levels.Length; i++)
        {
           // levels[i].name = worlds[slot].levels[i].name;
            UI_Level ui = levels[i].GetComponent<UI_Level>();
            ui._name.text = worlds[slot].levels[i].name;
            ui.description.text = worlds[slot].levels[i].description;
            ui.icon.sprite = worlds[slot].levels[i].icon;
            ui.backing.color = worlds[slot].levels[i].backing_color; 
        }

        map.SetActive(true);
    }
    
    //Hiding the World Map
    public void HideWorldMap()
    {
        displayedWorld = 0;
        map.SetActive(false);
    }
    
    #endregion
}
