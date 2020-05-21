using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Character : MonoBehaviourPun
{
    public GameObject playerCamera;
    public SpriteRenderer sprite;
    public Animator animator;
    public PhotonView photonView;
    private bool allowMovement = true;

    public float moveSpeed = 1.0f;

    private void Awake()
    {
        if (photonView.IsMine)
        {
            playerCamera.SetActive(true);
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            CheckInput();
        }
    }

    private void CheckInput()
    {
        if (allowMovement)
        {
            var movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0);
            transform.position += movement * moveSpeed * Time.deltaTime;
        }
        
        if (Input.GetKeyDown(KeyCode.RightControl) && !animator.GetBool("IsMoving"))
        {
            Shoot();
        }
        else if (Input.GetKeyUp(KeyCode.RightControl))
        {
            animator.SetBool("IsShooting", false);
            allowMovement = true;
        }

        if (Input.GetKeyDown(KeyCode.D) && !animator.GetBool("IsShooting")) //TEMP SOLUTION FOR FLIPPING AND MOVING
        {
            animator.SetBool("IsMoving", true);
            photonView.RPC("FlipSprite_Right", RpcTarget.AllBuffered);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("IsMoving", false);
        }
        if (Input.GetKeyDown(KeyCode.A) && !animator.GetBool("IsShooting"))
        {
            animator.SetBool("IsMoving", true);
            photonView.RPC("FlipSprite_Left", RpcTarget.AllBuffered);
            
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("IsMoving", false);
        }
    }

    private void Shoot()
    {
        animator.SetBool("IsShooting", true);
        allowMovement = false;
    }

    [PunRPC]
    private void FlipSprite_Right()
    {
        sprite.flipX = false;
    }

    [PunRPC]
    private void FlipSprite_Left()
    {
        sprite.flipX = true;
    }
}