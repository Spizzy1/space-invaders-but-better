using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class shoot : MonoBehaviour
{
    [SerializeField]
    public GameObject bulletPrefab;
    [SerializeField]
    float cooldownGeneral;
    [SerializeField]
    bool onCooldown = false;
    [SerializeField]
    float speed;
    public float damage;
    [SerializeField]
    int health;
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
            GameObject bulletinstance = Instantiate(bulletPrefab);
            bulletinstance.GetComponent<bulletData>().damage = damage;
            bulletinstance.transform.position = this.gameObject.transform.position;
            bulletinstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            onCooldown = true;
            StartCoroutine(cooldown(cooldownGeneral));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemyBullet" )
        {
            Debug.Log("Hi, hi hi");
            health--;
            Destroy(collision.gameObject);
            GameObject.Find("Lives").GetComponent<livesUI>().updateHP(health);

            if (health <= 0)
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
