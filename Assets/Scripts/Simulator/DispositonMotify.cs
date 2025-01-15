using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using DG.Tweening;

public class DispositionModify : MonoBehaviour
{
    
    public Vector3 initialPosition; 
    public float initialAngleX;
    public float initialAngleY;
    public float initialAngleZ;     
    private Tween moveTween;         
    private Tween rotateTween;

    public static DispositionModify instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
        initialPosition = GameSystem.instance.StartPosition;
        initialAngleX = transform.eulerAngles.x;
        initialAngleZ = transform.eulerAngles.y;
        initialAngleZ = transform.eulerAngles.z;

      
        UIManager.Instance.offSetSlider.onValueChanged.AddListener(OnOffsetSliderChanged);

        UIManager.Instance.angleXSlider.onValueChanged.AddListener(OnAngleXSliderChanged);
        UIManager.Instance.angleYSlider.onValueChanged.AddListener(OnAngleYSliderChanged);
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



    private void OnAngleYSliderChanged(float value)
    {
        float targetAngleY = initialAngleY + value;


        if (rotateTween != null && rotateTween.IsActive())
        {
            rotateTween.Kill();
        }


        rotateTween = transform.DORotate(
            new Vector3(transform.eulerAngles.x, targetAngleY, transform.eulerAngles.z),
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
