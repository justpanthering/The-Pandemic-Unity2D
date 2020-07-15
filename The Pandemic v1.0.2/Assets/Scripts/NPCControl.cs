using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCControl : MonoBehaviour
{
    NPCTraits myTraits;
    InfectedNPCTraits _infectedTraits;
    HealthyNPCTraits _healthyTraits;

    // Start is called before the first frame update
    void Start()
    {
        //Setting Traits of infected NPCs
        if (myTraits._isInfected)
        {
            CircleCollider2D Transmitter = gameObject.transform.GetChild(0).GetComponent<CircleCollider2D>();
            Transmitter.gameObject.SetActive(true);
            Transmitter.radius = LevelManager.InfectionRadius;

            if (myTraits._isBad)//bad NPCs
                _infectedTraits = new InfectedNPCTraits(LevelManager.SpreadChance_Bi);
            else//Good NPCs
                _infectedTraits = new InfectedNPCTraits(LevelManager.SpreadChance_Gi);
        }
        //Setting Traits for healthy NPCs
        else
        {
            if (myTraits._isBad)//bad NPCs
                _healthyTraits = new HealthyNPCTraits(LevelManager.VulnerablityChance_Bn);
            else//good NPCs
                _healthyTraits = new HealthyNPCTraits(LevelManager.VulnerablityChance_Gn);
        }
           
    }

    public void Initialize()
    {
        myTraits = new NPCTraits();
    }

    public void InitializeIsBad(bool _isBad)
    {
        myTraits._isBad = _isBad;
    }
    public void InitializeIsInfected(bool _isInfected)
    {
        myTraits._isInfected = _isInfected;
    }

    public void ChangeState(bool newState)
    {
        if(newState)
        {
            CircleCollider2D Transmitter = gameObject.transform.GetChild(0).GetComponent<CircleCollider2D>();
            Transmitter.gameObject.SetActive(true);
            Transmitter.radius = LevelManager.InfectionRadius;

            if (myTraits._isBad)//bad NPCs
                _infectedTraits = new InfectedNPCTraits(LevelManager.SpreadChance_Bi);
            else//Good NPCs
                _infectedTraits = new InfectedNPCTraits(LevelManager.SpreadChance_Gi);
        }
        InitializeIsInfected(true);
        
    }

    public void Infection(float InfectorSpreadChance)
    {
        if (!myTraits._isInfected)
        {
            Debug.Log("Can be infected");
            float mySpreadChance = GetComponentInParent<InfectedNPCTraits>().spreadChance;
            float otherVulnerability = _healthyTraits.vulnerability;

            //Total Chance of other getting infectedd
            float totalChance = mySpreadChance + otherVulnerability;
            //Calculate NPC luck
            float infectionChance = Random.Range(0f, 1f);

            //Infect
            if (infectionChance >= totalChance)
            {
                Debug.Log("NPC Infected");
                ChangeState(true);
            }
            else//Do not infect
                Debug.Log("Good Luck of NPC");
        }
    }



    public float getSpreadChance()
    {
        return _infectedTraits.spreadChance;
    }
}
