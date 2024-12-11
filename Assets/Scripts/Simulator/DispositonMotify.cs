using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using DG.Tweening;

public class DispositionModify : MonoBehaviour
{
    
    private Vector3 initialPosition; 
    private float initialAngleX;     
    private float initialAngleZ;     
    private Tween moveTween;         
    private Tween rotateTween;      

    void Start()
    {
       
        initialPosition = transform.position;
        initialAngleX = transform.eulerAngles.x;
        initialAngleZ = transform.eulerAngles.z;

      
        UIManager.Instance.offSetSlider.onValueChanged.AddListener(OnOffsetSliderChanged);
        UIManager.Instance.angleXSlider.onValueChanged.AddListener(OnAngleXSliderChanged);
        UIManager.Instance.angleZSlider.onValueChanged.AddListener(OnAngleZSliderChanged);
    }

    private void OnOffsetSliderChanged(float value)
    {
        float targetX = initialPosition.x + value;
        Vector3 targetPosition = new Vector3(targetX, initialPosition.y, initialPosition.z);

  
        if (moveTween != null && moveTween.IsActive())
        {
            moveTween.Kill();
        }

       
        moveTween = transform.DOMove(targetPosition, 0.5f).SetEase(Ease.OutQuad);
    }

    private void OnAngleXSliderChanged(float value)
    {
        float targetAngleX = initialAngleX + value;

       
        if (rotateTween != null && rotateTween.IsActive())
        {
            rotateTween.Kill();
        }

       
        rotateTween = transform.DORotate(
            new Vector3(targetAngleX, transform.eulerAngles.y, transform.eulerAngles.z),
            0.5f 
        ).SetEase(Ease.OutQuad);
    }

    private void OnAngleZSliderChanged(float value)
    {
        float targetAngleZ = initialAngleZ + value;

    
        if (rotateTween != null && rotateTween.IsActive())
        {
            rotateTween.Kill();
        }

   
        rotateTween = transform.DORotate(
            new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, targetAngleZ),
            0.5f
        ).SetEase(Ease.OutQuad);
    }
}
