using Cinemachine;
using System;

using UnityEngine;

public class DiscFlySimulator : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] DiscStats discStats;
    [SerializeField]
    [Range(1f, 5.0f)] float _force;


    private float _alpha;   // Góc tấn 
    public double ALPHA0 = -4;    // Góc tấn khi lực nâng bằng 0;

    private double roll;  // Momen Xoay quanh trục Z
    private double pitch; // Momen Xoay quanh trục Y
    private double spin;  // Momen Xuay quanh trục X

    public double AREA = 0.0568;  // Diện tích đĩa
    public double CL0 = 0.1;      // Hệ số lực nân khi góc tấn bằng 0;
    public double CLA = 1.4;      // Hệ số lực nâng theo góc tấn 
    public double CD0 = 0.08;     // Hệ số lực cản khi góc tấn bằng 0;
    public double CDA = 2.72;     // Hệ số lực cản theo góc tấn ;


    public double CRR = 0.014;    // Mô men xoay theo trục Z do roll;
    public double CRP = -0.0055;  // Mô men xoay theo trục Z do pitch;
    public double CNR = -0.0000071;  // Mô men xoay theo trục Y do roll;
    public double CM0 = -0.08;      // Mô men nghiêng 
    public double CMA = 0.43;      // Độ nhạy của mô men nghiêng theo góc tấn 
    public double CMq = -0.005;    // Mô men nghiêng theo tốc độ góc
    public double RHO = 1.225f;    // Mật độ không khí ở điều kiện chuẩn 
    public float diameter = 0.21f;  // Đường kính đĩa


    private bool isThrowing = false;

     public CinemachineVirtualCamera virtualCamera;
     public Camera cam;


    private void Tourque()
    {
        roll = (CRR * _rb.angularVelocity.y + CRP * _rb.angularVelocity.x) * 1 / 2 * RHO * Math.Pow(_rb.velocity.magnitude, 2) * AREA * diameter * 0.01f * 6 - discStats.TURN/2f;
        roll -= discStats.FADE*3 ;
        
        spin = -(CNR * _rb.angularVelocity.y) * 1 / 2 * RHO * Math.Pow(_rb.velocity.magnitude, 2) * AREA * diameter * 0.01f;
        pitch = (CM0 + CMA * (Math.PI / 180 * (_alpha)) + CMq * _rb.angularVelocity.z) * 1 / 2 * RHO * Math.Pow(_rb.velocity.magnitude, 2) * AREA * diameter * 0.01f * 6; 




        _rb.AddTorque(Vector3.Cross(transform.up, _rb.velocity).normalized * (float)roll, ForceMode.Acceleration);       
        _rb.AddTorque(transform.up * (float)spin, ForceMode.Acceleration);
        _rb.AddTorque(_rb.velocity.normalized * (float)pitch, ForceMode.Acceleration);
    }

    private void Lift()
    {
        double cl = CL0 + CLA * _alpha * Mathf.Deg2Rad; 
        double lift = (RHO * Math.Pow(_rb.velocity.magnitude, 2) * AREA * cl / 2 / _rb.mass) * Time.deltaTime  + discStats.GLIDE/ 9;
        _rb.AddForce(transform.up.normalized * (float)lift*2, ForceMode.Acceleration);
    }


    private void Gravity()
    {
       _rb.AddForce(Vector3.down * 9.8f, ForceMode.Acceleration);
    }

    private void Drag()
    {
        double cd = CD0 + CDA * Mathf.Pow((float)(_alpha - (ALPHA0) * Math.PI / 180), 2);
        double drag = (RHO * Math.Pow(_rb.velocity.magnitude, 2) * AREA * cd) / 2 * (15 / discStats.SPEED)/2f; 
       _rb.AddForce(-_rb.velocity.normalized * (float)drag, ForceMode.Acceleration);
    }

   
 
      void Start()
    {   

        _rb.maxAngularVelocity = 1000f;
      

        UIManager.Instance.throwBtn.onClick.AddListener(Throw);
        UIManager.Instance.powerSlider.onValueChanged.AddListener(value => { _force = value; });


    }

    void FixedUpdate()
    {
        if (isThrowing && DiscBehavior.instance.isFlying)
        {
            Gravity();
            Lift();
            Tourque();
            Drag();
        }
      
        
    }

    public void Throw()
    {   
        virtualCamera.gameObject.transform.rotation = cam.transform.rotation;
        virtualCamera.gameObject.SetActive(true);


        UIManager.Instance.ControlPanel.gameObject.SetActive(false);


        isThrowing = true;
        DiscBehavior.instance.isFlying = true;
        _alpha = Vector3.Angle(_rb.velocity, transform.forward);
        _rb.AddForce(transform.forward * _force * discStats.SPEED / 2f, ForceMode.Impulse);
        _rb.AddTorque(transform.up * 80f, ForceMode.Impulse);



    }
   







    void Update()
    {
        
    }
}
