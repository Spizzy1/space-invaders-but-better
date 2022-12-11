using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DifficultySelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int difficulty = 0;
    public string scene;
    public string description;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(difficultySelect);
    }
    void difficultySelect()
    {
        DataManage.difficulty = difficulty;
        SceneManager.LoadScene(scene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.activeSelf)
        {
            GameObject.Find("Tooltip").GetComponent<TextMeshProUGUI>().text = description;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (gameObject.activeSelf)
        {
            GameObject.Find("Tooltip").GetComponent<TextMeshProUGUI>().text = " ";
        }
    }
}
