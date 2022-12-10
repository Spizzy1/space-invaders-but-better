using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreDamageUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string storeDamage = " ";
        float calculatedDamage = Mathf.Clamp(GameObject.Find("player").GetComponent<Shoot>().storedDamage, 0, 1) * Mathf.Pow(1.5f, GameObject.Find("player").GetComponent<Shoot>().storedDamage);
        string calcDamageCast = calculatedDamage.ToString();
        int amountToTake = Mathf.Clamp(calcDamageCast.Length, calcDamageCast.Length, 4);
        calcDamageCast = calcDamageCast.Substring(0, amountToTake);
        if (upgradeScript.items["damageStore"] > 0 && !GameObject.Find("player").GetComponent<Shoot>().glassCanon)
        {
            storeDamage = "Stored damage " + calcDamageCast + "\nMAX " + GameObject.Find("player").GetComponent<Shoot>().health * upgradeScript.items["damageStore"];
        }
        gameObject.GetComponent<TextMeshProUGUI>().text = storeDamage;
    }
}
