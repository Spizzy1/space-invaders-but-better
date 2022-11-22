using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyMovement : MonoBehaviour
{
    float speed;
    public Vector3 addVector;
    GameObject wallNegative;
    GameObject wallPositive;
    // Start is called before the first frame update
    void Start()
    {
        wallNegative = GameObject.Find("Wall");
        wallPositive = GameObject.Find("Wall (1)");
        addVector = new Vector3(0.5f, 0, 0);
        speed = 1;
        StartCoroutine(move());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void resetPos()
    {

    }
    IEnumerator move()
    {
        GameObject firstX = refreshFirst();
        GameObject lastX = refreshLast();
        addVector = new Vector3(0.5f, 0, 0);
        bool canDo = true;
        while (canDo)
        {
            if(firstX == null)
            {
                firstX = refreshFirst();
            }
            if(lastX == null)
            {
                lastX = refreshLast();
            }
            if(lastX != null && firstX != null)
            {
                bool goDown = false;
                this.gameObject.transform.position += addVector;
                if (lastX.transform.position.x + lastX.transform.localScale.x > wallPositive.transform.position.x || firstX.transform.position.x - firstX.transform.localScale.x < wallNegative.transform.position.x)
                {
                    goDown = true;
                }
                if (goDown)
                {
                    addVector *= -1;
                    this.gameObject.transform.position += new Vector3(0, -0.5f, 0);
                }
                this.gameObject.transform.position = new Vector3(Mathf.Clamp(this.gameObject.transform.position.x, wallNegative.transform.position.x + (firstX.transform.position.x - this.gameObject.transform.position.x), wallPositive.transform.position.x - (lastX.transform.position.x - this.gameObject.transform.position.x)), this.gameObject.transform.position.y, 0);

            }
            else
            {
                canDo = false;
            }
            yield return new WaitForSeconds(0.5f / speed);
        }
    }
    GameObject refreshFirst()
    {
        List<GameObject> allChildrenObjects = new List<GameObject>();
        foreach (Transform child in this.gameObject.transform)
        {
            allChildrenObjects.Add(child.gameObject);
        }
        GameObject firstX = allChildrenObjects.Where(x => x.transform.position.x == allChildrenObjects.Min(x => x.transform.position.x)).ToList()[0];
        return firstX;
    }
    GameObject refreshLast()
    {
        List<GameObject> allChildrenObjects = new List<GameObject>();
        foreach (Transform child in this.gameObject.transform)
        {
            allChildrenObjects.Add(child.gameObject);
        }
        GameObject lastX = allChildrenObjects.Where(x => x.transform.position.x == allChildrenObjects.Max(x => x.transform.position.x)).ToList()[0];
        return lastX;
    }
}
