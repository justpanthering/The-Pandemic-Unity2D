using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class NPCMovement : MonoBehaviour
{
    public Transform Destinations;
    private List<Transform> DestList;
    private Transform CurrDest;

    public Vector2 speed;

    public AIPath aiPath;

    public Vector2 aiState;

    [SerializeField]
    private Animator Anim;
    // Start is called before the first frame update
    void Start()
    {
        speed = new Vector2(-1f, 0);
        aiState = new Vector2(0, 0);
        SetDestination();
    }

    void SetDestination()
    {
        Destinations = GameObject.FindGameObjectWithTag("Destinations").transform;
        DestList = new List<Transform>();
        foreach(Transform child in Destinations)
        {
            DestList.Add(child);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.isLevelFinished)
            MoveNPC();
        else
        {
            if(LevelManager.isRewindOver)
            {
                transform.parent.GetComponent<Seeker>().enabled = false;
                transform.parent.GetComponent<AIDestinationSetter>().enabled = false;
                transform.parent.GetComponent<AIPath>().enabled = false;
                Anim.SetBool("IsWalking", false);
            }
            
        }
            
    }

    void MoveNPC()
    {
        //Set Movement Animation
        if (aiPath.desiredVelocity.x > 0 || aiPath.desiredVelocity.x < 0 || aiPath.desiredVelocity.y > 0 || aiPath.desiredVelocity.y < 0)
        {
            Anim.SetBool("IsWalking", true);
            Anim.SetFloat("Horizontal", aiPath.desiredVelocity.x);
            Anim.SetFloat("Vertical", aiPath.desiredVelocity.y);
            aiState.x = aiPath.desiredVelocity.x;
            aiState.y = aiPath.desiredVelocity.y;
        }
        else
        {
            Anim.SetBool("IsWalking", false);
            Anim.SetFloat("Horizontal", aiState.x);
            Anim.SetFloat("Vertical", aiState.y);
        }

        CheckCurrDest();
    }

    void CheckCurrDest()
    {
        if(CurrDest==null || transform.parent.position==CurrDest.position || (aiPath.desiredVelocity.x== 0f && aiPath.desiredVelocity.y==0f))
        {
            //Set New Destination
            int i = Random.Range(0, DestList.Count);
            CurrDest = DestList[i];
            transform.parent.GetComponent<AIDestinationSetter>().target = CurrDest;
        }
    }
}
