using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryChanneling : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    private float counter = 0f;

    private float deliveryTime = 3.5f;

    public static bool isDelivering;
    public static GameObject goal;

    // Update is called once per frame
    void Update()
    {
        if (isDelivering)
        {
            gameObject.SetActive(true);
            slider.value = counter/deliveryTime;
            counter += Time.deltaTime;
            if (counter >= deliveryTime && !LevelManager.isLevelFinished)
            {
                goal.SetActive(false);
                goal = null;
                isDelivering = false;
                counter = 0f;
                AudioManager.isGoalComplete = true;
                LevelManager.no_OfActiveGoals--;
                gameObject.SetActive(false);
            }
        }
        else
        {
            counter = 0f;
            gameObject.SetActive(false);
        }
    }
}
