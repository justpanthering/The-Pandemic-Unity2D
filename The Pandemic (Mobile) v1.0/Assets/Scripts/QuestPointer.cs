using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPointer : MonoBehaviour
{
    [SerializeField]
    private Camera uiCamera;

    [SerializeField]
    private GameObject start;
    [SerializeField]
    private GameObject goal;
    [SerializeField]
    private RectTransform pointer;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float screenOffsetPix = 5f;

    public static int status = 0;

    private GameObject compass;

    // Start is called before the first frame update
    void Start()
    {
        compass = GameObject.FindGameObjectWithTag("Compass");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 objective;
        if (status == 0)
        {
            objective = start.transform.position;
        }
        else
        {
            objective = goal.transform.position;
        }
        Vector3 direction = (objective - player.transform.position).normalized;
        float angle = Vector3.Angle(Vector3.up, direction);

        //Set rotation of pointer
        pointer.localEulerAngles = new Vector3(0, 0, angle) * Mathf.Sign(Vector3.Cross(Vector3.up, direction).z);

        //Find position of objective wrt screen
        Vector3 objective_sc = Camera.main.WorldToScreenPoint(objective);
        //check if the objective is (to the left of) or (to the right of) or (below) or (above) the screen
        bool isOffScreen = objective_sc.x < 0f || objective_sc.x > Screen.width || objective_sc.y < 0f || objective_sc.y > Screen.height;

        if (isOffScreen)
        {
            compass.SetActive(true);

            Vector3 cappedTargetScreenPosition = objective_sc;
            //if(to the left, then x fixed to zero)
            if (objective_sc.x < 0)
                cappedTargetScreenPosition.x = 0f + screenOffsetPix;
            //if to the right, the x fixed to screen width
            if (objective_sc.x > Screen.width)
                cappedTargetScreenPosition.x = Screen.width - screenOffsetPix;
            //if below, then y set to 0
            if (objective_sc.y < 0)
                cappedTargetScreenPosition.y = 0f + screenOffsetPix;
            //if above, then y set to height
            if (objective_sc.y > Screen.height)
                cappedTargetScreenPosition.y = Screen.height - screenOffsetPix;
            //else the position is the same as that of the objective

            //place it on the world from uiCamera POV
            Vector3 pointerScreenPos = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);

            pointer.position = new Vector3(pointerScreenPos.x, pointerScreenPos.y, 0f);
        }
        else
        {
            GameObject compass = GameObject.FindGameObjectWithTag("Compass");
            if(compass!=null)
                compass.SetActive(false);
        }

    }
}
