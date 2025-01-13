using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    public bool IsDestroyOnClose = false;

    protected RectTransform m_RectTransform;
    private Animator m_Animator;
    private float m_OffsetY = 0;

    private void Start()
    {
        OnInit();
    }

    protected void OnInit()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_Animator = GetComponent<Animator>();

        float ratio = (float)Screen.height / (float)Screen.width;
        if (ratio > 2.1f)
        {
            Vector2 leftBottom = m_RectTransform.offsetMin;
            Vector2 rightTop = m_RectTransform.offsetMax;
            rightTop.y = -100f;
            m_RectTransform.offsetMax = rightTop;
            leftBottom.y = 0f;
            m_RectTransform.offsetMin = leftBottom;
            m_OffsetY = 100f;
        }
    }

    public virtual void Setup()
    {
        GUIManager.Ins.AddBackUI(this);
        GUIManager.Ins.PushBackAction(this, BackKey);
    }

    public virtual void BackKey()
    {

    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    public virtual void CloseDirectly()
    {
        GUIManager.Ins.RemoveBackUI(this);
        gameObject.SetActive(false);
        if (IsDestroyOnClose)
        {
            Destroy(gameObject);
        }
        
    }

    public virtual void Close(float delayTime)
    {
        Invoke(nameof(CloseDirectly), delayTime);
    }

}
