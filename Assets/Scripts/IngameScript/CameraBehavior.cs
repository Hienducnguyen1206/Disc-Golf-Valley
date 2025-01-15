using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public GameObject Disc; // Đối tượng đĩa mà camera sẽ theo dõi
    public Vector3 offset = new Vector3(0, 5, -6); // Offset mặc định (cao hơn 5, sau 6)

    void LateUpdate()
    {
        if (Disc == null)
        {
            Debug.LogWarning("Disc object is not assigned.");
            return;
        }

        if (!DiscBehavior.instance.isFlying)
        {
            // Tính toán vị trí mới của camera dựa trên offset và hướng của đĩa
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
