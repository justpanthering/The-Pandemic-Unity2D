using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    //create singleton
    public static AudioManager instance;
    
    public List<Audio> AudioClips;

    public static bool isSceneChanged;
    public static bool isGoalComplete;
    private bool playedInfectedEffect;

    public void Awake()
    {
        MakeSingleton();
        InitializeVars();
        PlayBG();
    }

    private void Update()
    {
        //Change BG music after scene change
        if(isSceneChanged)
        {
            PlayBG();
            isSceneChanged = false;
            playedInfectedEffect = false;
        }

        //Check if player infected
        else if(SceneManager.GetActiveScene().name=="Level" && LevelManager.isRewindStart && !playedInfectedEffect && !LevelManager.isLevelFinished)
        {
            PlayInfected();
            
            playedInfectedEffect = true;
        }

        if(isGoalComplete)
        {
            Play("GoalComplete");
            isGoalComplete = false;
        }
    }

    void PlayInfected()
    {
        Stop("LevelTheme");
        Play("InfectedEffect");
        StartCoroutine(PlayAfterSeconds("RewindEffect", 0.5f));
        StartCoroutine(PlayAfterSeconds("Woosh", 3.5f));
    }

    IEnumerator PlayAfterSeconds(string name, float sec)
    {
        yield return new WaitForSeconds(sec);
        Play(name);
    }

    void InitializeVars()
    {
        foreach (Audio audio in AudioClips)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.clip;
            audio.source.name = audio.name;
            audio.source.volume = audio.volume;
            audio.source.loop = audio.loop;
        }
        playedInfectedEffect = false;
        isGoalComplete = false;
    }

    private void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void PlayBG()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Stop("LevelTheme");
            Play("MenuTheme");
        }
        else
        {
            Stop("MenuTheme");
            Play("LevelTheme");
        }
    }

    public void Play(string name)
    {
        Audio a =  AudioClips.Find(audio => audio.name == name);
        a.source.Play();
    }

    public void Stop(string name)
    {
        AudioClips.Find(audio => audio.name == name).source.Stop();
    }
}
