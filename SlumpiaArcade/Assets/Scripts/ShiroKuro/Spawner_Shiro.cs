using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script for spawning Shiro's rolling ball */

public class Spawner_Shiro : MonoBehaviour
{
    /*
    private float westX = -6f;                                  // standard(X) for spawned left
    private float eastX = 6f;                                   // standard(X) for spawned right
    private float northY = 6f;                                  // standard(Y) for spawned up
    private float southY = -6f;                                 // standard(Y) for spawned down
    */

    public GameObject bulletPrefab;                             // rolling ball's prefab for instantiate
    private GameObject[] bullets;                               // GameObject[] : balls to be spawned

    private Vector2 poolPosition = new Vector2(-20, -20);       // balls instantiate position
    private Quaternion prefabRotation = Quaternion.Euler(0, 0, 270);

    private int bulletIndex;
    [SerializeField] private int bulletCount = 10;              // ball's maximum count for instantiate

    // Start is called before the first frame update
    void Start()
    {
        bullets = new GameObject[bulletCount];

        /*
        // decide ball's rotation degree considering its spawn position. Needed if spawner locates every edge of field
        if (transform.position.x < westX)
        {
            ;
        }
        if (transform.position.x > eastX)
        {
            prefabRotation = Quaternion.Euler(0, 0, 180);
        }
        if (transform.position.y < southY)
        {
            prefabRotation = Quaternion.Euler(0, 0, 90);
        }
        if (transform.position.y > northY)
        {
            // to be honest, Shiro's ball only spawns above
            prefabRotation = Quaternion.Euler(0, 0, 270);
        }
        */

        for (int i = 0; i < bulletCount; i++)
        {
            bullets[i] = Instantiate(bulletPrefab, poolPosition, prefabRotation);
        }

        bulletIndex = 0;
    }

    public void Shoot()
    {
        /*
         Shoot Shiro's ball from spawner
         */

        // each three spawners locate above field, spawn ball below spawner to shoot downward
        bullets[bulletIndex].transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1);

        bullets[bulletIndex].SetActive(false);
        bullets[bulletIndex].SetActive(true);

        bulletIndex++;
        if(bulletIndex >= bulletCount)
        {
            bulletIndex = 0;
        }
    }
}
