using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static int highscore;
    public static int no_of_Goals;
    public static float timeLimit;
    public static int Population;
    public static int no_of_good;
    public static int no_of_bad;
    public static int Infected;

    public static void CreateLevel()
    {
        highscore = PlayerPrefs.GetInt("HighScore", 0);

        if (highscore == 0)
        {
            no_of_Goals = 3;
            timeLimit = no_of_Goals * 20f;
            Population = no_of_Goals * 10;
            no_of_good = Population / 2;
            no_of_bad = Population / 2;
            Infected = Population / 2;
        }
        else
        {
            no_of_Goals = highscore + 2;
            timeLimit = no_of_Goals * 20f;
            Population = no_of_Goals * 10;
            no_of_good = Population / 2;
            no_of_bad = Population / 2;
            Infected = Population / 2;
            Debug.Log(no_of_Goals +" "+ Population +" "+ Infected);
        }

        LevelManager.GoalsCount = no_of_Goals;
        LevelManager._no_of_good = no_of_good;
        LevelManager._no_of_bad = no_of_bad;
        LevelManager.InfectedCount = Infected;
        LevelManager.Timer = timeLimit;
    }

    public static void UpdateGame()
    {
        
        if(no_of_Goals > highscore)
        {
            highscore = no_of_Goals;
            Debug.Log("Saving game with highscore: " + no_of_Goals);
            PlayerPrefs.SetInt("HighScore", no_of_Goals);
        }
            
    }

    void Awake()
    {
        MakeSingleton();
        highscore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void Start()
    {
        highscore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void MakeSingleton()
    {
        if(instance!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
