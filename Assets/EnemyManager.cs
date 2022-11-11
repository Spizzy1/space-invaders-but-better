using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    List<enemyData> enemyTypes = new List<enemyData>();
    float points = 1000;

    List<wrapperList> grid = new List<wrapperList>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void spawnEnemies()
    {
        int currentGrid = 0;
        enemyData selectedEnemy = null;
        while (selectedEnemy == null && points > enemyTypes.Min(x => x.cost))
        {
            int randomEnemy = Random.Range(0, enemyTypes.Count);
            if (enemyTypes[randomEnemy].cost < points)
            {
                selectedEnemy = enemyTypes[randomEnemy];
            }
        }
        if (selectedEnemy != null)
        {
            int randomSpawn = Random.Range(1, Mathf.Min(Mathf.FloorToInt(points / selectedEnemy.cost), Mathf.FloorToInt(grid[currentGrid].sizeX.Count) / selectedEnemy.slotX));
            bool canSpawn = true;
            for (int i = 0; i < grid[currentGrid].sizeX.Count; i++)
            {
                if (!grid[currentGrid].sizeX[i])
                {
                    for (int j = 1; j < selectedEnemy.slotX; i++)
                    {
                        if (grid[currentGrid].sizeX[i + j])
                        {
                            canSpawn = false;
                        }
                    }
                }
            }
        }
        else
        {
            return;
        }
    }
    [System.Serializable]
    public class enemyData
    {
        [SerializeField]
        int slotY;
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