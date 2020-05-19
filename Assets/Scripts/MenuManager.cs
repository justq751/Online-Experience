using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class MenuManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject UserNameScreen, ConnectScreen;
    [SerializeField] private GameObject CreateUserNameButton;
    [SerializeField] private InputField UserNameInput, CreateRoomInput, JoinRoomInput;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() //first called after successful connection
    {
        Debug.Log("Connected to master!");
        PhotonNetwork.JoinLobby(TypedLobby.Default); //check documentation
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Connected to lobby!");
        UserNameScreen.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        //play game scene
        PhotonNetwork.LoadLevel(1);
    }

    #region UIMethods
    public void OnClick_CreateNameButton()
    {
        PhotonNetwork.NickName = UserNameInput.text;
        UserNameScreen.SetActive(false);
        ConnectScreen.SetActive(true);
    }

    public void OnNameFieldChanged()
    {
        if(UserNameInput.text.Length >= 2)
        {
            CreateUserNameButton.SetActive(true);
        }
        else
        {
            CreateUserNameButton.SetActive(false);
        }
    }

    public void OnClick_JoinRoom()
    {
        RoomOptions roomops = new RoomOptions();
        roomops.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(JoinRoomInput.text, roomops, TypedLobby.Default);
    }

    public void OnClick_CreateRoom()
    {
        RoomOptions roomops = new RoomOptions();
        roomops.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(CreateRoomInput.text, roomops, null);
    }
    #endregion
}