using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandTerrain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
      
        Rigidbody otherRb = collision.gameObject.GetComponent<Rigidbody>();
        if (otherRb != null)
        {
            if(otherRb.velocity.magnitude<0.01f )
            {
                otherRb.velocity = Vector3.zero;
            }
            else
            {
                otherRb.velocity *= 0.3f;
            }
        

            if(otherRb.angularVelocity.magnitude < 0.01f)
            {
                otherRb.angularVelocity = Vector3.zero;
            }
            else
            {
                otherRb.angularVelocity *= 0.013f;
            }
           

        }
    }
}
