using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private PlayerTraits _playerTraits;
    private InfectedPlayerTraits _infectedPlayerTraits;
    private HealthyPlayerTraits _healthyPlayerTraits;

    // Start is called before the first frame update
    void Start()
    {
        _playerTraits = new PlayerTraits();
        InitializeIsInfected(false);
        _healthyPlayerTraits = new HealthyPlayerTraits(LevelManager.VulnerablityChance_Gn);
    }

    private void Update()
    {
        if(_playerTraits._isInfected)
        {

        }
    }

    public void InitializeIsInfected(bool _isInfected)
    {
        _playerTraits._isInfected = _isInfected;
        LevelManager.isPlayerInfected = _playerTraits._isInfected;
    }

    public void ChangeState()
    {
        InitializeIsInfected(true);
        //CircleCollider2D Transmitter = gameObject.transform.GetChild(0).GetComponent<CircleCollider2D>();
        //Transmitter.gameObject.SetActive(true);
        //Transmitter.radius = LevelManager.InfectionRadius;

        _infectedPlayerTraits = new InfectedPlayerTraits(0.5f);
    }

    public void Infection(float NPCSpreadChance)
    {
        Debug.Log(_playerTraits._isInfected);
        //if player is not already infected
        if (!_playerTraits._isInfected && !LevelManager.isLevelFinished)
        {
            float playerVulnerability = _healthyPlayerTraits.vulnerability;

            //Total Chance of other getting infectedd
            float totalChance = NPCSpreadChance + playerVulnerability;
            //Calculate NPC luck
            float infectionChance = Random.Range(0f, 1f);

            //Infect
            if (infectionChance <= totalChance)
            {
                Debug.Log("Player Infected");
                ChangeState();
            }
            else//Do not infect
                Debug.Log("Good Luck of Player");
        }
    }
}
