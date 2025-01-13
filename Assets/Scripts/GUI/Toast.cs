using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Toast : MonoBehaviour
{
    public static Toast Instance { get; private set; }
    public TextMeshProUGUI text;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
}
