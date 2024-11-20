using System;

using UnityEngine;

public class DiscFlySimulator : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] DiscStats discStats;
    [SerializeField]
    [Range(1f, 5.0f)] float _force;


    private float _alpha;
    private double roll;
    private double pitch;
    private double spin;

    public double AREA = 0.0568; 
    public double CL0 = 0.1; 
    public double CLA = 1.4; 
    public double CD0 = 0.08;  
    public double CDA = 2.72; 
    public double ALPHA0 = -4;

    public double CRR = 0.014;
    public double CRP = -0.0055;
    public double CNR = -0.0000071;
    public double CM0 = -0.08;
    public double CMA = 0.43;
    public double CMq = -0.005;
    public double RHO = 1.225f;
    public float diameter = 0.21f;


    private bool isThrowing = false;

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

   
 
     public void Start()
    {   

        _rb.maxAngularVelocity = 1000f;
        Throw();
    }

    public void FixedUpdate()
    {
        if (isThrowing)
        {
            Gravity();
            Lift();
            Tourque();
            Drag();
        }
      
        
    }

    public void Throw()
    {
        isThrowing = true;
        _alpha = Vector3.Angle(_rb.velocity, transform.forward);
        _rb.AddForce(transform.forward * _force * discStats.SPEED / 2f, ForceMode.Impulse);
        _rb.AddTorque(transform.up * 80f, ForceMode.Impulse);
    }
   







    void Update()
    {
        
    }
}
