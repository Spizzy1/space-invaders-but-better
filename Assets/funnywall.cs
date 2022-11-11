using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class funnywall : MonoBehaviour
{
    [SerializeField]
    GameObject otherWall;

    [SerializeField]
    public enum direction
    {
        left,
        right
    }
    [SerializeField]
    direction wallDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.tag == "Player")
        {
            if (wallDirection == direction.left && collision.GetComponent<playermovement>().movement.x < 0)
            {
                float newX = otherWall.transform.position.x;
                collision.gameObject.transform.position = new Vector2((newX-0.1f), collision.gameObject.transform.position.y);
            }
            if (wallDirection == direction.right && collision.GetComponent<playermovement>().movement.x > 0)
            {
                float newX = otherWall.transform.position.x;
                collision.gameObject.transform.position = new Vector2((newX+0.1f), collision.gameObject.transform.position.y);
            }
        }
        */
    }
}
