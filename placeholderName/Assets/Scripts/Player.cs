using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform checkTouchingGround = null;
    [SerializeField] private Transform checkTouchingLadder = null;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask playerMask2;

    private Rigidbody rbComponent;
    private bool jumpKeyPressed = false;
    private bool crouching = false;
    private bool touchingGround;
    private bool LadderClimbing = false;
    // FIX: climbing -> walking transition
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
        if (Input.GetKey("s") && LadderClimbing == true)
        {
            crouching = false;
        }
        else if (Input.GetKey("s"))
        {
            crouching = true;
        }
        else
        {
            crouching = false;
        }
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
        if (Physics.OverlapBox(checkTouchingGround.position, new Vector3(0.4999f, 0.05f, 0.1f), new Quaternion(1,0,0,0), playerMask).Length > 0)
        {
            touchingGround = true;
        }
        else
        {
            touchingGround = false;
        }

        if (Physics.OverlapBox(checkTouchingLadder.position, new Vector3(0.5f, 0.5f, 0.1f), new Quaternion(1,0,0,0), playerMask2).Length > 0)
        {
            LadderClimbing = true;
        }
        else
        {
            LadderClimbing = false;
        }

        // jumping
        if (jumpKeyPressed && touchingGround && LadderClimbing)
        {
            rbComponent.AddForce(new Vector3(0, 1, 0), ForceMode.VelocityChange);
            jumpKeyPressed = false;
        }
        else if (jumpKeyPressed && touchingGround)
        {
            rbComponent.AddForce(new Vector3(0, 7, 0), ForceMode.VelocityChange);
            jumpKeyPressed = false;
        }

        // walking
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
        // gor in levo/desno
        if (LadderClimbing == true && Input.GetKey("w") && Input.GetKey("d"))
        {
            rbComponent.velocity = new Vector3(1, 3, 0);
            GetComponent<Rigidbody>().useGravity = false;
        }
        else if (LadderClimbing == true && Input.GetKey("w") && Input.GetKey("a"))
        {
            rbComponent.velocity = new Vector3(-1, 3, 0);
            GetComponent<Rigidbody>().useGravity = false;
        }
        // dol in levo/desno
        else if (LadderClimbing == true && Input.GetKey("s") && Input.GetKey("d"))
        {
            rbComponent.velocity = new Vector3(1, -3, 0);
            GetComponent<Rigidbody>().useGravity = false;
        }
        else if (LadderClimbing == true && Input.GetKey("s") && Input.GetKey("a"))
        {
            rbComponent.velocity = new Vector3(-1, -3, 0);
            GetComponent<Rigidbody>().useGravity = false;
        }
        // gor
        else if (LadderClimbing == true && Input.GetKey("w"))
        {
            rbComponent.velocity = new Vector3(0, 3, 0);
            GetComponent<Rigidbody>().useGravity = false;
        }
        // dol
        else if (LadderClimbing == true && Input.GetKey("s"))
        {
            rbComponent.velocity = new Vector3(0, -3, 0);
            GetComponent<Rigidbody>().useGravity = false;
        }
        // desno na lestvi
        else if (LadderClimbing == true && Input.GetKey("d"))
        {
            rbComponent.velocity = new Vector3(1, 0, 0);
            GetComponent<Rigidbody>().useGravity = false;
        }
        // levo na lestvi
        else if (LadderClimbing == true && Input.GetKey("a"))
        {
            rbComponent.velocity = new Vector3(-1, 0, 0);
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

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
