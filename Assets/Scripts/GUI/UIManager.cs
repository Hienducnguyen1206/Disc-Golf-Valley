using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{   
    public static UIManager Instance;

    public Slider offSetSlider;
    public Slider angleXSlider;
    public Slider angleYSlider;
    public Slider angleZSlider;
    public Slider powerSlider;
    public Button throwBtn;
    public FixedJoystick fixedJoystick;
    [SerializeField] TextMeshProUGUI offSetText;
    [SerializeField] TextMeshProUGUI angleXText;
    [SerializeField] TextMeshProUGUI angleYText;
    [SerializeField] TextMeshProUGUI angleZText;
    [SerializeField] TextMeshProUGUI powerText;


    public GameObject winGameMenu;
    public GameObject loseGameMenu;
    public GameObject ingameMenu;
    public GameObject ControlPanel;





    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        offSetText.text = offSetSlider.value.ToString();
        angleXText.text = angleXSlider.value.ToString();
        angleYText.text = angleYSlider.value.ToString();
        angleZText.text = angleZSlider.value.ToString();
        powerText.text = powerSlider.value.ToString();
    }
}
