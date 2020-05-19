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
}
