using Photon.Pun;
using Photon.Realtime;

using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.UI;


public class RoomListManager : MonoBehaviourPunCallbacks
{

    public GameObject roomPrefabs;
    public GameObject roomContainer;




    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);

        foreach (Transform child in roomContainer.transform)
        {
            Destroy(child.gameObject);
        }


        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].IsOpen && roomList[i].IsVisible && roomList[i].PlayerCount >= 1)
            {
                GameObject room = Instantiate(roomPrefabs, Vector3.zero, Quaternion.identity, roomContainer.transform);
                room.GetComponentInChildren<TextMeshProUGUI>().text = roomList[i].Name;

                string roomName = roomList[i].Name; 
                room.GetComponent<Button>().onClick.AddListener(() =>
                {

                    GamePhotonNetwork.instance.JoinRoomByName(roomName);

                });
            }
        }
    }







}
