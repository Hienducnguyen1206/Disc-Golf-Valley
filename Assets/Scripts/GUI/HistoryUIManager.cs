using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryUIManager : MonoBehaviour
{
    public GameObject HistoryPrefab;
    public GameObject HistoryContainer;
    void Start()
    {
        UpdateHistoryBoard();
    }

  
    void Update()
    {
        
    }

    public void UpdateHistoryBoard()
    {
       
      ClearAllChildren(HistoryContainer.transform);

        FirebaseAuthManager.instance.LoadPlayerData(FirebaseAuthManager.instance.auth.CurrentUser.UserId);

        foreach (var history in FirebaseAuthManager.instance.CurrentPlayerData.History)
        {
            Debug.Log($"Room Name: {history.RoomName}, Score: {history.Score}, Is Winner: {history.IsWinner}");
        }

        for (int i = 0; i < FirebaseAuthManager.instance.CurrentPlayerData.History.Count; i++)
        {

            GameObject historyEntry = Instantiate(HistoryPrefab, Vector3.zero,Quaternion.identity, HistoryContainer.transform);

           
            historyEntry.transform.Find("Roomname").GetComponent<TMPro.TextMeshProUGUI>().text =  FirebaseAuthManager.instance.CurrentPlayerData.History[i].RoomName.ToString();
            historyEntry.transform.Find("MaxScore").GetComponent<TMPro.TextMeshProUGUI>().text =  FirebaseAuthManager.instance.CurrentPlayerData.History[i].Score.ToString();
            historyEntry.transform.Find("Result").GetComponent<TMPro.TextMeshProUGUI>().text = FirebaseAuthManager.instance.CurrentPlayerData.History[i].IsWinner ? "Winner" : "Loser";
        }
    }


    public  void ClearAllChildren( Transform transform)
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
