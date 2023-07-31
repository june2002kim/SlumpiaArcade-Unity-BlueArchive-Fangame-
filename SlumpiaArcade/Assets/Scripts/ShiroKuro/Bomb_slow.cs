using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_slow : MonoBehaviour
{
    [Header("Bomb Settings")]
    [SerializeField] private float warnDuration = 3f;
    [SerializeField] private float effectDuration = 0.02f;

    public GameObject warningCircle;

    private void OnEnable()
    {
        StartCoroutine("WarnAndExplode");
    }

    private IEnumerator WarnAndExplode()
    {
        warningCircle.SetActive(false);
        warningCircle.SetActive(true);

        yield return new WaitForSeconds(warnDuration);

        warningCircle.gameObject.tag = "Slow";

        yield return new WaitForSeconds(effectDuration);

        warningCircle.gameObject.tag = "Untagged";
        gameObject.SetActive(false);
    }
}
