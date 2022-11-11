using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    [SerializeField]
    enemy enemyCustomization;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [System.Serializable, SerializeField]
    class enemy
    {
        [SerializeField]
        float speed;
        [SerializeField]
        float HP;
    }
}
