using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{   
    public Slider sfxSlider;
    public Slider musicSlider;
    public Toggle sfxToggle;
    public Toggle musicToggle;
    

   public static LobbyUIManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
