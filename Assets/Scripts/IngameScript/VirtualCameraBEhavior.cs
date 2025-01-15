using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class VirtualCameraBEhavior : MonoBehaviour
{
    public GameObject Disc;
    public Camera Camera;


    public Vector3 offset = new Vector3(0, 5, -6);
    private void OnEnable()
    {
        if (!DiscBehavior.instance.isFlying)
        {
            Vector3 desiredPosition = Disc.transform.position
                                  + Disc.transform.up * offset.y
                                  + Disc.transform.forward * offset.z
                                  + Disc.transform.right * offset.x;

            transform.position = desiredPosition;

            // Camera luôn nhìn về phía đĩa theo hướng forward
            transform.LookAt(Disc.transform.position, Disc.transform.up);

        }
    }
}
