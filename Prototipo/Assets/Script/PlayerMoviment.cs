using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    public Animator animator;
    private CharacterController controller;
    public GameManager gameManager;
    public SpawnManager spawnManager;

    private Vector3 direction;

    public float fowardSpeed;
    public int desiredLane = 1; // 0 : left, 1: middle , 2: right
    public float laneDistance = 4; // distance between  two lanes

    public float jumpForce = 0;
    public float gravity = -20;

    private bool isCoroutineRunning;
    public float slideDuration = 1.0f;
    private bool isRolling = false;

    public string spawnerTriggerTag;

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
                fowardSpeed += 0.00005f;
        }
        direction.z = fowardSpeed;
        animator.SetBool("isGrounded", controller.isGrounded);

        if (controller.isGrounded)
        {
            animator.SetBool("isRunning", true);

            // animator.SetBool("isRunning", true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //animator.SetBool("isRunning", false);
                if(isRolling)
                    return;
                if (isCoroutineRunning)
                    return;

                Jump();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (isRolling)
                    return;
                StartCoroutine(delayRoll());


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

        // transform.position = Vector3.Lerp(transform.position, targetPosition, 70 * Time.fixedDeltaTime);
        // controller.center = controller.center;

        if (transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * 30 * Time.deltaTime;
            if (moveDir.sqrMagnitude < diff.magnitude)
                controller.Move(moveDir);
            else
                controller.Move(diff);
        }

        controller.Move(direction * Time.deltaTime);


    }
    IEnumerator delayRoll()
    {
        isRolling = true;
        animator.SetBool("isRolling", true);
        yield return new WaitForSeconds(0.25f / Time.timeScale);
        controller.center = new Vector3(0, 0f, 0);
        controller.height = 1;

        yield return new WaitForSeconds((slideDuration - 0.25f) / Time.timeScale);

        animator.SetBool("isRolling", false);

        controller.center = new Vector3(0,1,0);
        controller.height = 3;

        isRolling = false;
    }
    private void Jump()
    {
        //if (!CountdownController.isGameStarted)
        //{
        //    Debug.Log("espera");

        //    return;

        //}
        animator.SetTrigger("isJumping");
        controller.center = new Vector3(0, 1, 0);
        controller.height = 3;
        isRolling = false;

        direction.y = Mathf.Sqrt(jumpForce * 2 * -gravity);
        //direction.y = jumpForce;
    }

    private void FixedUpdate()
    {
        if (!CountdownController.isGameStarted)
            return;
        controller.Move(direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cheese")
        {
            gameManager.SendMessage("AtualizarContador");
            other.gameObject.active = false;
            StartCoroutine(RespawnCoin(other.gameObject));
            //Destroy(other.gameObject);
            //Instantiate(other.gameObject);

            Debug.Log("colidiu");
        }else if (other.tag == "SpawnTrigger")
        {
            spawnManager.SpawnTriggerEntered();
        }
    }

    IEnumerator RespawnCoin(GameObject gameObject)
    {

         yield return new WaitForSeconds(1f);
        Debug.Log("ok");
        gameObject.SetActive(true);


    }


}
