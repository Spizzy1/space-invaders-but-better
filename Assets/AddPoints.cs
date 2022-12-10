using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddPoints : MonoBehaviour
{
    public delegate void onLoad();
    public static event onLoad Pointsloaded;
    public float savePoints;
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
        savePoints += points * ((upgradeScript.items["doublePoint"] * 0.5f + 1) * ((upgradeScript.items["bigPoint"] * 10)+1));
        gameObject.GetComponent<TextMeshProUGUI>().text = $"Points" + " " + savePoints;
        GameObject.Find("Data Manager").GetComponent<DataManage>().points = savePoints;
    }
}
