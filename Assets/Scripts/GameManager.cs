using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float timeAmount = 5f;
    private bool startRespawn;

    public GameObject playerPrefab;
    public GameObject canvas;
    public GameObject sceneCamera;
    public GameObject leaveScreen;

    public Text pingRate;
    public Text spawnTimer;
    public GameObject respawnUI;
    [HideInInspector] public GameObject localPlayer;

    public static GameManager instance = null;

    private void Awake()
    {
        instance = this;
        canvas.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleLeaveScreen();
        }
        if (startRespawn)
        {
            StartRespawn();
        }
        pingRate.text = "Network ping: " + PhotonNetwork.GetPing();
    }

    public void StartRespawn()
    {
        timeAmount -= Time.deltaTime;
        spawnTimer.text = "Respawn in: " + timeAmount.ToString("F0");

        if (timeAmount <= 0)
        {
            respawnUI.SetActive(false);
            startRespawn = false;
            PlayerRelocation();
            localPlayer.GetComponent<Health>().EnableInputs();
            localPlayer.GetComponent<PhotonView>().RPC("Revive", RpcTarget.AllBuffered);
        }
    }

    public void EnableRespawn()
    {
        timeAmount = 5f;
        startRespawn = true;
        respawnUI.SetActive(true);
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

    public void PlayerRelocation()
    {
        float randomPos = Random.Range(-5f, 5f);
        localPlayer.transform.localPosition = new Vector2(randomPos, 2);
    }

    public void ToggleLeaveScreen()
    {
        if (leaveScreen.activeSelf)
        {
            leaveScreen.SetActive(false);
        }
        else
        {
            leaveScreen.SetActive(true);
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }
}
