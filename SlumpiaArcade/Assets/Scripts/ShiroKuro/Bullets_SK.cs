using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets_SK : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float warnDuration = 3f;
    [SerializeField] private float fireDuration = 2f;
    [SerializeField] private float fireSpeed = 10f;
    private bool isWarned = false;

    public GameObject warningLine;

    private float westX = -4.5f;
    private float eastX = 4.5f;
    private float northY = 4.5f;
    private float southY = -4.5f;

    private Vector3 fireDirection;

    private void OnEnable()
    {
        isWarned = false;

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

        StartCoroutine("WarnAndFire");
    }

    private void Update()
    {
        if (isWarned && (transform.position.x > -7 && transform.position.x < 7 && transform.position.y > -7 && transform.position.y < 7))
        {
            transform.Translate(Vector3.right * fireSpeed * Time.deltaTime);
        }
    }

    private IEnumerator WarnAndFire()
    {
        warningLine.SetActive(false);
        warningLine.SetActive(true);

        yield return new WaitForSeconds(warnDuration);
        isWarned = true;

        warningLine.SetActive(false);

        yield return new WaitForSeconds(fireDuration);
        isWarned = false;

    }
}
