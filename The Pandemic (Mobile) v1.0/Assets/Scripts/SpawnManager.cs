using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAreaDet
{
    public Vector2 From;
    public Vector2 To;
    
    public SpawnAreaDet(Vector2 from, Vector2 to)
    {
        From = from;
        To = to;
    }
}

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _badPrefab;
    [SerializeField]
    private GameObject _goodPrefab;

    
    [SerializeField]
    private float _spawnRadiusOffset = 7f;

    [SerializeField]
    private Transform SpawnArea;
    private List<Transform> SpawnAreaList;
    private Dictionary<int, SpawnAreaDet> SpawnAreaDict;

    private List<Vector3> _badSpawnPoints;
    //private Vector3[] _badSpawnPoints;
    private List<Vector3> _goodSpawnPoints;
    //private Vector3[] _goodSpawnPoints;

    private int _infectedCount;
    private List<int> _infectedNPCIndex;

    // Start is called before the first frame update
    void Start()
    {
        _infectedCount = GameManager.Infected;
        _infectedNPCIndex = new List<int>();
        for (int i = 0; i < _infectedCount; i++)
        {
            _infectedNPCIndex.Add(Random.Range(0, GameManager.no_of_bad));
        }
        SpawnAreaInit();
        //SetSpawnPoints();
        Debug.Log("Now Spawning NPCs");
        SpawnNPC(_badPrefab, GameManager.no_of_bad, true);
        SpawnNPC(_goodPrefab, GameManager.no_of_good, false);
    }

    void SpawnAreaInit()
    {
        int keyCount = 0;
        SpawnAreaDict = new Dictionary<int, SpawnAreaDet>();
        foreach (Transform child in SpawnArea)
        {
            Vector2[] points = new Vector2[2];
            int i = 0;
            foreach(Transform point in child)
            {
                points[i] = point.position;
                i++;
            }
            //add to dictionary
            SpawnAreaDict.Add(keyCount, new SpawnAreaDet(points[0], points[1]));
            keyCount++;
        }
    }

    /*
    void SetSpawnPoints()
    {
        _badSpawnPoints = new List<Vector3>();
        _goodSpawnPoints = new List<Vector3>();

        //Spawn bad NPCs
        for (int i = 0; i < _no_of_bad; i++)
        {
            Vector2 point;

            do
            {
                point.x = Random.Range(-_playarea.x, _playarea.x);
                point.y = Random.Range(-_playarea.y, _playarea.y);
            } while (CheckRadius(point));

            _badSpawnPoints.Add(new Vector3(point.x, point.y, 0)); ;
        }

        //Spawn good NPCs
        for (int i = 0; i < _no_of_good; i++)
        {
            Vector2 point;

            do
            {
                point.x = Random.Range(-_playarea.x, _playarea.x);
                point.y = Random.Range(-_playarea.y, _playarea.y);
            } while (CheckRadius(point));


            _goodSpawnPoints.Add(new Vector3(point.x, point.y, 0));
        }
    }

    bool CheckRadius(Vector2 value)
    {
        foreach (var item in _badSpawnPoints)
        {
            if ((value.x > item.x - _spawnRadiusOffset && value.x < item.x + _spawnRadiusOffset) && (value.y > item.y - _spawnRadiusOffset && value.y < item.y + _spawnRadiusOffset))
                return true;
        }
        foreach (var item in _goodSpawnPoints)
        {
            if ((value.x > item.x - _spawnRadiusOffset && value.x < item.x + _spawnRadiusOffset) && (value.y > item.y - _spawnRadiusOffset && value.y < item.y + _spawnRadiusOffset))
                return true;
        }
        return false;
    }
    */

    public void SpawnNPC(GameObject prefab, int spawnCount, bool _isBad)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            //Select SpawnPoint
            Vector2 point = SelectSpawnPoint();

            //Spawn
            GameObject npc = Instantiate(prefab, point, Quaternion.identity);
            //npc.SendMessage("Initialize");
            npc.BroadcastMessage("Initialize");
            if (_isBad)
            {
                //npc.SendMessage("InitializeIsBad", true);
                npc.BroadcastMessage("InitializeIsBad", true);

                if (_infectedNPCIndex.Contains(i))
                    //npc.SendMessage("InitializeIsInfected", true);
                    npc.BroadcastMessage("InitializeIsInfected", true);
                else
                    //npc.SendMessage("InitializeIsInfected", false);
                    npc.BroadcastMessage("InitializeIsInfected", false);
                Debug.Log("Spawned NPC");
            }
            else
            {
                //npc.SendMessage("InitializeIsBad", false);
                npc.BroadcastMessage("InitializeIsBad", false);
                Debug.Log("Spawned NPC");
            }
        }
        Debug.Log("Spawned" + spawnCount + "NPCs");
    }

    private Vector2 SelectSpawnPoint()
    {
        Vector2 point;

        int zone = Random.Range(0, SpawnAreaDict.Count);
        SpawnAreaDet limits = SpawnAreaDict[zone];

        point = new Vector2(Random.Range(limits.From.x, limits.To.x), Random.Range(limits.From.y, limits.To.y));

        return point;
    }
}
