using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddPoints : MonoBehaviour
{
    float savePoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void pointAdd(float points)
    {
        savePoints += points;
        gameObject.GetComponent<TextMeshProUGUI>().text = $"Points:" + " " + savePoints;
    }
}
