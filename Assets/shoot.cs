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
    public bool glassCanon;
    public float hurtMultiplier;
    public float damageReduction;
    public int pierce;
    public int bulletMultiplier;
    bool holyMantle;
    [SerializeField]
    Color shieldColor;
    [SerializeField]
    Color defaultColor;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Lives").GetComponent<livesUI>().updateHP(health);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !onCooldown)
        {
            #region formulas
            float cooldownFormula = cooldownGeneral * (Mathf.Pow(0.9f, upgradeScript.items["attackspeedIncrease"])) * ((upgradeScript.items["doubleShoot"] * 0.5f) + 1);
            float damageFormula = (damage + (upgradeScript.items["damageUP"] * 0.5f)) * ((upgradeScript.items["doubleDamage"]) + 1) * (upgradeScript.items["glassCannon"] * 2 + 1) * (upgradeScript.items["smallDamageMulti"] * 0.2f + 1);
            float bulletMultiplierFormula = bulletMultiplier + upgradeScript.items["doubleShoot"];
            float pierceFormula = pierce + upgradeScript.items["pierceOne"] + (upgradeScript.items["pierceInf"] * 1000000);
            float shotSpeedFormula = speed * (upgradeScript.items["shotIncrease"] * 0.5f + 1);
            #endregion
            float totalAngle = 4 * bulletMultiplierFormula;
            float angle = -totalAngle / 2;
            float anglePieces = totalAngle / (bulletMultiplierFormula - 1);
            for (int i = 0; i < bulletMultiplierFormula; i++)
            {
                GameObject bulletinstance = Instantiate(bulletPrefab);
                bulletinstance.GetComponent<bulletData>().damage = damageFormula;
                bulletinstance.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + gameObject.transform.localScale.y / 2);
                if(bulletMultiplierFormula > 1)
                {
                    bulletinstance.transform.Rotate(new Vector3(0, 0, 90 + angle + anglePieces * i));
                }
                else
                {
                    bulletinstance.transform.Rotate(new Vector3(0, 0, 90));
                }
                bulletinstance.GetComponent<Rigidbody2D>().velocity = bulletinstance.transform.right * (4 * shotSpeedFormula);
                bulletinstance.GetComponent<bulletData>().pierce = pierceFormula;
            }
            onCooldown = true;
            StartCoroutine(cooldown(cooldownFormula));
        }
        if(holyMantle == false && upgradeScript.items["HolyMantle"] > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().color = shieldColor;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = defaultColor;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemyBullet" )
        {
            Debug.Log("Hi, hi hi");
            float damageFormula = (collision.gameObject.GetComponent<bulletData>().damage * Mathf.Pow(0.5f,upgradeScript.items["halfDamage"])) - upgradeScript.items["unCommonDR"] *0.2f;
            if(holyMantle == false && upgradeScript.items["HolyMantle"] > 0)
            {
                holyMantle = true;
                StartCoroutine(HolyMantle());
            }
            else
            {
                health -= Mathf.Clamp(damageFormula, 0, damageFormula);
            }
            Destroy(collision.gameObject);
            GameObject.Find("Lives").GetComponent<livesUI>().updateHP(health);

            if (health <= 0 || glassCanon)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }
    IEnumerator HolyMantle()
    {
        yield return new WaitForSeconds(5 * Mathf.Pow(0.5f, upgradeScript.items["HolyMantle"] - 1));
        holyMantle = false;
    }
    private IEnumerator cooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }
}
