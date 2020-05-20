using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Character : MonoBehaviourPun
{
    public GameObject playerCamera;
    public float moveSpeed = 1.0f;
    public SpriteRenderer sprite;

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
        var movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0);
        transform.position += movement * moveSpeed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.D))
        {
            sprite.flipX = false;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            sprite.flipX = true;
        }
    }
}