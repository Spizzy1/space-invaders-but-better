using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletData : MonoBehaviour
{
    public float damage;
    public bool hasTouched;
    public float pierce;
    public List<int> touchedID = new List<int>();
    bool isShatter;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void BurstCheck()
    {
        if(upgradeScript.items["shatterBullet"] > 0 && !isShatter)
        {
            for(int i = 0; i < upgradeScript.items["shatterBullet"] * 2 + 2; i++)
            {
                GameObject shatterBullet = Instantiate(gameObject);
                shatterBullet.transform.position = gameObject.transform.position;
                shatterBullet.transform.rotation = Quaternion.identity;
                shatterBullet.transform.Rotate(new Vector3(0, 0, -180 + ((360 / (upgradeScript.items["shatterBullet"] * 2 + 2)) * i)));
                shatterBullet.GetComponent<bulletData>().isShatter = true;
                shatterBullet.GetComponent<bulletData>().damage = damage * (upgradeScript.items["shatterBullet"] * 0.2f);
                shatterBullet.GetComponent<Rigidbody2D>().velocity = shatterBullet.transform.right * 4;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
