using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform checkTouchingGround = null;
    [SerializeField] private LayerMask playerMask;

    private bool jumpKeyPressed = false;
    private float horizontalInput;
    private Rigidbody rbComponent;
    private bool touchingGround;
    private int characterVelocity = 5;
    private bool crouching = false;

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

        // crouching and character velocity
        if (Input.GetKeyDown("s"))
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.5f, transform.localPosition.z);
        }

        if (Input.GetKey("s"))
        {
            crouching = true;
            characterVelocity = 2;
        }
        else
        {
            crouching = false;

            if (Input.GetKey("left shift") && touchingGround)
            {
                characterVelocity = 10;
            }
            else
            {
                characterVelocity = 5;
            }
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
    }

    // pobiranje stvari
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
