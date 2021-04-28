using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float gravity;
    [SerializeField] float jumpSpeed;
    [SerializeField] int jumpCounter;
    int currentJumps;
    Vector3 velocity;
    Rigidbody2D myRigidbody2d;
    [SerializeField] Transform feet;
    [SerializeField] LayerMask obstacleLayer;
    float horizontalInput;
    bool jumpInput, hasJumped, isGrounded;

    void Awake()
    {
        myRigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetButtonDown("Jump");

        velocity.x = moveSpeed * horizontalInput;
        if(jumpInput && currentJumps < jumpCounter) {
            currentJumps++;
            hasJumped = true;
            velocity.y = jumpSpeed;
            Debug.Log("has Jumped!");
        }
    }

    /* There is a bug when you jump that you might fall too far and therefor look like you are inside the floor*/
    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(feet.position, -feet.up, velocity.y * Time.fixedDeltaTime, obstacleLayer);
        isGrounded = hit.collider != null;
        if (isGrounded && velocity.y <= 0){
            transform.position += new Vector3(0, -hit.distance, 0);
            velocity.y = 0;
            currentJumps = 0;
        }
        else if(!isGrounded) {
            velocity.y += gravity * Time.fixedDeltaTime;
        }
        myRigidbody2d.velocity = velocity;
        hasJumped = false;
    }
}
