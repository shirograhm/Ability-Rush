using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        yaw += 2.0f * Input.GetAxis("Mouse X");
        pitch -= 2.0f * Input.GetAxis("Mouse Y");

        transform.parent.gameObject.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
