
using UnityEngine;




[CreateAssetMenu(fileName = "NewDiscStats", menuName = "Disc Golf/Disc Stats")]
public class DiscStats : ScriptableObject
{
    [Header("Disc Properties")]
    [Range(1, 14)] public int SPEED;  
    [Range(1, 7)] public int GLIDE;   
    [Range(-5, 1)] public int TURN;   
    [Range(0, 5)] public int FADE;   

    [TextArea] public string description; 

    
}
