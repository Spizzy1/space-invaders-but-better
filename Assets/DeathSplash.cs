using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeathSplash : MonoBehaviour
{
    [SerializeField]
    List<string> splashScreens = new List<string>();
    [SerializeField]
    float thetaRange;
    // Start is called before the first frame update
    void Start()
    {
        int index = Random.Range(0, splashScreens.Count);
        gameObject.GetComponent<TextMeshProUGUI>().text = splashScreens[index];
        StartCoroutine(rotatE());
    }
    IEnumerator rotatE()
    {
        float degreesPerRotation = 0.3f;
        while (true)
        {
            int rotateSteps = Mathf.FloorToInt(thetaRange*2 / Mathf.Abs(degreesPerRotation));
            Debug.Log(rotateSteps);
            for(int i = 0; i < rotateSteps; i++)
            {
                gameObject.transform.Rotate(new Vector3(0, 0, degreesPerRotation));
                yield return new WaitForSeconds(0.01f);
            }
            degreesPerRotation *= -1;
            Debug.Log(degreesPerRotation);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
