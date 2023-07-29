using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_SK : MonoBehaviour
{
    public static GameManager_SK instance;

    public bool isGameOver;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerDead()
    {
        isGameOver = true;
        Debug.Log("GameOvered");
    }
}
