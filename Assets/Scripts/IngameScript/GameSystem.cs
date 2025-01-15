using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class GameSystem : MonoBehaviourPunCallbacks
{  
    public bool isWinner;

    public GameObject Disc;
    public GameObject Basket;

   
    public Vector3 NewPosition;
    public Vector3 CurrentPosition;
    public Vector3 StartPosition;


  //  int numberofthrowns = 1;

    public static GameSystem instance;

    public CinemachineVirtualCamera virtualCamera;

    public PhotonView photonView;

    public Gamemode gamemode = Gamemode.RoomMasterWin;

    private void Awake()
    {
        instance = this;
        StartPosition = new Vector3(-85f,20f, -283f);
    }


    void Start()
    {
        isWinner = false;
        photonView = GetComponent<PhotonView>();
        InitStartPosition();

    }



    public void OnEnable()
    {
        DiscBehavior.CollisionWithBasket += Endgame;
    }


    
    void Update()
    {
        
    }


    public void OnDisable()
    {
        DiscBehavior.CollisionWithBasket -= Endgame;
    }


    public void Endgame()
    {
       
    }

    
    public void InitStartPosition()
    {   
        Disc.GetComponent<Rigidbody>().useGravity = false;
        Disc.transform.position = StartPosition;
     ;
        CurrentPosition = StartPosition;
        
    }


    public void ReThrow()
    {
        Disc.GetComponent<Rigidbody>().useGravity = false;
        Disc.transform.position = CurrentPosition;

        virtualCamera.gameObject.SetActive(false);
       

        DispositionModify.instance.initialPosition = CurrentPosition;
        DispositionModify.instance.initialAngleX = 0;
        DispositionModify.instance.initialAngleY = 0;
        DispositionModify.instance.initialAngleZ = 0;

        Disc.transform.rotation = Quaternion.Euler(0, Disc.transform.eulerAngles.y, 0);


        Vector3 directionToBasket = Basket.transform.position - Disc.transform.position;


        Disc.transform.rotation = Quaternion.LookRotation(new Vector3(directionToBasket.x, 0, directionToBasket.z));




    }

    public void MoveToNewPosition()
    {
        virtualCamera.gameObject.SetActive(false);
        Disc.GetComponent<Rigidbody>().useGravity = false;

  
        
        CurrentPosition = NewPosition;

        Disc.transform.position = NewPosition;

        DispositionModify.instance.initialPosition = NewPosition;
        DispositionModify.instance.initialAngleX = 0;
        DispositionModify.instance.initialAngleY = 0;
        DispositionModify.instance.initialAngleZ = 0;

        Disc.transform.rotation = Quaternion.Euler(0, Disc.transform.eulerAngles.y, 0);

        Vector3 directionToBasket = Basket.transform.position - Disc.transform.position;
        Disc.transform.rotation = Quaternion.LookRotation(new Vector3(directionToBasket.x, 0, directionToBasket.z));
    }



    public void SetNewPosition(Vector3 newPosition)
    {
        NewPosition = newPosition;
    }

    public void CheckGameWinner()
    {
        switch (gamemode)
        {
            case Gamemode.RoomMasterWin:
                if (PhotonNetwork.IsMasterClient)
                {
                  
                    ActiveWinPopup();
                    SaveGameHistory();

                
                    photonView.RPC("ActiveLosePopup", RpcTarget.Others);
                    photonView.RPC("SaveGameHistory", RpcTarget.Others);
                }
                else
                {
                    
                    
                }
                break;

            case Gamemode.RoomMemberWin:
                
                if (photonView.IsMine)
                {
                
                    ActiveWinPopup();
                    SaveGameHistory();
                    photonView.RPC("ActiveLosePopup", RpcTarget.Others);
                    photonView.RPC("SaveGameHistory", RpcTarget.Others);
                }
                else
                {
                    
                    
                }
                break;
        }
    }


    [PunRPC]
    public void ActiveLosePopup()
    {  
        isWinner = false;
        UIManager.Instance.loseGameMenu.SetActive(true);
    }

    [PunRPC]
    public void ActiveWinPopup()
    {
        isWinner = true;
        UIManager.Instance.winGameMenu.SetActive(true);
    }


    [PunRPC]
    public void SaveGameHistory()
    {
        FirebaseAuthManager.instance.CurrentPlayerData.AddHistory(new GameHistory(PhotonNetwork.CurrentRoom.Name,100,isWinner));
        FirebaseAuthManager.instance.SavePlayerData(FirebaseAuthManager.instance.auth.CurrentUser.UserId);
    }



    
}
