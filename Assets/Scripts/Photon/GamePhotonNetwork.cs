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
        FirebaseAuthManager.instance.LoadPlayerData(FirebaseAuthManager.instance.auth.CurrentUser.UserId);
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

        InitPlayerName();    
        if (playerName) playerName.text = PhotonNetwork.NickName;
   

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
        RecyclableScrollerDemo recyclableScroller = FindObjectOfType<RecyclableScrollerDemo>();
        recyclableScroller?.UpdateContactList();

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

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log($"Player {newPlayer.NickName} joined the room.");

        RecyclableScrollerDemo recyclableScroller = FindObjectOfType<RecyclableScrollerDemo>();
        recyclableScroller?.UpdateContactList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log($"Player {otherPlayer.NickName} left the room.");

        RecyclableScrollerDemo recyclableScroller = FindObjectOfType<RecyclableScrollerDemo>();
        recyclableScroller?.UpdateContactList();
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

 



    public void Editname()
    {    

         
          editNameField.text = "";
                 
       
    }


    public void SaveName()
    {  
        PhotonNetwork.NickName = editNameField.text;
        FirebaseAuthManager.instance.CurrentPlayerData.PlayerName = editNameField.text;
        FirebaseAuthManager.instance.SavePlayerData(FirebaseAuthManager.instance.auth.CurrentUser.UserId);
        
        if (playerName) playerName.text = PhotonNetwork.NickName;
    }

    public void UpdatePlayerScore(int newScore)
    {
        var properties = new ExitGames.Client.Photon.Hashtable { { "Score", newScore } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(properties);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void InitPlayerName()
    {
        FirebaseAuthManager.instance.LoadPlayerData(FirebaseAuthManager.instance.auth.CurrentUser.UserId);
        playerName.text = FirebaseAuthManager.instance.CurrentPlayerData.PlayerName;
    }

    public void InitPlayerAvatar()
    {
        FirebaseAuthManager.instance.LoadPlayerData(FirebaseAuthManager.instance.auth.CurrentUser.UserId);
        Avatar.instance.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Avatar/" + FirebaseAuthManager.instance.CurrentPlayerData.AvatarCode);
    }
}
