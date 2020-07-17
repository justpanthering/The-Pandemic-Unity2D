using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    //private bool channeling;
    //private float counter;

    // Start is called before the first frame update
    void Start()
    {
        //channeling = false;
        //counter = 0f;
    }

    // Update is called once per frame
    void Update()
    {/*
        if(channeling && isActive)
        {
            counter += Time.deltaTime;
            if(counter >=5f)
            {
                isActive = false;
                gameObject.SetActive(false);
                LevelManager.no_OfActiveGoals--;
            }
        }
        else
        {
            counter = 0;
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Trigger Enter");
            LevelManager.Del_LoadBar.SetActive(true);
            DeliveryChanneling.goal = gameObject;
            DeliveryChanneling.isDelivering = true;
            //channeling = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Trigger Exit");
            DeliveryChanneling.isDelivering = false;
            //channeling = false;
        }
    }
}
