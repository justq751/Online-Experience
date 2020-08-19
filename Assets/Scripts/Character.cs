using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Character : MonoBehaviourPun
{
    public GameObject playerCamera;
    public SpriteRenderer sprite;
    public Animator animator;
    public PhotonView photonView;
    public GameObject bulletPrefab;
    public Transform bulletSpawnpointRight;
    public Transform bulletSpawnpointLeft;
    public Text playerName;
    

    private bool allowMovement = true;
    public float moveSpeed = 1.0f;

    private void Awake()
    {
        if (photonView.IsMine)
        {
            playerCamera.SetActive(true);
            playerName.text = "You: " + PhotonNetwork.NickName;
            playerName.color = Color.green;
        }
        else
        {
            playerName.text = photonView.Owner.NickName;
            playerName.color = Color.red;
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
        if (sprite.flipX == false)
        {
            GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, new Vector2(bulletSpawnpointRight.position.x, bulletSpawnpointRight.position.y), Quaternion.identity, 0);
        }
        if (sprite.flipX == true)
        {
            GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, new Vector2(bulletSpawnpointLeft.position.x, bulletSpawnpointLeft.position.y), Quaternion.identity, 0);
            bullet.GetComponent<PhotonView>().RPC("ChangeDirection", RpcTarget.AllBuffered);
        }
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