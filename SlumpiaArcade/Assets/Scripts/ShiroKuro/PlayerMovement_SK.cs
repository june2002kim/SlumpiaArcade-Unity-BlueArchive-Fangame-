using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement_SK : MonoBehaviour
{
    public static PlayerMovement_SK instance;

    public Rigidbody2D _rigidbody;
    private InputProvider _inputProvider;
    private SpriteRenderer _spriteRenderer;
    public AudioSource _audioSource;

    public GameObject shield;
    public GameObject shieldUI;

    //private bool isDead;
    public bool isSlowed;
    public bool isStunned;
    private bool isFacingRight;

    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float slowmoveSpeed = 4f;
    private float currentSpeed;

    [Header("Dash Settings")]
    [SerializeField] float dashSpeed = 25f;
    [SerializeField] float dashDuration = 0.2f;
    [SerializeField] float dashCooldown = 1f;
    private bool isDashing;
    private bool canDash;

    [Header("Shield Settings")]
    [SerializeField] float shieldDuration = 0.7f;
    [SerializeField] float shieldCooldown = 10f;
    private bool canShield;

    [Header("CrowdControl Settings")]
    [SerializeField] float ImmortalTime = 1f;
    [SerializeField] float slowDuration = 2f;
    [SerializeField] float stunDuration = 3f;

    [Header("Audio Clip")]
    public AudioClip DashAudioClip;
    public AudioClip ShieldAudioClip;
    public AudioClip HitAudioClip;

    private void OnEnable()
    {
        _inputProvider = new InputProvider();
        _inputProvider.dashPerformed += startDash;
        _inputProvider.shieldPerformed += startShield;
        _inputProvider.Enable();
    }

    private void OnDisable()
    {
        _inputProvider.dashPerformed -= startDash;
        _inputProvider.shieldPerformed -= startShield;
        _inputProvider.Disable();
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("More than 2 PlayerMovement_ShiroKuro exist in Scene!");
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //isDead = false;
        isSlowed = false;
        isStunned = false;
        isFacingRight = false;
        isDashing = false;
        canDash = true;
        canShield = true;

        dashCooldown = PlayerPrefs.GetFloat("dashCooldownSet");
        dashDuration = PlayerPrefs.GetFloat("dashDurationSet");
        dashSpeed = PlayerPrefs.GetFloat("dashSpeedSet");

        shieldCooldown = PlayerPrefs.GetFloat("shieldCooldownSet");

        currentSpeed = PlayerPrefs.GetFloat("moveSpeedSet");

        ImmortalTime = PlayerPrefs.GetFloat("ImmortalTimeSet");

        shieldUI.GetComponent<Animator>().speed = 0.9f / shieldCooldown;
    }

    private void FixedUpdate()
    {
        if (isDashing || GameManager_SK.instance.isGameOver || isStunned)
        {
            return;
        }

        if (isSlowed)
        {
            currentSpeed = slowmoveSpeed;
            StartCoroutine("Slow");
        }
        
        _rigidbody.velocity = _inputProvider.MovementInput() * currentSpeed;

        Flip();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.tag != "Immortal" && !GameManager_SK.instance.isGameOver)
        {
            if (collision.tag == "Enemy")
            {
                GameManager_SK.instance.OnPlayerDamaged();
                StartCoroutine("Immortal");
            }

            if (collision.tag == "Slow")
            {
                //Debug.Log("is Slowed");
                isSlowed = true;
            }

            if (collision.tag == "Stun")
            {
                //Debug.Log("is Stunned");
                isStunned = true;
                _rigidbody.velocity = Vector2.zero;
                StartCoroutine("Stun");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (this.tag != "Immortal" && !GameManager_SK.instance.isGameOver)
        {
            if (collision.tag == "Enemy")
            {
                GameManager_SK.instance.OnPlayerDamaged();
                StartCoroutine("Immortal");
            }

            if (collision.tag == "Slow")
            {
                //Debug.Log("is Slowed");
                isSlowed = true;
            }

            if (collision.tag == "Stun")
            {
                //Debug.Log("is Stunned");
                isStunned = true;
                _rigidbody.velocity = Vector2.zero;
                StartCoroutine("Stun");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (this.tag != "Immortal" && !GameManager_SK.instance.isGameOver)
        {
            if (collision.tag == "Enemy")
            {
                GameManager_SK.instance.OnPlayerDamaged();
                StartCoroutine("Immortal");
            }

            if (collision.tag == "Slow")
            {
                //Debug.Log("is Slowed");
                isSlowed = true;
            }

            if (collision.tag == "Stun")
            {
                //Debug.Log("is Stunned");
                isStunned = true;
                _rigidbody.velocity = Vector2.zero;
                StartCoroutine("Stun");
            }
        }
    }

    private void Flip()
    {
        if((_inputProvider.MovementInput().x > 0 && !isFacingRight) || (_inputProvider.MovementInput().x < 0 && isFacingRight)){
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Immortal()
    {
        this.tag = "Immortal";
        _spriteRenderer.color = new Color32(255, 255, 255, 100);
        yield return new WaitForSeconds(ImmortalTime);
        _spriteRenderer.color = new Color32(255, 255, 255, 255);
        this.tag = "Player";
    }

    private void startDash(InputAction.CallbackContext context)
    {
        if (canDash && !GameManager_SK.instance.isGameOver && !isStunned && !GameManager_SK.instance.isPaused)
        {
            StartCoroutine("Dash");
        }
        if (GameManager_SK.instance.isGameOver)
        {
            GameManager_SK.instance.restartGame();
        }
    }

    private IEnumerator Dash()
    {
        //Debug.Log("Dashed");
        _audioSource.clip = DashAudioClip;
        _audioSource.Play();


        if(PlayerPrefs.GetInt("isImmortalDash") == 1)
        {
            gameObject.tag = "Immortal";
        }
        canDash = false;
        isDashing = true;
        _rigidbody.velocity = _inputProvider.MovementInput() * dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        gameObject.tag = "Player";
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void startShield(InputAction.CallbackContext context)
    {
        if (canShield && !GameManager_SK.instance.isGameOver && !GameManager_SK.instance.isPaused)
        {
            StartCoroutine("Shield");
        }
    }

    private IEnumerator Shield()
    {
        //Debug.Log("Shield ON");
        _audioSource.clip = ShieldAudioClip;
        _audioSource.Play();

        gameObject.tag = "Immortal";
        shield.SetActive(true);
        
        canShield = false;
        shieldUI.GetComponent<Animator>().SetBool("isUsed", !canShield);

        yield return new WaitForSeconds(shieldDuration);

        gameObject.tag = "Player";
        shield.SetActive(false);

        yield return new WaitForSeconds(shieldCooldown);

        canShield = true;
        shieldUI.GetComponent<Animator>().SetBool("isUsed", !canShield);
    }

    public IEnumerator Slow()
    {
        yield return new WaitForSeconds(slowDuration);
        currentSpeed = moveSpeed;
        isSlowed = false;
    }

    public IEnumerator Stun()
    {
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;
    }
}
