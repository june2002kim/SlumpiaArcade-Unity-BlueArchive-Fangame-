using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script for Kuro's obstacle */

public class Bullets_SK : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float warnDuration = 3f;       // warning red rectangle's duration time. Obstacles start to move after 'warnDuration'
    [SerializeField] private float fireDuration = 2f;       // after firing obstacle, it flies through field during 'fireDuration'
    [SerializeField] private float fireSpeed = 10f;
    private bool isWarned = false;

    public GameObject warningLine;                          // GameObject : warning red rectangle

    /*
    private float westX = -4.5f;                            // standard(X) for spawned left
    private float eastX = 4.5f;                             // standard(X) for spawned right
    private float northY = 4.5f;                            // standard(Y) for spawned up
    private float southY = -4.5f;                           // standard(Y) for spawned down

    private Vector3 fireDirection;                          // obstacles movement direction
    */

    private void OnEnable()
    {
        isWarned = false;

        /*
        // check obstacle's spawn position and its movement direction
        if (transform.position.x < westX)
        {
            //Debug.Log("Warn Right");
            fireDirection = Vector3.right;
        }
        if (transform.position.x > eastX)
        {
            //Debug.Log("Warn Left");
            fireDirection = Vector3.left;
        }
        if (transform.position.y < southY)
        {
            //Debug.Log("Warn Up");
            fireDirection = Vector3.up;
        }
        if (transform.position.y > northY)
        {
            //Debug.Log("Warn Down");
            fireDirection = Vector3.down;
        }
        */

        StartCoroutine("WarnAndFire");
    }

    private void Update()
    {
        // Move obstacle to right side (* Depending Original Prefabs direction, it should move right side even its rotated *)
        if (isWarned && (transform.position.x > -7 && transform.position.x < 7 && transform.position.y > -7 && transform.position.y < 7))
        {
            transform.Translate(Vector3.right * fireSpeed * Time.deltaTime);
        }
    }

    private IEnumerator WarnAndFire()
    {
        /*
         Warn and Fire obstacle
         */

        warningLine.SetActive(false);
        warningLine.SetActive(true);

        yield return new WaitForSeconds(warnDuration);
        
        // start to move (obstacle inside field)
        isWarned = true;

        warningLine.SetActive(false);

        yield return new WaitForSeconds(fireDuration);

        // stop moving (obstacle outside field)
        isWarned = false;

    }
}
