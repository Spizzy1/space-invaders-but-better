using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class livesUI : MonoBehaviour
{
    [SerializeField]
    Color GlassColor;
    bool isGlass = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void updateHP(float HP, bool glassCanon)
    {
        if (!glassCanon)
        {
            string HPCast = HP.ToString();
            int amountToTake = Mathf.Clamp(HPCast.Length, HPCast.Length, 4);
            HPCast = HPCast.Substring(0, amountToTake);
            gameObject.GetComponent<TextMeshProUGUI>().text = "Lives" + " " + HPCast;
        }
    }
    public void GlassHP()
    {
        gameObject.GetComponent<TextMeshProUGUI>().color = GlassColor;
        gameObject.GetComponent<TextMeshProUGUI>().text = "N U L L";
    }
}
