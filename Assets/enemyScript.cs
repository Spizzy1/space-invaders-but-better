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
    // Start is called before the first frame update
    void Start()
    {
        if (enemyCustomization.shoots)
        {
            StartCoroutine(shoot(shootObject, enemyCustomization.shootCooldown, enemyCustomization.shootSpeed));
        }
    }
    IEnumerator shoot(GameObject pew, float cooldown, float speed)
    {
        while (this.gameObject != null)
        {
            GameObject pewInstance = Instantiate(pew);
            pewInstance.transform.position = this.gameObject.transform.position;
            pewInstance.transform.Rotate(new Vector3(0, 0, 180));
            pewInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
            pewInstance.GetComponent<bulletData>().damage = damage;

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
                if (collision.gameObject.GetComponent<bulletData>().pierce <= 0)
                {
                    Destroy(collision.gameObject);
                }
                collision.gameObject.GetComponent<bulletData>().pierce--;
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
