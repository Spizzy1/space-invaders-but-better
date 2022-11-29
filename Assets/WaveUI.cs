using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void addWave(int wave)
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "Wave" + " " + wave;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
