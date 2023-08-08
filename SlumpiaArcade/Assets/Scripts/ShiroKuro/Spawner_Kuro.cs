using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script for spawning Kuro's obstacle */

public class Spawner_Kuro : MonoBehaviour
{
    private float westX = -6f;                                  // standard(X) for spawned left
    private float eastX = 6f;                                   // standard(X) for spawned right
    private float northY = 6f;                                  // standard(Y) for spawned up
    private float southY = -6f;                                 // standard(Y) for spawned down

    public GameObject[] bulletPrefabs;                          // obstacles prefab to be instantiate
    private GameObject[] bullets;                               // GameObject[] : obstacles to be spawned

    private Vector2 poolPosition = new Vector2(-20, -20);       // obstacles instantiate position
    private Quaternion prefabRotation;

    private int bulletIndex;
    [SerializeField] public int bulletCount = 10;               // obstacle's maximum count for instantiate

    // Start is called before the first frame update
    void Start()
    {
        bullets = new GameObject[bulletCount];

        // decide ball's rotation degree considering its spawn position.
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
            prefabRotation = Quaternion.Euler(0, 0, 270);
        }

        for (int i = 0; i < bulletCount; i++)
        {
            // select one obstacle from three types of prefab (cup, horse, car)
            bulletIndex = Random.Range(0, 3);
            bullets[i] = Instantiate(bulletPrefabs[bulletIndex], poolPosition, prefabRotation);
        }

        bulletIndex = 0;
    }

    public void Shoot()
    {
        /*
         Shoot Kuro's obstacle from spawner
         */

        if (transform.position.x < westX)
        {
            // if spawner locates left, spawn obstacle right side

            //Debug.Log("Shoot Right");
            bullets[bulletIndex].transform.position = new Vector2(gameObject.transform.position.x + 1, gameObject.transform.position.y);
        }
        if (transform.position.x > eastX)
        {
            // if spawner locates right, spawn obstacle left side

            //Debug.Log("Shoot Left");
            bullets[bulletIndex].transform.position = new Vector2(gameObject.transform.position.x - 1, gameObject.transform.position.y);
        }
        if (transform.position.y < southY)
        {
            // if spawner locates down side, spawn obstacle above

            //Debug.Log("Shoot Up");
            bullets[bulletIndex].transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1);
        }
        if (transform.position.y > northY)
        {
            // if spawner locates up side, spawn obstacle below

            //Debug.Log("Shoot Down");
            bullets[bulletIndex].transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1);
        }

        bullets[bulletIndex].SetActive(false);
        bullets[bulletIndex].SetActive(true);

        bulletIndex++;
        if(bulletIndex >= bulletCount)
        {
            bulletIndex = 0;
        }
    }
}
