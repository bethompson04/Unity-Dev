using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    [SerializeField][Range(-1080, 1080)] float rotateAngle;
    float speed = 5;

    void Update()
    {
        transform.rotation *= Quaternion.AngleAxis(rotateAngle * Time.deltaTime, Vector3.up);
        if(Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
}
