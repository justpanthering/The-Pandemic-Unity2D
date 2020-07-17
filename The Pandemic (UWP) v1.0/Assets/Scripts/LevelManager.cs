using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private Vector2 StartPos;

    public static bool isPlayerInfected;

    public static int _no_of_bad;
    public static int _no_of_good;
    public static int InfectedCount;
    public static float InfectionRadius = 0.18f;
    public static float SpreadChance_Gi = 0.2f;
    public static float SpreadChance_Bi = 0.7f;
    public static float VulnerablityChance_Gn = 0.12f;
    public static float VulnerablityChance_Bn = 0.2f;

    public static int GoalsCount;
    public static int no_OfActiveGoals;

    [SerializeField]
    private Transform GoalSet_1;
    [SerializeField]
    private Transform GoalSet_2;
    [SerializeField]
    private Transform GoalSet_3;
    [SerializeField]
    private Transform GoalSet_4;
    [SerializeField]
    private Transform GoalSet_5;
    [SerializeField]
    private List<Transform> GoalSets;
    [SerializeField]
    private GameObject DeliveryLoadingBar;

    //UI Elements
    [SerializeField]
    private GameObject gameStats;
    private Text[] gameStatsList;
    [SerializeField]
    private GameObject objectiveStats;
    [SerializeField]
    private Text TimerText;
    [SerializeField]
    private Animator Anim;
    [SerializeField]
    private Animator CanvasAnim;

    public static GameObject Del_LoadBar;

    public static bool isRewindStart;
    public static bool isRewindOver;
    private bool isWon;
    private bool isGameOver;
    private bool isTimeUp;
    private bool isGameUpdated;

    public static bool isLevelFinished;

    
    public static float Timer;

    private void Awake()
    {
        InitializeVars();
    }

    // Start is called before the first frame update
    void Start()
    {
        //InitializeVars();
        InitializeGoals();
        SetMission();
        DeliveryBarInitialize();
        StatsUIInitialize();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGameStatus();
        
        if (!isGameOver && !isWon)
        {
            UpdateTimer();
            UpdateUI();
        }
        else
        {
            CheckAndPlayAnim();
        }
    }

    void CheckAndPlayAnim()
    {

        if (isGameOver && isTimeUp)
        {
            CanvasAnim.SetBool("isLevelOver", true);
            Anim.SetBool("isGameOver", true);
            Anim.SetBool("isTimeOut", true);
        }
        else if (isPlayerInfected && isGameOver)
        {
            if (isRewindStart && !isRewindOver)
            {
                CanvasAnim.SetBool("isLevelOver", true);
                Anim.SetBool("isPlayerInfected", true);
            }
                
            else if(isRewindOver)
            {
                CanvasAnim.SetBool("isLevelOver", true);
                Anim.SetBool("isRewindOver", true);
            }
        }
        else if (isWon)
        {
            CanvasAnim.SetBool("isLevelOver", true);
            Anim.SetBool("isGameOver", true);
            Anim.SetBool("isWon", true);
        }
    }

    void CheckGameStatus()
    {
        if (no_OfActiveGoals == 0)
            isWon = true;

        if(isGameOver || isWon)
        {
            isLevelFinished = true;
            if(isWon && !isGameUpdated)
            {
                GameManager.UpdateGame();
                isGameUpdated = true;
            }
                
        }

        if (isPlayerInfected)
        {
            //play rewind
            if (isRewindStart)
                isGameOver = true;
            //Game Over
            if (isRewindOver)
            {
                Debug.Log("Rewind and Game Over");
            }
        }
    }

    void InitializeVars()
    {
        isWon = false;
        isGameOver = false;
        isLevelFinished = false;
        isPlayerInfected = false;
        isRewindStart = false;
        isRewindOver = false;
        isTimeUp = false;
        isGameUpdated = false;
        AudioManager.isSceneChanged = true;

        GameManager.CreateLevel();
    }

    void UpdateTimer()
    {
        Timer -= Time.deltaTime;
        if(Timer <= 0f)
        {
            isTimeUp = true;
            isGameOver = true;
        }
    }

    void UpdateUI()
    {
        objectiveStats.GetComponentInChildren<Text>().text = "Deliveries Remaining: " + no_OfActiveGoals;
        
        TimerText.text = Mathf.Floor(Timer/60).ToString("00") + ":" + (Timer%60).ToString("00");
        if(Timer <= 10f)
        {
            TimerText.color = Color.red;
        }
    }

    void StatsUIInitialize()
    {
        objectiveStats.GetComponentInChildren<Text>().text = "Deliveries Remaining: " + no_OfActiveGoals;

        gameStatsList = new Text[2];
        gameStatsList = gameStats.GetComponentsInChildren<Text>();
        gameStatsList[1].text = "Total Population: " + ((int)_no_of_good + (int)_no_of_bad);
        gameStatsList[2].text = "No of Infected: " + InfectedCount;
    }

    public void DeliveryBarInitialize()
    {
        Del_LoadBar = DeliveryLoadingBar;
        Del_LoadBar.SetActive(false);
    }

    void InitializeGoals()
    {
        no_OfActiveGoals = 0;
        SetGoals();
    }

    void SetGoals()
    {
        List<List<Transform>> GoalLists = new List<List<Transform>>();

        //initialize list
        foreach (var child in GoalSets.Select((value, index) => new { value, index }))
        {
            GoalLists.Add(new List<Transform>());
            foreach(Transform child1 in child.value)
            {
                GoalLists[child.index].Add(child1);
            }
        }

        List<Vector2> GoalSelectList = new List<Vector2>();
        //selectgoals
        for (int i = 0; i < GoalsCount; i++)
        {
            bool isSelected = false;

            do
            {
                Vector2 selectgoal = new Vector2();
                selectgoal.x = Random.Range(0, GoalLists.Count);
                selectgoal.y = Random.Range(0, GoalLists[(int)selectgoal.x].Count);

                if(!GoalSelectList.Contains(selectgoal))
                {
                    GoalSelectList.Add(selectgoal);
                    //set active
                    GoalLists[(int)selectgoal.x][(int)selectgoal.y].gameObject.SetActive(true);
                    no_OfActiveGoals++;
                    isSelected = true;
                }
            } while (!isSelected);
            
        }
    }


    void SetMission()
    {
        StartPos = new Vector2(-20, 35);
    }

    public void PlayAgainClick()
    {
        SceneManager.LoadScene("Level");
    }

    public void MainMenuClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevelClick()
    {
        GameManager.CreateLevel();
        SceneManager.LoadScene("Level");
        
    }

    public void PauseClick()
    {
        Debug.Log("Pausing game");
        Time.timeScale = 0;
        CanvasAnim.SetBool("isGamePaused", true);
    }

    public void ResumeClick()
    {
        Time.timeScale = 1;
        CanvasAnim.SetBool("isGamePaused", false);
    }
}
