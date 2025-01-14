using Photon.Pun;
using UnityEngine;

public class ExitRoom : MonoBehaviourPunCallbacks
{
    public static ExitRoom instance;


    private void Awake()
    {
        instance = this;
    }

    public void ExitToLobby()
    {
       
        PhotonNetwork.LeaveRoom();

     
         PhotonNetwork.LoadLevel("GameScene");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Player has left the room.");
       
    }



    [PunRPC]
    public void NotifyAllPlayersToExitRoom()
    {
       
        PhotonNetwork.LeaveRoom();
        Debug.Log("Player won! Everyone is being kicked out.");
    }


    public void OnPlayerWin()
    {
        if (PhotonNetwork.IsMasterClient) 
        {
           
            photonView.RPC("NotifyAllPlayersToExitRoom", RpcTarget.All);
        }
    }




}
