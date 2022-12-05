using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddPoints : MonoBehaviour
{
    public delegate void onLoad();
    public static event onLoad Pointsloaded;
    public float savePoints;
    public float pointMultiplier = 1;
    // Start is called before the first frame update
    void Start()
    {
        Pointsloaded();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void pointAdd(float points)
    {
        savePoints += points * pointMultiplier;
        gameObject.GetComponent<TextMeshProUGUI>().text = $"Points:" + " " + savePoints;
    }
}
