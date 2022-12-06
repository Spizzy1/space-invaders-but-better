using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DataManage : MonoBehaviour
{
    public float points = 0;
    // Start is called before the first frame update
    void Start()
    {
        AddPoints.Pointsloaded += resetPoints;
    }
    private void Awake()
    {
        if(GameObject.FindGameObjectsWithTag("Data").Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene("GameOver");
        }
        if(GameObject.Find("Points") != null)
        {
            points = GameObject.Find("Points").GetComponent<AddPoints>().savePoints;
            if(PlayerPrefs.GetFloat("highScore") < points)
            {
                PlayerPrefs.SetFloat("highScore", points); 
            }
        }
    }
    void resetPoints()
    {
        GameObject points = GameObject.Find("Points");
        points.GetComponent<AddPoints>().savePoints = 0;
        points.GetComponent<TextMeshProUGUI>().text = "Points:" + " " + 0;
    }
}
