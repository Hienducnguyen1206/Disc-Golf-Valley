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
    public Button JoinRoomBtn;




    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        CreateRoomBtn.onClick.AddListener(CreateRoom);
        JoinRoomBtn.onClick.AddListener(JoinRoom);
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




    // Update is called once per frame
    void Update()
    {
        
    }
}
