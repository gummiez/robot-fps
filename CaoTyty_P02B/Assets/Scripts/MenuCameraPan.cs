using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraPan : MonoBehaviour
{
    [SerializeField] float cameraRotSpeed = 5f;
    Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        rb.transform.Rotate(0, cameraRotSpeed * Time.deltaTime, 0, Space.World);
    }
}
