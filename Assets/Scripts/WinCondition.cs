using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [SerializeField] VoidEvent victoryEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            victoryEvent.RaisedEvent();
        }
    }
}
