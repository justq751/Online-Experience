using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject canvas;
    public GameObject sceneCamera;
    public Text pingRate;

    private void Awake()
    {
        canvas.SetActive(true);
    }

    private void Update()
    {
        pingRate.text = "Network ping: " + PhotonNetwork.GetPing();
    }

    public void SpawnPlayer()
    {
        float randomValue = Random.Range(-5, 5);
        PhotonNetwork.Instantiate(playerPrefab.name, 
            new Vector2(playerPrefab.transform.position.x*randomValue, playerPrefab.transform.position.y), 
            Quaternion.identity, 0);
        canvas.SetActive(false);
        sceneCamera.SetActive(false);
    }
}
