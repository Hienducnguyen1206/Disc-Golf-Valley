using System.Collections.Generic;
using System.IO;
using System.Collections;
using UnityEngine;
using Unity.VisualScripting;
using Photon.Pun;

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
                    if (PlayerPrefs.HasKey("Score")) 
                    {
                        if (int.Parse(PlayerPrefs.GetString("Score")) < Score)
                        {
                            PlayerPrefs.SetString("Score", Score.ToString());
                            PlayerPrefs.Save();
                        }
                    }
                    else 
                    {
                        PlayerPrefs.SetString("Score", Score.ToString());
                        PlayerPrefs.Save();
                    }
                }
                else
                {
                    Score = Mathf.RoundToInt(Mathf.Clamp01((totalDistance - currentDistance) / totalDistance) * 99);

                    if (Score >= 95 && !isHistorySaved)
                    {   
                        isHistorySaved = true;
                        Debug.Log("Wingame");
                        isWinner = true;
                        SaveHistory();
                    }

                    if (PlayerPrefs.HasKey("Score")) 
                    {
                        if (int.Parse(PlayerPrefs.GetString("Score")) < Score)
                        {
                            PlayerPrefs.SetString("Score", Score.ToString());
                            PlayerPrefs.Save();
                        }
                    }
                    else 
                    {
                        PlayerPrefs.SetString("Score", Score.ToString());
                        PlayerPrefs.Save();
                    }
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
        FirebaseAuthManager.instance.CurrentPlayerData.AddHistory( new GameHistory(roomName, Score, isWinner));
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