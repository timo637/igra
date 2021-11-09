using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool jumpKeyPressed = false;
    private float horizontalInput;
    private Rigidbody rbComponent;
    private bool touchingGround;
    private int sprinting;

    // Start is called before the first frame update
    void Start()
    {
        rbComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown("w") && touchingGround)
        {
            jumpKeyPressed = true;
        }
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
        if (jumpKeyPressed && touchingGround)
        {
            rbComponent.AddForce(new Vector3(0, 7, 0), ForceMode.VelocityChange);
            jumpKeyPressed = false;
        }

        if (Input.GetKey("left shift"))
        {
            sprinting = 10;
        }
        else
        {
            sprinting = 5;
        }

        rbComponent.velocity = new Vector3(sprinting * horizontalInput, rbComponent.velocity.y, sprinting * rbComponent.velocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        touchingGround = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        touchingGround = false;
    }
}
