using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] Slider boost;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject titleUI;
    [SerializeField] GameObject winUI;
    [SerializeField] GameObject loseUI;
    [SerializeField] TMP_Text livesUI;
    [SerializeField] TMP_Text timerUI;
    [SerializeField] TMP_Text speedUI;
    [SerializeField] Slider healthUI;

    [SerializeField] FloatVariable health;
    [SerializeField] FloatVariable speed;
    [SerializeField] GameObject respawn;
    [Header("Events")]
    //[SerializeField] IntEvent scoreEvent;

    [SerializeField] IntEvent scoreEvent = default;
    [SerializeField] VoidEvent gameStartEvent;
    [SerializeField] GameObjectEvent respawnEvent;
    public enum State
    {
        TITLE,
        START_GAME,
        PLAY_GAME,
        GAME_WIN,
        GAME_OVER
    }

    private int score = 0;
    public int Score { 
        get { return score; } 
        set {
            score = value;
            scoreText.text = score.ToString();
            scoreEvent.RaisedEvent(score);
        } 
    }

    public State state = State.TITLE;
    public float timer = 0;
    int lives = 0;
    public int Lives { get { return lives; } set { lives = value; livesUI.text = "Lives: " + lives.ToString();}}
    public float Timer { get { return timer;} set { timer = value; timerUI.text = string.Format("{0:F1}", timer);}}
    public float Speed {get {return speed.value;} set {speed.value = value; speedUI.text = string.Format("{0:F1}", speed.value);}}

    bool reload = false;

    private void OnEnable()
    {
        //scoreEvent.Subscribe(OnAddPoints);
    }

    private void OnDisable()
    {
        //scoreEvent.Unsubscribe(OnAddPoints);
    }
    void Start()
    {
        gameStartEvent.Subscribe(onStartGame);
    }


    void Update()
    {
        switch (state)
        {
            
            case State.TITLE:
                if(reload)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    reload = false;
                }
                titleUI.SetActive(true);
                loseUI.SetActive(false);
                winUI.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            break;
            case State.START_GAME:
                titleUI.SetActive(false);
                Timer = 20;
                Lives = 3;
                health.value = 100;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                gameStartEvent.RaisedEvent();
                respawnEvent.RaisedEvent(respawn);
                state = State.PLAY_GAME;
            break;
            case State.PLAY_GAME:   
                Timer = Timer - Time.deltaTime;
                Speed = speed.value;
                if (Timer <= 0)
                {
                    state = State.GAME_OVER;
                }
            break;
            case State.GAME_WIN:
            {
                winUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                reload = true;
                break;
            } 
            case State.GAME_OVER: 
            Lives = Lives - 1;
            if(lives > 0)
            {
                OnPlayerDead();
                
            }else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                loseUI.SetActive(true);
                reload = true;
            }
            break;
        }

        healthUI.value = health.value / 100.0f;
    }



    public void onStartGame()
    {
        state = State.START_GAME;
    }

    public void onToTitle()
    {
        state = State.TITLE;
    }

    public void OnPlayerDead()
    {
        state = State.PLAY_GAME;
        respawnEvent.RaisedEvent(respawn);
        timer = 20;
        print(Lives);
        print(lives);
    }

    public void OnAddPoints(int points)
    {
        print(points);
    }

    public void OnGrabCoin(string tag)
    {
        switch(tag)
        {
            case "Coin" : 
            {
                score += 10; 
                print(score);
                scoreText.text = score.ToString();
                break;
            }
            case "BoostCoin": boost.value = 1; break;
            case "TimeCoin" : timer += 10; break;
        }
    }

    public void OnPlayerWin()
    {
        state = State.GAME_WIN;
    }
}
