using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStandardUnits : MonoBehaviour
{
    float conversionFactor;
    GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = this.gameObject;
    }
    private void Awake()
    {
        conversionFactor = (camera.GetComponent<Camera>().orthographicSize * 2) / GetComponent<Camera>().pixelHeight;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public static float lerp(float x, float minin, float maxin, float min, float max)
    {
        return (x - minin) / (maxin - minin) * (max - min) + min;
    }

}
