using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* Script for managing player's movement using Unity's new InputSystem */

public class PlayerMovement_SK : MonoBehaviour
{
    public static PlayerMovement_SK instance;

    public Rigidbody2D _rigidbody;
    private InputProvider _inputProvider;
    private SpriteRenderer _spriteRenderer;
    public AudioSource _audioSource;
    private TrailRenderer _trailRenderer;

    public GameObject shield;
    public GameObject shieldUI;

    //private bool isDead;
    public bool isSlowed;
    public bool isStunned;
    private bool isFacingRight;

    [SerializeField] float slowmoveSpeed = 4f;
    private float currentSpeed;

    [Header("Dash Settings")]
    [SerializeField] float dashSpeed = 25f;
    [SerializeField] float dashDuration;
    [SerializeField] float dashCooldown;
    private bool isDashing;
    private bool canDash;
    private Color32 trailStartColor = new Color32(144, 235, 254, 255);
    private Color32 trailEndColor = new Color32(255, 255, 255, 0);

    [Header("Shield Settings")]
    [SerializeField] float shieldDuration = 0.7f;
    [SerializeField] float shieldCooldown;
    private bool canShield;

    [Header("CrowdControl Settings")]
    [SerializeField] float ImmortalTime;
    [SerializeField] float slowDuration = 2f;
    [SerializeField] float stunDuration = 3f;

    [Header("Audio Clip")]
    public AudioClip DashAudioClip;
    public AudioClip ShieldAudioClip;
    public AudioClip HitAudioClip;

    private void OnEnable()
    {
        /*
         New input system uses event for each situation
         */

        _inputProvider = new InputProvider();
        _inputProvider.dashPerformed += startDash;
        _inputProvider.shieldPerformed += startShield;
        _inputProvider.Enable();
    }

    private void OnDisable()
    {
        /*
         New input system uses event for each situation
         */

        _inputProvider.dashPerformed -= startDash;
        _inputProvider.shieldPerformed -= startShield;
        _inputProvider.Disable();
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        _trailRenderer = GetComponent<TrailRenderer>();

        // Singleton
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
        // set basic values from 'PlayerPrefs'

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

        // set trail for dash
        trailOn();
        // turn trail off in common situation
        trailOff();
    }

    private void FixedUpdate()
    {
        if (isDashing || GameManager_SK.instance.isGameOver || isStunned)
        {
            // prevent moving when player 'is dasing' or 'game is over' or 'is stunned'
            return;
        }

        if (isSlowed)
        {
            // when player collides with slow bomb, slower movespeed
            currentSpeed = slowmoveSpeed;
            StartCoroutine("Slow");
        }
        
        // players basic movement
        _rigidbody.velocity = _inputProvider.MovementInput() * currentSpeed;

        // flipping sprite for left and right
        Flip();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if player is 'immortal' or game is over, doesn't detect trigger

        if (this.tag != "Immortal" && !GameManager_SK.instance.isGameOver)
        {
            if (collision.tag == "Enemy")
            {
                // when collides with 'Enemy', starts 'OnPlayerDamaged()'
                GameManager_SK.instance.OnPlayerDamaged();
                // also starts coroutine 'Immortal()' for invincible time
                StartCoroutine("Immortal");
            }

            if (collision.tag == "Slow")
            {
                // when collides with 'Slow', set 'isSlowed' to 'true'

                //Debug.Log("is Slowed");
                isSlowed = true;
            }

            if (collision.tag == "Stun")
            {
                // when collides with 'Stun', set 'isStunned' to 'true', stops player's move and starts 'Stun()'

                //Debug.Log("is Stunned");
                isStunned = true;
                _rigidbody.velocity = Vector2.zero;
                StartCoroutine("Stun");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        /*
         Trigger Stay for staying inside bomb's explosion area
        Note that rigidbody's 'NeverSleep' should be selected to detect every time
         */

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
        /*
         Trigger Exit to detect getting out of bomb's explosion area
         */

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
        /*
         Flipt player's sprite using localScale
         */

        if((_inputProvider.MovementInput().x > 0 && !isFacingRight) || (_inputProvider.MovementInput().x < 0 && isFacingRight)){
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Immortal()
    {
        /*
         Invincible Time
         */

        this.tag = "Immortal";
        // makes player little bit transparent to notice it has been damaged
        _spriteRenderer.color = new Color32(255, 255, 255, 100);

        yield return new WaitForSeconds(ImmortalTime);

        _spriteRenderer.color = new Color32(255, 255, 255, 255);
        this.tag = "Player";
    }

    private void startDash(InputAction.CallbackContext context)
    {
        /*
         When dasing is possible, starts Dash
         */

        if (canDash && !GameManager_SK.instance.isGameOver && !isStunned && !GameManager_SK.instance.isPaused)
        {
            StartCoroutine("Dash");
        }
        if (GameManager_SK.instance.isGameOver)
        {
            // used dash button for restart button to makes it work also in Mobile Platform
            GameManager_SK.instance.restartGame();
        }
    }

    private IEnumerator Dash()
    {
        /*
         Dash
         */

        //Debug.Log("Dashed");

        // dash audio
        _audioSource.clip = DashAudioClip;
        _audioSource.Play();

        // turn on trail
        trailOn();

        if(PlayerPrefs.GetInt("isImmortalDash") == 1)
        {
            // If ability is 'ImmortalDash' let it be 'Immortal' when dashing
            gameObject.tag = "Immortal";
        }
        // let dash impossible for cooldown
        canDash = false;
        // prevent moving when dasing
        isDashing = true;

        // dash
        _rigidbody.velocity = _inputProvider.MovementInput() * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        // turn off trail
        trailOff();

        isDashing = false;
        gameObject.tag = "Player";

        yield return new WaitForSeconds(dashCooldown);

        // let dash possible again
        canDash = true;
    }

    private void startShield(InputAction.CallbackContext context)
    {
        /*
         When shield is possible, starts Shield
         */

        if (canShield && !GameManager_SK.instance.isGameOver && !GameManager_SK.instance.isPaused)
        {
            StartCoroutine("Shield");
        }
    }

    private IEnumerator Shield()
    {
        /*
         Shield
         */

        //Debug.Log("Shield ON");

        // shield audio
        _audioSource.clip = ShieldAudioClip;
        _audioSource.Play();

        gameObject.tag = "Immortal";
        shield.SetActive(true);

        // let shield impossible for cooldown
        canShield = false;
        // showing shield UI's cooldown in screen
        shieldUI.GetComponent<Animator>().SetBool("isUsed", !canShield);

        yield return new WaitForSeconds(shieldDuration);

        gameObject.tag = "Player";
        shield.SetActive(false);

        yield return new WaitForSeconds(shieldCooldown);

        // let shield possible again
        canShield = true;

        shieldUI.GetComponent<Animator>().SetBool("isUsed", !canShield);
    }

    public IEnumerator Slow()
    {
        /*
         Slow for 'slowDuration' and restore its original speed
         */

        yield return new WaitForSeconds(slowDuration);

        currentSpeed = PlayerPrefs.GetFloat("moveSpeedSet");
        isSlowed = false;
    }

    public IEnumerator Stun()
    {
        /*
         Stun for 'stunDuration' and restore its movement
         */

        yield return new WaitForSeconds(stunDuration);

        isStunned = false;
    }

    private void trailOn()
    {
        /*
         Set player's trail
         */

        _trailRenderer.startColor = trailStartColor;
        _trailRenderer.endColor = trailEndColor;
        _trailRenderer.startWidth = 0.8f;
        _trailRenderer.endWidth = 0f;
    }

    private void trailOff()
    {
        /*
         Turn off player's trail by setting every color transparent
         */

        _trailRenderer.startColor = trailEndColor;
    }
}
