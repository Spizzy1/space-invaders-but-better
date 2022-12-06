using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour
{
    [SerializeField]
    List<WaveColor> waveColorList = new List<WaveColor>();
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void addWave(int wave)
    {
        foreach(WaveColor color in waveColorList)
        {
            if(color.Wave == wave)
            {
                gameObject.GetComponent<TextMeshProUGUI>().color = color.Colour;
            }
        }
        gameObject.GetComponent<TextMeshProUGUI>().text = "Wave" + " " + wave;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [System.Serializable]
    class WaveColor
    {
        [SerializeField]
        int wave;
        public int Wave { get => wave; }
        [SerializeField]
        Color color;
        public Color Colour { get => color; }
    }
}
