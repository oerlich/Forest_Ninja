using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed = 2.0f;
    [SerializeField] float sprintSpeed = 2.5f;
    [SerializeField] float jumpHeight = 0.7f;
    [SerializeField] Camera mainCamera;
    [SerializeField] Animator animator;
    public AudioSource RunningSound;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float currSpeed = 0f;
    private float gravityValue = Physics.gravity.y;
    private PlayerCombat pc;

    private void Start()
    {
        controller = this.GetComponent<CharacterController>();
        pc = this.GetComponent<PlayerCombat>();
        currSpeed = playerSpeed;
        animator.SetFloat("Sprint", 1.0f);
    }

    void Update()
    {
        if (!pc._isAttacking)
        {
            float vert = Input.GetAxis("Vertical");
            float horiz = Input.GetAxis("Horizontal");

            if (Input.GetKey("left shift"))
            {
                currSpeed = sprintSpeed;
                animator.SetFloat("Sprint", 1.3f);
            }
            else
            {
                currSpeed = playerSpeed;
                animator.SetFloat("Sprint", 1.0f);
            }

            if (vert > 0)
                animator.SetBool("Forward", true);
            else if (vert < 0)
                animator.SetBool("Back", true);
            else
            {
                animator.SetBool("Forward", false);
                animator.SetBool("Back", false);
            }

            if (horiz > 0)
                animator.SetBool("Right", true);
            else if (horiz < 0)
                animator.SetBool("Left", true);
            else
            {
                animator.SetBool("Right", false);
                animator.SetBool("Left", false);
            }
        }
    }

    void FixedUpdate()
    {
        if(Input.GetKeyDown("escape") || Input.GetKey("escape"))
        {
            SceneManager.LoadScene("Level");
        }

        float vert = Input.GetAxis("Vertical");
        float horiz = Input.GetAxis("Horizontal");

        //Rotation code by iKabyLake30 on Unity Answers
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.cyan);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

        if (!pc._isAttacking)
        {
            // Base Code from Unity Official Documentation https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector3 move = (transform.forward * vert) + (Camera.main.transform.right * horiz);
            move.Normalize();

            controller.Move(move * Time.deltaTime * currSpeed);

            // Changes the height position of the player..
            if ((Input.GetButtonDown("Jump") || Input.GetButton("Jump")) && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);


    }
}
