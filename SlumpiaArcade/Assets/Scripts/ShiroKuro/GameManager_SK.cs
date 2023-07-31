using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager_SK : MonoBehaviour
{
    public static GameManager_SK instance;

    public bool isGameOver;

    public bool isPaused;
    public bool isPaneled;

    private float score;

    public GameObject gameplayUI;
    public TMP_Text currentScore;
    public GameObject gameoverUI;
    public TMP_Text finalScore;
    public GameObject bestRecordUI;
    public GameObject gamepauseUI;

    public TMP_Text healthText;

    [Header("Standard Settings")]
    [SerializeField] private int HealthPointSet = 3;
    private int hp;

    private Rigidbody2D playerRigidbody;

    private void Awake()
    {
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
        isGameOver = false;

        isPaused = false;
        isPaneled = false;

        score = 0;

        hp = HealthPointSet;
        healthText.text = "Life : x" + hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            return;
        }
        score += Time.deltaTime;
        currentScore.text = "";
        currentScore.text += score;

        if (Input.GetButtonDown("Cancel"))
        {
            if (isPaused)
            {
                if (isPaneled)
                {
                    ;
                }
                else
                {
                    resumeGame();
                }
            }
            else
            {
                pauseGame();
            }
        }
    }

    public void OnPlayerDead()
    {
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
        hp--;
        healthText.text = "Life : x" + hp;

        if (hp == 0)
        {
            OnPlayerDead();
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        gamepauseUI.SetActive(true);
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        FindObjectOfType<PauseMenu_SK>().gameObject.SetActive(false);
    }
}
