using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class loadHighScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "High Score " + PlayerPrefs.GetFloat("highScore");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
