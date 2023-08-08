using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script for Shiro's slow bomb */

public class Bomb_slow : MonoBehaviour
{
    [Header("Bomb Settings")]
    [SerializeField] private float warnDuration = 3f;
    [SerializeField] private float effectDuration = 0.02f;
    [SerializeField] private float animDuration = 1f;

    public GameObject warningCircle;
    public GameObject explodeAnim;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    private Color32 transparent = new Color32(255, 255, 255, 0);
    private Color32 origin = new Color32(255, 255, 255, 255);

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        StartCoroutine("WarnAndExplode");
    }

    private IEnumerator WarnAndExplode()
    {
        spriteRenderer.color = origin;
        warningCircle.SetActive(false);
        warningCircle.SetActive(true);

        yield return new WaitForSeconds(warnDuration);

        // changing 'warningCircle's tag to "Slow", collision slows player
        warningCircle.gameObject.tag = "Slow";
        audioSource.Play();

        yield return new WaitForSeconds(effectDuration);

        warningCircle.gameObject.tag = "Untagged";

        spriteRenderer.color = transparent;
        warningCircle.SetActive(false);
        explodeAnim.SetActive(false);
        explodeAnim.SetActive(true);

        yield return new WaitForSeconds(animDuration);

        explodeAnim.SetActive(false);
        gameObject.SetActive(false);
    }
}
