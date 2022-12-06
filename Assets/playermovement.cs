using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    public float movespeed = 5f;
    public Rigidbody2D rigidbody;
    public Vector2 movement;
    float cordinate = 10.8f;

    private void Start()
    {
        foreach(Transform child in this.transform)
        {
            //child.position = new Vector2();
        }
    }

    void Update()
    {
        float speedFormula = movespeed * ((upgradeScript.items["moveIncrease"] * 0.3f)+1);
        movement.x = Input.GetAxisRaw("Horizontal");
        if (this.gameObject.transform.position.x > cordinate)
        {
            this.gameObject.transform.position = new Vector2(-cordinate, this.gameObject.transform.position.y);
        }
        if (this.gameObject.transform.position.x < -cordinate)
        {
            this.gameObject.transform.position = new Vector2(cordinate, this.gameObject.transform.position.y);
        }
        bool isShiftKeyDown = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        if (isShiftKeyDown)
        {
            rigidbody.MovePosition(rigidbody.position + movement * (speedFormula / 2) * Time.fixedDeltaTime);
        }
        else
        {
            rigidbody.MovePosition(rigidbody.position + movement * speedFormula * Time.fixedDeltaTime);
        }
    }

    void FixedUpdate()
    {

    }
}

