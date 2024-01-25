using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    //[SerializeField] TMP_Text scoreText;

    [SerializeField] Slider boost;
    [SerializeField] GameObject boostEffect;
    [SerializeField] UnityEngine.AudioClip boostSound;

    [SerializeField] Transform cameraTransform;
    [SerializeField] FloatVariable health;
    [SerializeField] FloatVariable speed;
    [SerializeField] PhysicsCharacterController characterController;
    [Header("Events")]
    //[SerializeField] IntEvent scoreEvent = default;
    [SerializeField] VoidEvent gameStartEvent = default;
    [SerializeField] VoidEvent playerDeadEvent = default;


    //private int score = 0;

    // public int Score { 
    //     get { return score; } 
    //     set {score = value;
    //      scoreText.text = score.ToString();
    //       scoreEvent.RaisedEvent(score);
    //       } 
    //       }

    private void OnEnable()
    {
        gameStartEvent.Subscribe(OnStartGame);
    }

    private void Start()
    {
        health.value = 5.5f;
    }

    private void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        // Grabbed this from a Unity forum
        // - Ends what I got from (https://forum.unity.com/threads/how-do-i-find-the-forward-velocity-of-an-object-and-other-issues.1378059/)
        
        if (Input.GetButton("Boost") && boost.value > 0)
        {
            playerBoost(rb);
        }

        speed.value = rb.velocity.magnitude;

    }

    // public void AddPoints(int points)
    // {
    //     Score += points;
    // }

    private void OnStartGame()
    {
        characterController.enabled = true;
    }
    public void TakeDamage(float damage)
    {
        health.value -= damage;
        if (health.value <= 0)
        {
            playerDeadEvent.RaisedEvent();
        }
    }

    private void playerBoost(Rigidbody rb)
    {
        rb.AddForce(cameraTransform.forward * 50, ForceMode.Force);
        boost.value -= 2 * Time.deltaTime;
        Instantiate(boostEffect, transform.position, Quaternion.identity);
        this.GameObject().GetComponent<AudioSource>().PlayOneShot(boostSound, 0.25f);
        
    }

    public void OnRespawn(GameObject respawn)
    {
        transform.position = respawn.transform.position;
        transform.rotation = respawn.transform.rotation;
        
        characterController.Reset();
    }

}
