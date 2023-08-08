using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script for Launching Shiro's Bombs to field */

public class BombLauncher : MonoBehaviour
{
    public GameObject[] bombPrefabs;                            // three types of bomb prefabs for instantiate
    private GameObject[] bombs;                                 // GameObject[] : instantiated bombs to be launched

    public int normalBomb;                                      // normal bomb's launching count                        
    public int slowBomb;                                        // slow bomb's launching count
    public int homingBomb;                                      // homing(stun) bomb's launching count

    [Header("Bomb Settings")]
    [SerializeField] public int normalBombCnt = 6;              // normal bomb's maximum count for instantiate
    [SerializeField] public int slowBombCnt = 3;                // slow bomb's maximum count for instantiate
    [SerializeField] public int homingBombCnt = 2;              // homing(stun) bomb's maximum count for instantiate

    [SerializeField] public int normalBombSet = 4;              // normal bomb's initial launching count
    [SerializeField] public int slowBombSet = 1;                // slow bomb's initial launching count
    [SerializeField] public int homingBombSet = 1;              // homing(stun) bomb's initial launching count

    private Vector2 poolPosition = new Vector2(20, -20);        // bombs instantiate position

    // Start is called before the first frame update
    void Start()
    {
        // bombs size : sum of three types of bombs maximum count
        bombs = new GameObject[normalBombCnt + slowBombCnt + homingBombCnt];

        // set bomb's count to initial value
        normalBomb = normalBombSet;
        slowBomb = slowBombSet;
        homingBomb = homingBombSet;

        for (int i = 0; i < normalBombCnt; i++)
        {
            // instantiate normal bomb
            bombs[i] = Instantiate(bombPrefabs[0], poolPosition, Quaternion.Euler(0,0,Random.Range(0, 360)));
            // set active false to mute explosion audio
            bombs[i].SetActive(false);
        }
        for (int i = normalBombCnt; i < normalBombCnt + slowBombCnt; i++)
        {
            // instantiate slow bomb
            bombs[i] = Instantiate(bombPrefabs[1], poolPosition, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            bombs[i].SetActive(false);
        }
        for (int i = normalBombCnt + slowBombCnt; i < normalBombCnt + slowBombCnt + homingBombCnt; i++)
        {
            // instantiate homing(stun) bomb
            bombs[i] = Instantiate(bombPrefabs[2], poolPosition, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            bombs[i].SetActive(false);
        }
    }

    public void Launch()
    {
        /*
         Launch each type of bomb to field
         */

        for (int i = 0; i < normalBomb; i++)
        {
            // launch normal bombs
            bombs[i].transform.position = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));

            bombs[i].SetActive(false);
            bombs[i].SetActive(true);
        }
        for (int i = normalBombCnt; i < normalBombCnt + slowBomb; i++)
        {
            // launch slow bombs
            bombs[i].transform.position = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));

            bombs[i].SetActive(false);
            bombs[i].SetActive(true);
        }
        for (int i = normalBombCnt + slowBombCnt; i < normalBombCnt + slowBombCnt + homingBomb; i++)
        {
            // launch homing(stun) bombs
            bombs[i].transform.position = PlayerMovement_SK.instance.transform.position;

            bombs[i].SetActive(false);
            bombs[i].SetActive(true);
        }
    }
}
