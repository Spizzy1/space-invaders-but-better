using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string tooltip = "Loading...";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject.Find("Tooltip").GetComponent<TextMeshProUGUI>().text = tooltip;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameObject.Find("Tooltip").GetComponent<TextMeshProUGUI>().text = " ";
    }
}
