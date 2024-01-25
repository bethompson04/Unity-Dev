using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField][Range(0, 90)]float defaultPitch = 40;
    [SerializeField][Range(1,10)]float distance = 5;
    [SerializeField][Range(0.1f, 100.0f)] float sensitivity = 1;

    float yaw = 0;
    float pitch = 0;

    private void Start()
    {
        pitch = defaultPitch;
    }
    void Update()
    {
        yaw+= Input.GetAxis("Mouse X") * sensitivity;

        Quaternion qyaw = Quaternion.AngleAxis(yaw, Vector3.up);
        Quaternion qpitch = Quaternion.AngleAxis(pitch, Vector3.right);
        Quaternion rotation = qyaw * qpitch;

        transform.position = target.position + (rotation * Vector3.back * distance);
        transform.rotation = rotation;
    }
}
