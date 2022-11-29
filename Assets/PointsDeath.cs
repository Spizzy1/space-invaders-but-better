using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsDeath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "Score: " + GameObject.Find("Data Manager").GetComponent<DataManage>().points;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
