using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Health : MonoBehaviourPun
{
    public Image fillImage;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public BoxCollider2D boxCol;

    public float health;

    public void CheckHealth()
    {
        if (photonView.IsMine && health <= 0)
        {
            this.GetComponent<PhotonView>().RPC("Death", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    public void Death()
    {
        rb.gravityScale = 0;
        boxCol.enabled = false;
        sr.enabled = false;
    }

    [PunRPC]
    public void Revive()
    {
        rb.gravityScale = 1;
        boxCol.enabled = true;
        sr.enabled = true;
    }

    [PunRPC]
    public void HealthUpdate(float damage)
    {
        fillImage.fillAmount -= damage;
        health = fillImage.fillAmount;
        CheckHealth();
    }
}
