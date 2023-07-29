using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Shiro : MonoBehaviour
{
    private float westX = -6f;
    private float eastX = 6f;
    private float northY = 6f;
    private float southY = -6f;

    public GameObject bulletPrefab;
    private GameObject[] bullets;

    private Vector2 poolPosition = new Vector2(-20, -20);
    private Quaternion prefabRotation;

    private int bulletIndex;
    [SerializeField] private int bulletCount = 10;

    // Start is called before the first frame update
    void Start()
    {
        bullets = new GameObject[bulletCount];

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
            bullets[i] = Instantiate(bulletPrefab, poolPosition, prefabRotation);
        }

        bulletIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
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
