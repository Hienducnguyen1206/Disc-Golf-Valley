using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    public static Avatar instance;

    private void Awake()
    {
        instance = this;
    }
}
