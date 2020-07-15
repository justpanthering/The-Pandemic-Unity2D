using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Text HighScore;
    public Animator Anim;
    private int instnCount;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.isSceneChanged = true;
        HighScore.text = "Highscore: " + PlayerPrefs.GetInt("HighScore", 0);
        instnCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level");
        Debug.Log("Loading Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        HighScore.text = "Highscore: " + PlayerPrefs.GetInt("HighScore", 0);
    }

    public void HowToPlayOpen()
    {
        Anim.SetBool("ShowInstruction", true);
    }

    public void NextInstn()
    {
        instnCount++;
        Anim.SetInteger("Instn", instnCount);
    }

    public void HowToPlayClose()
    {
        Debug.Log("Closing");
        Anim.SetBool("ShowInstruction", false);
        instnCount = 0;
        Anim.SetInteger("Instn", instnCount);
    }
}
