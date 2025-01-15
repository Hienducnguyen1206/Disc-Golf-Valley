using System;
using UnityEngine;

public class DiscBehavior : MonoBehaviour
{
    public static Action CollisionWithBasket;

    public bool isFlying;
    public Rigidbody _rb;
    public static DiscBehavior instance;

    private float stopThreshold = 0.2f;
    private float stopTimeThreshold = 2.5f;
    private float stopTimer;
    private bool isTouchingSurface;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        isFlying = false;
        isTouchingSurface = false;
        stopTimer = 0f;
    }

    void Update()
    {
        if (isFlying && isTouchingSurface)
        {
            if (_rb.velocity.magnitude < stopThreshold && _rb.angularVelocity.magnitude < stopThreshold)
            {
                stopTimer += Time.deltaTime;
            }
            else
            {
                stopTimer = 0f;
            }

            if (stopTimer >= stopTimeThreshold)
            {
                isFlying = false;
                stopTimer = 0f;

               
                GameSystem.instance.SetNewPosition(ConvertPosition(transform.position));
                UIManager.Instance.ingameMenu.gameObject.SetActive(true);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            isTouchingSurface = true;

            if (collision.gameObject.GetComponent<BasketBehavior>() != null)
            {
                Debug.Log("Disc touched the basket. Checking winner...");
                CollisionWithBasket?.Invoke(); // Gọi sự kiện chạm giỏ
                GameSystem.instance.CheckGameWinner(); // Kiểm tra người chiến thắng ngay lập tức
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision != null)
        {
            isTouchingSurface = false;
            stopTimer = 0f;
        }
    }

    public Vector3 ConvertPosition(Vector3 position)
    {
        return new Vector3(position.x, position.y + 10f, position.z);
    }
}
