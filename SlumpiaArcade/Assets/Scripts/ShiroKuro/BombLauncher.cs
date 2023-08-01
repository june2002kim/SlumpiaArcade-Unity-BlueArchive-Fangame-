using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLauncher : MonoBehaviour
{
    public GameObject[] bombPrefabs;
    private GameObject[] bombs;

    public int normalBomb;
    public int slowBomb;
    public int homingBomb;

    [Header("Bomb Settings")]
    [SerializeField] public int normalBombCnt = 6;
    [SerializeField] public int slowBombCnt = 3;
    [SerializeField] public int homingBombCnt = 2;

    [SerializeField] public int normalBombSet = 4;
    [SerializeField] public int slowBombSet = 1;
    [SerializeField] public int homingBombSet = 1;

    private Vector2 poolPosition = new Vector2(20, -20);

    // Start is called before the first frame update
    void Start()
    {
        bombs = new GameObject[normalBombCnt + slowBombCnt + homingBombCnt];

        normalBomb = normalBombSet;
        slowBomb = slowBombSet;
        homingBomb = homingBombSet;

        for (int i = 0; i < normalBombCnt; i++)
        {
            bombs[i] = Instantiate(bombPrefabs[0], poolPosition, Quaternion.Euler(0,0,Random.Range(0, 360)));
        }
        for (int i = normalBombCnt; i < normalBombCnt + slowBombCnt; i++)
        {
            bombs[i] = Instantiate(bombPrefabs[1], poolPosition, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        }
        for (int i = normalBombCnt + slowBombCnt; i < normalBombCnt + slowBombCnt + homingBombCnt; i++)
        {
            bombs[i] = Instantiate(bombPrefabs[2], poolPosition, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Launch()
    {
        for (int i = 0; i < normalBomb; i++)
        {
            bombs[i].transform.position = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));

            bombs[i].SetActive(false);
            bombs[i].SetActive(true);
        }
        for (int i = normalBombCnt; i < normalBombCnt + slowBomb; i++)
        {
            bombs[i].transform.position = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));

            bombs[i].SetActive(false);
            bombs[i].SetActive(true);
        }
        for (int i = normalBombCnt + slowBombCnt; i < normalBombCnt + slowBombCnt + homingBomb; i++)
        {
            bombs[i].transform.position = PlayerMovement_SK.instance.transform.position;

            bombs[i].SetActive(false);
            bombs[i].SetActive(true);
        }
    }
}
