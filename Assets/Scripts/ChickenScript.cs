using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenScript : MonoBehaviour
{
    [SerializeField]float rotateAngle = 25;
    [SerializeField]float hoverSpeed = 5;

    [SerializeField]float maxHeight = 1.8f;
    [SerializeField]float minHeight = 1.6f;
    bool floatingUp = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.AngleAxis(rotateAngle * Time.deltaTime, Vector3.up);
        
        if(transform.position.y <= maxHeight && floatingUp)
        {

            transform.position += transform.up * hoverSpeed * Time.deltaTime;
        } 
        else if(!floatingUp && transform.position.y >= minHeight)
        {

            transform.position -= transform.up * hoverSpeed * Time.deltaTime;
        }

        if(transform.position.y >= maxHeight)
        {
            floatingUp = false;
        }
        if(transform.position.y <= minHeight)
        {
            floatingUp = true;
        }
    }
}
