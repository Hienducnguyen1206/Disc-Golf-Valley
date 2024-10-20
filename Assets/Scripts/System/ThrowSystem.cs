using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowSystem : MonoBehaviour
{
    [SerializeField]
    [Range(-30f,30f)] float DiscThrowDeg = 0f ;
    [SerializeField]
    [Range(0, 10f)] float Offset = 0f;
    [SerializeField]
    [Range(0, 10f)] float ThrowPower = 0f;

    [SerializeField] Slider OffSet;
    [SerializeField] Slider Angle;
    [SerializeField] Slider Power;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeDiscState()
    {

    }

    public void ChangeDisc() { }

    public void Throw()
    {

    }


}
