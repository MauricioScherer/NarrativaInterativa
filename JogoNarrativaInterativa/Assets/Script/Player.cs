using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 2.0f;
    private float gravityValue = -9.81f;

    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GameObject conjPlayer;

    private float vertical;
    private float horizontal;
    private bool canMoving = false;

    public int direction;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if(canMoving)
        {
            if (direction == 0)
            {
                vertical = -Input.GetAxis("Vertical");
                horizontal = Input.GetAxis("Horizontal");
            }
            else if (direction == 1)
            {
                vertical = Input.GetAxis("Horizontal");
                horizontal = Input.GetAxis("Vertical");
            }
            Vector3 move = new Vector3(vertical, 0, horizontal);
            controller.Move(move * Time.deltaTime * playerSpeed);
            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        anim.SetFloat("Speed", Input.GetAxis("Vertical") + Input.GetAxis("Horizontal"));
    }

    public void SetCanMoving()
    {
        canMoving = !canMoving;
    }

    public void SetViewPlayer(bool p_status)
    {
        conjPlayer.SetActive(p_status);
    }

    public bool GetCanMoving()
    {
        return canMoving;
    }
}
