using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] StringEvent pickUpEvent;
    [SerializeField] GameObject pickupPrefab = null;
    [SerializeField] AudioClip pickupSound;
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
        print(collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<Player>(out Player player))
        {
            pickUpEvent.RaisedEvent(tag);
        }
        Instantiate(pickupPrefab, transform.position, Quaternion.identity);
        
        this.GameObject().GetComponent<AudioSource>().PlayOneShot(pickupSound);
        gameObject.active = false;
    }
}
