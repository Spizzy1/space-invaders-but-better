using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class shoot : MonoBehaviour
{
    [SerializeField]
    public GameObject bulletPrefab;
    [SerializeField]
    public float cooldownGeneral;
    [SerializeField]
    bool onCooldown = false;
    [SerializeField]
    float speed;
    public float damage;
    [SerializeField]
    public float health;
    public float damageMultiplier = 1;
    public bool glassCanon;
    public float hurtMultiplier;
    public float damageReduction;
    public int pierce;
    public int bulletMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Lives").GetComponent<livesUI>().updateHP(health);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space) && !onCooldown)
        {
            /*float positionChunk = gameObject.transform.localScale / bulletMultiplier;
            for(int i = 0; i < bulletMultiplier; i++)
            {

            }
            */
            GameObject bulletinstance = Instantiate(bulletPrefab);
            bulletinstance.GetComponent<bulletData>().damage = damage * damageMultiplier;
            bulletinstance.transform.position = this.gameObject.transform.position;
            bulletinstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            bulletinstance.GetComponent<bulletData>().pierce = pierce;
            onCooldown = true;
            StartCoroutine(cooldown(cooldownGeneral));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemyBullet" )
        {
            Debug.Log("Hi, hi hi");
            health -= Mathf.Clamp(collision.gameObject.GetComponent<bulletData>().damage * hurtMultiplier - 1*damageReduction, 0, collision.gameObject.GetComponent<bulletData>().damage * hurtMultiplier - 1 * damageReduction);
            Destroy(collision.gameObject);
            GameObject.Find("Lives").GetComponent<livesUI>().updateHP(health);

            if (health <= 0 || glassCanon)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }
    private IEnumerator cooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }
}
