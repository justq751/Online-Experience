using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MyBullet : MonoBehaviourPun
{
    public bool movingDirection;
    public float moveSpeed = 8f;
    public float destroyTime = 2f;
    public float bulletDamage = 0.3f;

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(destroyTime);
        this.GetComponent<PhotonView>().RPC("Destroy", RpcTarget.AllBuffered);
    }

    private void Update()
    {
        if (!movingDirection)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
    }

    [PunRPC]
    public void ChangeDirection()
    {
        movingDirection = true;
    }

    [PunRPC]
    void Destroy()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!photonView.IsMine)
        {
            return;
        }

        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        if (target != null && (target.IsMine || target.IsSceneView))
        {
            if (target.tag == "Player")
            {
                target.RPC("HealthUpdate", RpcTarget.AllBuffered, bulletDamage);
            }
            this.GetComponent<PhotonView>().RPC("Destroy", RpcTarget.AllBuffered);
        }
    }
}