using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DataManage : MonoBehaviour
{
    public float points = 0;
    public static int difficulty = 0;
    [SerializeField]
    public List<Difficulty> difficultyList = new List<Difficulty>();
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
        if(points > PlayerPrefs.GetFloat("highScore"))
        {
            PlayerPrefs.SetFloat("highScore", points);
        }
    }
    void resetPoints()
    {
        points = 0;
        GameObject pointObject = GameObject.Find("Points");
        pointObject.GetComponent<AddPoints>().savePoints = 0;
        pointObject.GetComponent<TextMeshProUGUI>().text = "Points" + " " + 0;
    }
    [System.Serializable]
    public class Difficulty
    {
        public float HPScale;
        public float DamageScale;
        public int rareItemWave;
    }
}
