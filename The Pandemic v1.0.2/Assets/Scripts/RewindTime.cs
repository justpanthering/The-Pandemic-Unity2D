using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindTime : MonoBehaviour
{
    private bool isRecording;
    private bool isRewinding;

    private float recordTime = 2.25f;
    private float recordTimer;
    private float animTime = 0.5f;

    private List<Vector3> recorder;

    // Start is called before the first frame update
    void Start()
    {
        isRecording = false;
        isRewinding = false;
        recordTimer = recordTime;
        recorder = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelManager.isPlayerInfected && !isRewinding)
        {
            isRecording = true;
        }

        if (isRecording)
            Record();
        else if (isRewinding)
            Rewind();
    }

    void Record()
    {
        if(recordTimer>0)
        {
            recordTimer -= Time.deltaTime;
            recorder.Add(transform.position);
            Debug.Log("Now Recording");
        }
        else
        {
            isRecording = false;
            isRewinding = true;
        }

    }

    void Rewind()
    {
        LevelManager.isRewindStart = true;
        //Wait for Infected Effect Animation to play
        if (animTime>0)
        {
            animTime -= Time.deltaTime;
        }
        else
        {
            if (recordTimer < recordTime)
            {
                Debug.Log("Now Playing");
                recordTimer += Time.deltaTime;
                if (recorder.Count != 0)
                {
                    transform.position = recorder[recorder.Count - 1];
                    recorder.RemoveAt(recorder.Count - 1);
                }
            }
            else if (isRewinding && LevelManager.isPlayerInfected)
            {
                Debug.Log("Play Over");
                isRewinding = false;
                LevelManager.isRewindOver = true;
            }
        }
    }
}
