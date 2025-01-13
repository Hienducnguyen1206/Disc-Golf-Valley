using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;
using Photon.Realtime;

public class GamePhotonNetwork : MonoBehaviourPunCallbacks
{  

    public TextMeshProUGUI textMeshPro;
    public TMP_InputField ipRoomName;
    public Button CreateRoomBtn;
    public TextMeshProUGUI playerName;
    public TMP_InputField editNameField;


    public static GamePhotonNetwork instance;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        CreateRoomBtn.onClick.AddListener(CreateRoom);
       
    }


    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
        textMeshPro.text = "Loading...";
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        textMeshPro.text = "Connected";
        string randomName = GenerateRandomName(10);

        
        PhotonNetwork.NickName = randomName;

        playerName.text = PhotonNetwork.NickName;
     
      

    }


    public void CreateRoom()
    {  
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 5;
        PhotonNetwork.CreateRoom(ipRoomName.text,roomOptions);
    }


    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(ipRoomName.text);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Join room successfully");
        PhotonNetwork.LoadLevel("PlayGameScene");
    }

    public void JoinRoomByName(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(message);
    }



    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("Create room success");
        PhotonNetwork.LoadLevel("PlayGameScene");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log(message);
    }

    private string GenerateRandomName(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        System.Random random = new System.Random();
        char[] stringChars = new char[length];

        for (int i = 0; i < length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        return new string(stringChars);
    }



    public void Editname()
    {
        editNameField.text = PhotonNetwork.NickName;

    }


    public void SaveName()
    {
        PhotonNetwork.NickName = editNameField.text;
        playerName.text = PhotonNetwork.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
