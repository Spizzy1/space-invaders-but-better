using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shoot : MonoBehaviour
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
    public bool firstHit;
    bool storingDamage;
    bool setColor;
    bool holyMantle;
    bool immunity;
    public float storedDamage;
    [SerializeField]
    Color shieldColor;
    [SerializeField]
    Color defaultColor;
    [SerializeField]
    Color storeDamageColor;
    [SerializeField]
    Color storeDamageShieldColor;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = defaultColor;
        GameObject.Find("Lives").GetComponent<livesUI>().updateHP(health, glassCanon);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !onCooldown)
        {
            #region formulas
            float cooldownFormula = cooldownGeneral * (Mathf.Pow(0.9f, upgradeScript.items["attackspeedIncrease"])) * ((upgradeScript.items["doubleShoot"] * 0.5f) + 1);
            float damageFormula = (damage + (upgradeScript.items["damageUP"] * 0.5f)) * ((upgradeScript.items["doubleDamage"]) + 1) * (upgradeScript.items["glassCannon"] + 1) * (upgradeScript.items["smallDamageMulti"] * 0.2f + 1);
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
                if (bulletMultiplierFormula > 1)
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
        if (upgradeScript.items["HolyMantle"] > 0 && !setColor)
        {
            setColor = true;
            gameObject.GetComponent<SpriteRenderer>().color = shieldColor;
        }
        if (Input.GetKeyDown(KeyCode.X) && upgradeScript.items["damageStore"] > 0)
        {
            storingDamage = true;
            if(holyMantle == false && upgradeScript.items["HolyMantle"] > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().color = storeDamageShieldColor;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().color = storeDamageColor;
            }
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            storingDamage = false;
            if (holyMantle == false && upgradeScript.items["HolyMantle"] > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().color = shieldColor;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().color = defaultColor;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemyBullet" )
        {
            Debug.Log("Hi, hi hi");
            float damageFormula = ((collision.gameObject.GetComponent<bulletData>().damage * Mathf.Pow(0.5f,upgradeScript.items["halfDamage"])) - upgradeScript.items["unCommonDR"] *0.2f)*Mathf.Pow(0.5f, upgradeScript.items["oneHithalf"] * Convert.ToInt32(firstHit));
            if (!immunity)
            {
                if (holyMantle == false && upgradeScript.items["HolyMantle"] > 0)
                {
                    holyMantle = true;
                    StartCoroutine(HolyMantle());
                    StartCoroutine(Flash(1, gameObject, true));
                }
                else
                {
                    if (!storingDamage)
                    {
                        health -= Mathf.Clamp(damageFormula, 0, damageFormula);
                        firstHit = false;
                        if (health <= 0 || glassCanon)
                        {
                            SceneManager.LoadScene("GameOver");
                        }
                        StartCoroutine(Flash(2, gameObject, true));
                    }
                    else
                    {
                        firstHit = false;
                        storedDamage += damageFormula;
                        if (Mathf.Pow(1.5f, storedDamage) > (health * upgradeScript.items["damageStore"]) || glassCanon)
                        {
                            SceneManager.LoadScene("GameOver");
                        }
                        StartCoroutine(Flash(1, gameObject, true));
                    }
                }
                GameObject.Find("Lives").GetComponent<livesUI>().updateHP(health, glassCanon);
            }
            Destroy(collision.gameObject);
        }
    }
    public static IEnumerator Flash(int times, GameObject target, bool player = false, float startAlpha = 1)
    {
        Color currentColor = target.GetComponent<SpriteRenderer>().color;

        if (player)
        {
            target.GetComponent<Shoot>().immunity = true;
        }
        for(int i = 0; i < times; i++)
        {
            currentColor = target.GetComponent<SpriteRenderer>().color;
            target.GetComponent<SpriteRenderer>().color = new Color(currentColor.r, currentColor.g, currentColor.b, 0);
            yield return new WaitForSeconds(0.1f);
            target.GetComponent<SpriteRenderer>().color = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a);
            yield return new WaitForSeconds(0.1f);
        }
        target.GetComponent<SpriteRenderer>().color = new Color(currentColor.r, currentColor.g, currentColor.b, startAlpha);
        if (player)
        {
            target.GetComponent<Shoot>().immunity = false;
        }

    }
    IEnumerator HolyMantle()
    {
        if (storingDamage)
        {
            gameObject.GetComponent<SpriteRenderer>().color = storeDamageColor;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = defaultColor;

        }
        yield return new WaitForSeconds(5 * Mathf.Pow(0.5f, upgradeScript.items["HolyMantle"] - 1));
        holyMantle = false;
        if (storingDamage)
        {
            gameObject.GetComponent<SpriteRenderer>().color = storeDamageShieldColor;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = shieldColor;
        }
    }
    private IEnumerator cooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }
}
