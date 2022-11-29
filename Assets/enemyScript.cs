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
        if (enemyCustomization.HP <= 0)
        {
            GameObject.Find("Points").GetComponent<AddPoints>().pointAdd(enemyCustomization.points);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "bullet")
        {
            Debug.Log("agg");
            enemyCustomization.HP -= collision.GetComponent<bulletData>().damage;
            Destroy(collision.gameObject);
        }
    }
    [System.Serializable, SerializeField]
    class enemy
    {
        [SerializeField]
        public float HP;
        public float points;
    }
}
