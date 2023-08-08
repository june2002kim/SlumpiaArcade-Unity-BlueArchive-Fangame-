using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

/* Script for GameManager */

public class GameManager_SK : MonoBehaviour
{
    public static GameManager_SK instance;

    public bool isGameOver;

    public bool isPaused;
    public bool isPaneled;

    private int hitDamage;
    private float score;

    public GameObject gameplayUI;
    public TMP_Text currentScore;
    public GameObject gameoverUI;
    public TMP_Text finalScore;
    public GameObject bestRecordUI;
    public GameObject gamepauseUI;

    public TMP_Text healthText;

    [Header("Standard Settings")]
    //[SerializeField] private int HealthPointSet = 3;
    [SerializeField] private float healthRegenCooldown = 45f;
    private int hp;
    private float startTime;
    private float recentHitTime;

    private Rigidbody2D playerRigidbody;
    private AudioSource playerAudiosource;
    private AudioSource gameAudiosource;
    private AudioClip playerHitClip;

    private void Awake()
    {
        // Singleton
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("More than 2 ShiroKuro_GameManager exist in Scene!");
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // restore timeScale to '1' for restarting and resuming after paused 
        Time.timeScale = 1;

        gameAudiosource = GetComponent<AudioSource>();

        isGameOver = false;

        isPaused = false;
        isPaneled = false;

        score = 0;

        // set HP considering ability
        hp = PlayerPrefs.GetInt("healthPointSet");
        healthText.text = "" + hp;

        startTime = Time.time;
        recentHitTime = Time.time;

        // set hit damage considering ability
        if (PlayerPrefs.GetInt("isHealthRegen") == 1)
        {
            hitDamage = 2;
        }
        else
        {
            hitDamage = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            return;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            if (isPaused)
            {
                if (isPaneled)
                {
                    // when 'cancel' button downed pasued game and panel menu is loaded -> stay paused
                    ;
                }
                else
                {
                    // when 'cancel' button downed paused game -> resume game
                    resumeGame();
                }
            }
            else
            {
                // when 'cancel' button downed playing game -> pause game
                pauseGame();
            }
        }

        // set score to playing time
        score = Time.time - startTime;
        currentScore.text = "";
        // round up using System.Math
        currentScore.text += System.Math.Round(score, 3);

        if(PlayerPrefs.GetInt("isHealthRegen") == 1)
        {
            if(hp < PlayerPrefs.GetInt("healthPointSet"))
            {
                if(Time.time >= recentHitTime + healthRegenCooldown)
                {
                    // if player has 'HealthRegen' ability and it has been passed 'healthRegenCooldown' from 'recentHitTime', regenerate health
                    recentHitTime = Time.time;
                    hp++;
                    healthText.text = "" + hp;
                }
            }
        }
    }

    public void OnPlayerDead()
    {
        /* 
         When player is dead, let game over and show 'gameoverUI' and save its score 
         */

        isGameOver = true;

        playerRigidbody = FindObjectOfType<PlayerMovement_SK>()._rigidbody;
        playerRigidbody.velocity = Vector2.zero;

        //Debug.Log("GameOvered");

        finalScore.text = "Score :" + score;

        gameplayUI.SetActive(false);
        gameoverUI.SetActive(true);

        if (score > PlayerPrefs.GetFloat("ShiroKuroRecord"))
        {
            PlayerPrefs.SetFloat("ShiroKuroRecord", score);
            bestRecordUI.SetActive(true);
        }
    }

    public void OnPlayerDamaged()
    {
        /*
         When player is damaged, play 'HitAudioClip' and reduce player's health
         */

        playerAudiosource = FindObjectOfType<PlayerMovement_SK>()._audioSource;
        playerAudiosource.clip = FindObjectOfType<PlayerMovement_SK>().HitAudioClip;
        playerAudiosource.Play();

        hp = hp - hitDamage;
        healthText.text = "" + hp;

        // for calculating health regeneration
        recentHitTime = Time.time;

        if (hp <= 0)
        {
            // when player's health becomes lower than 1, game over
            OnPlayerDead();
        }
    }

    public void restartGame()
    {
        /*
         Restart game by releading current scene
         */

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void pauseGame()
    {
        /*
         Pause game and audio
         */

        Time.timeScale = 0;
        isPaused = true;
        gameAudiosource.Pause();

        gamepauseUI.SetActive(true);
    }

    public void resumeGame()
    {
        /*
         Resume game and audio
         */

        Time.timeScale = 1;
        isPaused = false;
        gameAudiosource.Play();

        FindObjectOfType<PauseMenu_SK>().gameObject.SetActive(false);
    }
}
