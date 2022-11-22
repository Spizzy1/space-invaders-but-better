using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class funnywall : MonoBehaviour
{
    [SerializeField]
    GameObject otherWall;

    [SerializeField]
    public enum direction
    {
        left,
        right
    }
    [SerializeField]
    direction wallDirection;
    GameObject enemyGroup;
    // Start is called before the first frame update
    void Start()
    {
        enemyGroup = GameObject.Find("EnemyGroup");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
