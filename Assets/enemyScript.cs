    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class enemyScript : MonoBehaviour
{
    [SerializeField]
    GameObject shootObject;
    [SerializeField]
    enemy enemyCustomization;
    public float damage;
    public int ID;
    public float angle;
    public int bulletMultiplier;
    [SerializeField]
    public int pierceResistance;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        if (enemyCustomization.shoots)
        {
            float randomWait = Random.Range(0.1f, 1);
            yield return new WaitForSeconds(randomWait);
            Debug.Log(randomWait);
            StartCoroutine(shoot(shootObject, enemyCustomization.shootCooldown, enemyCustomization.shootSpeed));
        }
    }
    IEnumerator shoot(GameObject pew, float cooldown, float speed)
    {
        while (this.gameObject != null)
        {
            float saveAngle = -angle / 2;   
            float anglePieces = angle / (bulletMultiplier-1);
            for(int i = 0; i < bulletMultiplier; i++)
            {
                GameObject pewInstance = Instantiate(pew);
                pewInstance.transform.position = this.gameObject.transform.position;
                if(saveAngle != 0)
                {
                    pewInstance.transform.Rotate(new Vector3(0, 0, -90 + saveAngle + anglePieces * i));
                }
                else
                {
                    pewInstance.transform.Rotate(new Vector3(0, 0, -90));
                }
                pewInstance.GetComponent<Rigidbody2D>().velocity = pewInstance.transform.right * (speed*4);
                pewInstance.GetComponent<bulletData>().damage = damage;
            }


            yield return new WaitForSeconds(cooldown);
        }
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
            if (!collision.gameObject.GetComponent<bulletData>().touchedID.Contains(ID))
            {
                collision.gameObject.GetComponent<bulletData>().touchedID.Add(ID);
                Debug.Log("agg");
                enemyCustomization.HP -= collision.GetComponent<bulletData>().damage;
                collision.gameObject.GetComponent<bulletData>().pierce -= pierceResistance;
                if (collision.gameObject.GetComponent<bulletData>().pierce <= 0)
                {
                    Destroy(collision.gameObject);
                }
            }
        }
    }
    [System.Serializable, SerializeField]
    class enemy
    {
        [SerializeField]
        public float HP;
        public float points;
        public bool shoots;
        public float shootCooldown;
        public float shootSpeed;

    }
}
