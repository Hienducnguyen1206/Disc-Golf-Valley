using System.Collections.Generic;
using System.IO;
using System.Collections;
using UnityEngine;
using Unity.VisualScripting;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameManager : Singleton<GameManager>
{
    public GameObject MapPrefab;

    public int Score;

    public string AccountName = "";

    private GameObject mapObject;

    private GameState gameState;

    private Transform zeusTransform;

    private string saveFilePath;

    private bool isWinner = false;

    private bool isHistorySaved = false;

    private void Start()
    {
        if (PlayerPrefs.HasKey("AccountName"))
        {
            string savedAccountName = PlayerPrefs.GetString("AccountName");
            Debug.Log("Loaded Account Name: " + savedAccountName);
        }

        if (mapObject != null)
        {
            Destroy(mapObject);
        }

        mapObject = Instantiate(MapPrefab, Vector3.zero, Quaternion.identity);
        zeusTransform = mapObject.transform.Find("StartPoint/zeus");
        Transform gameplayTransform = mapObject.transform.Find("GUI");
        gameplayTransform.gameObject.SetActive(false);
        Invoke("ShowWelcomeUI", 1.5f);
    }

    private void ShowWelcomeUI()
    {
        ChangeState(GameState.Start);
        GUIManager.Ins.OpenUI<CanvasWelcome>();
    }

    public void UpdateBestScore(int score)
    {
        int currentBestScore = PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("BestScore")
            ? (int)PhotonNetwork.LocalPlayer.CustomProperties["BestScore"]
            : 0;

        if (score > currentBestScore)
        {
            Hashtable newProperties = new Hashtable { { "BestScore", score } };
            PhotonNetwork.LocalPlayer.SetCustomProperties(newProperties);

            Debug.Log("Updated Best Score to: " + score);
        }
    }

    private void LateUpdate()
    {
        if (GameManager.Ins.IsState(GameState.Playing) && zeusTransform != null)
        {
            Rigidbody rb = zeusTransform.GetComponent<Rigidbody>();

            if (rb.velocity.magnitude > 0)
            {
                Transform gameplayTransform = mapObject.transform.Find("GUI");
                gameplayTransform.gameObject.SetActive(false);
            }

            if (rb.velocity.magnitude > 0 && rb.velocity.magnitude < 0.01f)
            {
                Vector3 basketPoint = mapObject.transform.Find("basket").position;
                Vector3 startPoint = new Vector3(-85.099f, 36f, -282.8994f);
                Vector3 currentPoint = zeusTransform.position;
                float totalDistance = Vector3.Distance(startPoint, basketPoint);
                float currentDistance = Vector3.Distance(basketPoint, currentPoint);

                if (currentDistance > totalDistance)
                {
                    Score = 0;
                    PlayerPrefs.SetString("Score", Score.ToString());
                    PlayerPrefs.Save();
                }
                else
                {
                    Score = Mathf.RoundToInt(Mathf.Clamp01((totalDistance - currentDistance) / totalDistance) * 99);

                    if (Score >= 90 && !isHistorySaved)
                    {
                        isHistorySaved = true;
                        Debug.Log("Wingame");
                        isWinner = true;
                        SaveHistory();
                    }

                    PlayerPrefs.SetString("Score", Score.ToString());
                    PlayerPrefs.Save();
                }

                UpdateBestScore(Score);

                foreach (var player in PhotonNetwork.PlayerList)
                {
                    string playerName = player.NickName;
                    int bestScore = player.CustomProperties.ContainsKey("BestScore")
                        ? (int)player.CustomProperties["BestScore"]
                        : 0;

                    Debug.Log($"Player: {playerName}, Best Score: {bestScore}");
                }

                GUIManager.Ins.OpenUI<CanvasScore>();
            }
        }
    }




    private void Update()
    {

    }



    public void SaveHistory()
    {
        string roomName = PhotonNetwork.CurrentRoom?.Name ?? "UnknownRoom";
        FirebaseAuthManager.instance.CurrentPlayerData.AddHistory(new GameHistory(roomName, Score, isWinner));
        FirebaseAuthManager.instance.SavePlayerData(FirebaseAuthManager.instance.auth.CurrentUser.UserId);
    }












    public void Init()
    {
        if (mapObject != null)
        {
            Destroy(mapObject);
        }
        mapObject = Instantiate(MapPrefab, Vector3.zero, Quaternion.identity);
        zeusTransform = mapObject.transform.Find("StartPoint/zeus");
    }

    public void SetActiveGamePlay(bool active)
    {
        mapObject.transform.Find("GUI").gameObject.SetActive(active);
    }

    public void ChangeState(GameState gameState)
    {
        this.gameState = gameState;
    }

    public bool IsState(GameState gameState)
    {
        return this.gameState == gameState;
    }

    public string GetScore()
    {
        return this.Score.ToString();
    }

    public void UpdateAccountName(string name)
    {
        this.AccountName = name;
    }

    /*
    public void AddItem(ItemData item)
    {
        item.Timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        itemList.Insert(0, item);
        SaveData();
    }

    public List<ItemData> GetItemList()
    {
        return itemList;
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(new SaveData(itemList), true);
        File.WriteAllText(saveFilePath, json);
    }

    public void LoadData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            itemList = saveData.itemList ?? new List<ItemData>();
        }
    }

    public ItemData GetItemByName(string name)
    {
        return itemList.Find(item => item.Name == name);
    }

    public void AddOrUpdateItem(string name, int score)
    {
        AddItem(new ItemData(name, score));
        SaveData();
    }

   
    public List<ItemData> GetItemsByName(string name)
    {
        return itemList.FindAll(item => item.Name == name);
    }
     */

}

[System.Serializable]
public class ItemData
{
    public string Name;
    public int Score;
    public string Timestamp;

    public ItemData(string name, int score)
    {
        Name = name;
        Score = score;
        Timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}

[System.Serializable]
public class SaveData
{
    public List<ItemData> itemList;

    public SaveData(List<ItemData> itemList)
    {
        this.itemList = itemList;
    }
}