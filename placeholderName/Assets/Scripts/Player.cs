using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform checkTouchingGround = null;
    [SerializeField] private LayerMask playerMask;

    private Rigidbody rbComponent;
    private bool jumpKeyPressed = false;
    private bool crouching = false;
    private bool touchingGround;
    private bool LadderClimbing;
    private float horizontalInput;
    private int characterVelocity = 5;

    // Start is called before the first frame update
    void Start()
    {
        rbComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // left-right movement
        horizontalInput = Input.GetAxis("Horizontal");

        // jumping
        if (Input.GetKeyDown("w") && touchingGround)
        {
            jumpKeyPressed = true;
        }

        if (touchingGround)
        {
            // crouching position transform
            if (Input.GetKeyDown("s"))
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.5f, transform.localPosition.z);
            }
            // sprinting
            if (Input.GetKey("left shift"))
            {
                characterVelocity = 10;
            }
            // velocities
            if (Input.GetKey("s"))
            {
                characterVelocity = 2;
            }
            else if (Input.GetKey("left shift"))
            {
                characterVelocity = 10;
            }
            else
            {
                characterVelocity = 5;
            }
        }

        // crouching
        if (Input.GetKey("s"))
        {
            crouching = true;
        }
        else if (LadderClimbing == true)
        {
            crouching = false;
        }
        else
        {
            crouching = false;
        }
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
        if (Physics.OverlapSphere(checkTouchingGround.position, 0.1f, playerMask).Length > 0)
        {
            touchingGround = true;
        }
        else
        {
            touchingGround = false;
        }

        // jumping
        if (jumpKeyPressed && touchingGround)
        {
            rbComponent.AddForce(new Vector3(0, 7, 0), ForceMode.VelocityChange);
            jumpKeyPressed = false;
        }

        // sprinting
        rbComponent.velocity = new Vector3(characterVelocity * horizontalInput, rbComponent.velocity.y, rbComponent.velocity.z);

        // crouching
        if (crouching)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 2, 1);
        }

        // ladder climbing
        if (LadderClimbing == true && Input.GetKey("w"))
        {
            if (Input.GetKey("a"))
            {
                rbComponent.velocity = new Vector3(-3, 0, 0);
            }

            rbComponent.velocity = new Vector3(0, 3, 0);
            GetComponent<Rigidbody>().useGravity = false;
        }
        else if (LadderClimbing == true && Input.GetKey("s"))
        {
            rbComponent.velocity = new Vector3(0, -3, 0);
            GetComponent<Rigidbody>().useGravity = false;
        }
        else if (LadderClimbing == true)
        {
            rbComponent.velocity = new Vector3(0, 0, 0);
            GetComponent<Rigidbody>().useGravity = false;
        }
        else 
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
    }

    // pregleda �e se dotika� lestve
    private void OnTriggerEnter(Collider other) => LadderClimbing = true;

    private void OnTriggerExit(Collider other) => LadderClimbing = false;
}
