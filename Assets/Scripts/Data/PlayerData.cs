using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public string PlayerName;
    public List<GameHistory> History;

   
    public PlayerData()
    {    
        History = new List<GameHistory>();
    }

    public PlayerData(string playerName)
    {   
        PlayerName = playerName;
        History = new List<GameHistory>();
    }




    public PlayerData(string playerName, List<GameHistory> histories)
    {
        PlayerName = playerName;
        History = histories;
    }

   
    public void AddHistory(GameHistory history)
    {
        History.Add(history);
    }
}

[System.Serializable]
public struct GameHistory
{
    public string RoomName;
    public int Score;
    public bool IsWinner;
    public string Timestamp;


   
   
    public GameHistory(string roomName, int score, bool isWinner)
    {
        RoomName = roomName;
        Score = score;
        IsWinner = isWinner;
        Timestamp = System.DateTime.Now.ToString();
    }
}
