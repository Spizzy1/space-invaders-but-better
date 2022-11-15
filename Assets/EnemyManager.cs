using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    List<enemyData> enemyTypes = new List<enemyData>();
    public float points;

    [SerializeField]
    List<wrapperList> grid = new List<wrapperList>();
    List<GameObject> locations = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in GameObject.Find("Spawn list").transform)
        {
            locations.Add(child.gameObject);
        }
        spawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void spawnEnemies()
    {
        
        float minCost = enemyTypes.Where(x => x.cost > 0).Min(x => x.cost);
        int currentGrid = 0;
        int attempts = 0;
        while(points > minCost && grid.SelectMany(x => x.sizeX).Contains(false) && attempts < 50 && currentGrid <= grid.Count-1)
        {
            enemyData selectedEnemy = null;
            while (selectedEnemy == null && points > minCost)
            {
                int randomEnemy = Random.Range(0, enemyTypes.Count);
                if (enemyTypes[randomEnemy].cost < points)
                {
                    Debug.Log("please...");
                    selectedEnemy = enemyTypes[randomEnemy];
                }
            }
            if (selectedEnemy != null)
            {
                //int randomSpawn = Random.Range(1, Mathf.Min(Mathf.FloorToInt(points / selectedEnemy.cost), Mathf.FloorToInt(grid[currentGrid].sizeX.Count) / selectedEnemy.slotX));
                int randomSpawn = 1;
                bool canSpawn = true;
                int spawnIndex = -1;
                for (int o = 0; o < randomSpawn; o++)
                {
                    for (int i = 0; i < selectedEnemy.slotY; i++)
                    {
                        if(grid.Count - currentGrid + 1 < selectedEnemy.slotY)
                        {
                            return;
                        }
                        else if(grid[currentGrid + i].sizeX.Count != grid[currentGrid].sizeX.Count)
                        {
                            return;   
                        }
                    }
                    for (int i = 0; i < grid[currentGrid].sizeX.Count; i++)
                    {
                        if (!grid[currentGrid].sizeX[i] && grid[currentGrid].sizeX.Count - (i) >= selectedEnemy.slotX)
                        {
                            for (int j = 0; j < selectedEnemy.slotX; j++)
                            {
                                if((i + j + 1) > grid[currentGrid].sizeX.Count)
                                {
                                    canSpawn = false;
                                }
                                else
                                {
                                    if (grid[currentGrid].sizeX[i + j])
                                    {
                                        canSpawn = false;
                                    }
                                }
                            }
                            if (canSpawn == true)
                            {
                                spawnIndex = i;
                                break;
                            }
                        }
                        
                    }
                    if (canSpawn && spawnIndex != -1)
                    {
                        points -= selectedEnemy.cost;
                        GameObject sawnedEnemy = Instantiate(selectedEnemy.prefab);
                        for(int i = 0; i < selectedEnemy.slotX; i++)
                        {
                            for(int j = 0; j < selectedEnemy.slotY; j++)
                            {
                                Debug.Log(spawnIndex);
                                grid[j].sizeX[i + spawnIndex] = true;
                            }
                        }
                        float yPosition = GameObject.Find("Spawn list").transform.position.y - ((selectedEnemy.slotY-1) * locations[spawnIndex].transform.localScale.y/2);
                        float xPosition = (locations[spawnIndex].transform.position.x - locations[spawnIndex].transform.localScale.x / 2) + (locations[spawnIndex].transform.localScale.x / 2) * selectedEnemy.slotX;
                        sawnedEnemy.transform.position = new Vector3(xPosition, yPosition, 0);

                    }
                }
            }
            else
            {
                return;
            }
            attempts++;
        }
    }
    [System.Serializable]
    public class enemyData
    {
        [SerializeField]
        public int slotY;
        [SerializeField]
        public int slotX;
        [SerializeField]
        public GameObject prefab;
        [SerializeField]
        public float cost;
    }
    [System.Serializable]
    class spawnLocations
    {
        [SerializeField]
        GameObject location;
        bool used;  
    }
    [System.Serializable]
    class wrapperList
    {
        public List<bool> sizeX;
    }
}