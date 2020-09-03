using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    public void Awake()
    {
        instance = this;
    }
    #endregion

    public delegate void LevelUpdate();

    //Everything that's called when the level starts and ends.
    public LevelUpdate levelStart;
    public LevelUpdate levelEnd;

    private ArcadeManager arcadeManager;
    public void Start()
    {
        //References
        Application.targetFrameRate = 60;
        arcadeManager = ArcadeManager.instance;

        levelStart += new LevelUpdate(StartLevel);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void StartLevel()
    {
        GameObject player = GameObject.FindGameObjectWithTag("player");
        Transform startPoint = GameObject.FindGameObjectWithTag("dungeon_start").transform;

        player.transform.position = new Vector2(startPoint.position.x, startPoint.position.y);
    }

    public void CompleteLevel()
    {
        arcadeManager.CompleteWorldLevel();
    }
}
