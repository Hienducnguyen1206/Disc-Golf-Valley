using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public string PlayerName;
    public string AvatarCode;
    public List<GameHistory> History;

   
    public PlayerData()
    {    
        History = new List<GameHistory>();
        AvatarCode = "F1";
    }

    public PlayerData(string playerName)
    {   
        PlayerName = playerName;
        History = new List<GameHistory>();
        AvatarCode = "F1";
    }




    public PlayerData(string playerName, List<GameHistory> histories)
    {
        PlayerName = playerName;
        History = histories;
        AvatarCode = "1";
    }

   
    public void AddHistory(GameHistory history)
    {
        History.Add(history);
    }

    public void ChangeAvatar(string avatarCode)
    {
        AvatarCode = avatarCode;
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
