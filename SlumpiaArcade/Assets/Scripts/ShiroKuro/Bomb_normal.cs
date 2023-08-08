using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script for Shiro's normal bomb */

public class Bomb_normal : MonoBehaviour
{
    [Header("Bomb Settings")]
    [SerializeField] private float warnDuration = 3f;                   // Warning red circle's duration time
    [SerializeField] private float effectDuration = 0.02f;              // Bombs explosion affects Player during 'effectDuration'
    [SerializeField] private float animDuration = 1f;                   // Bombs explosion animation left during 'animDuration'

    public GameObject warningCircle;                                    // Gameobject : Warning red circle
    public GameObject explodeAnim;                                      // Gameobject : explosion animation

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    private Color32 transparent = new Color32(255, 255, 255, 0);        // transparent color (alpha : 0) for making bomb transparent when playing explosion animation
    private Color32 origin = new Color32(255, 255, 255, 255);           // original color for re-loading bomb

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
        /*
         Coroutine for bombs warning and explosion
         */

        spriteRenderer.color = origin;
        warningCircle.SetActive(false);
        warningCircle.SetActive(true);

        yield return new WaitForSeconds(warnDuration);

        // changing 'warningCircle's tag to "Enemy", collision damages player
        warningCircle.gameObject.tag = "Enemy";
        // play explosion audioClip
        audioSource.Play();

        yield return new WaitForSeconds(effectDuration);

        // changing 'warningCircle's tag to "Untagged", collision doesn't affect player
        warningCircle.gameObject.tag = "Untagged";

        spriteRenderer.color = transparent;
        warningCircle.SetActive(false);
        explodeAnim.SetActive(false);
        explodeAnim.SetActive(true);

        yield return new WaitForSeconds(animDuration);

        explodeAnim.SetActive(false);
        // after every routine set gameObject's activation to false
        gameObject.SetActive(false);
    }
}
