using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class livesUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void updateHP(float HP)
    {
        string HPCast = HP.ToString();
        int amountToTake = Mathf.Clamp(HPCast.Length, HPCast.Length, 4);
        HPCast = HPCast.Substring(0, amountToTake);
        gameObject.GetComponent<TextMeshProUGUI>().text = "Lives:" + " " + HPCast;
    }
}
