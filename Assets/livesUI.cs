using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public void updateHP(int HP)
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "Lives:" + " " + HP;
    }
}
