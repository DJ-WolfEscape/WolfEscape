using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    public Animator animator;
    private CharacterController controller;

    public SpawnManager spawnManager;

    private Vector3 direction;

    public float fowardSpeed;

    public int desiredLane = 1; // 0 : left, 1: middle , 2: right
    public float laneDistance = 4; // distance between  two lanes

    public float jumpForce = 0;
    public float gravity = -20;

    private bool isCoroutineRunning;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        // animator.SetBool("isRunning", true);


    }

    private void Update()
    {

        if (!CountdownController.isGameStarted)
        {
            animator.SetBool("isRunning", false);
            return;
        }

        if (Mathf.RoundToInt(transform.position.z) % 3 == 0)
        {
            if (fowardSpeed <= 40)
                fowardSpeed += 0.001f;
        }
        direction.z = fowardSpeed;


        if (controller.isGrounded)
        {
            animator.SetBool("isRunning", true);

            // animator.SetBool("isRunning", true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isCoroutineRunning)
                    return;
                animator.SetBool("isRunning", false);
                animator.SetTrigger("isJumping");
                //animator.SetBool("isRunning", false);

                Jump();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (isCoroutineRunning)
                    return;
                animator.SetTrigger("isRolling");


                StartCoroutine(delayRoll());
                isCoroutineRunning = true;

                //animator.SetBool("isRolling", true);

            }
            //direction.y = -1;
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
        }

        // gather input on which lane we should be
        if (Input.GetKeyDown(KeyCode.A))
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;

        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, 80 * Time.fixedDeltaTime);


    }
    IEnumerator delayRoll()
    {
        yield return new WaitForSeconds(1f);
        isCoroutineRunning = false;


    }
    private void Jump()
    {
        if (!CountdownController.isGameStarted)
            return;
        direction.y = jumpForce;
    }

    private void FixedUpdate()
    {
        if (!CountdownController.isGameStarted)
            return;
        controller.Move(direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        spawnManager.SpawnTriggerEntered();
    }


}
