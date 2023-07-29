using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement_SK : MonoBehaviour
{
    public static PlayerMovement_SK instance;

    private Rigidbody2D _rigidbody;
    private InputProvider _inputProvider;

    private bool isDead;
    private bool isSlowed;
    private bool isStunned;

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
    [SerializeField] float slowDuration = 2f;
    [SerializeField] float stunDuration = 3f;

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
        isDead = false;
        isSlowed = false;
        isStunned = false;
        isDashing = false;
        canDash = true;
        canShield = true;

        currentSpeed = moveSpeed;
    }

    private void FixedUpdate()
    {
        if (isDashing || isDead || isStunned)
        {
            return;
        }

        if (isSlowed)
        {
            currentSpeed = slowmoveSpeed;
            StartCoroutine("Slow");
        }

        _rigidbody.velocity = _inputProvider.MovementInput() * currentSpeed;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.tag != "Immortal" && !isDead)
        {
            if (collision.tag == "Enemy")
            {
                isDead = true;
                _rigidbody.velocity = Vector2.zero;
                GameManager_SK.instance.OnPlayerDead();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (this.tag != "Immortal" && !isDead)
        {
            if (collision.tag == "Enemy")
            {
                isDead = true;
                _rigidbody.velocity = Vector2.zero;
                GameManager_SK.instance.OnPlayerDead();
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
                StartCoroutine("Stun");
            }
        }
    }

    private void startDash(InputAction.CallbackContext context)
    {
        if (canDash && !isDead)
        {
            StartCoroutine("Dash");
        } 
    }

    private IEnumerator Dash()
    {
        //Debug.Log("Dashed");
        gameObject.tag = "Immortal";
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
        if (canShield && !isDead)
        {
            StartCoroutine("Shield");
        }
    }

    private IEnumerator Shield()
    {
        //Debug.Log("Shield ON");
        gameObject.tag = "Immortal";
        canShield = false;
        yield return new WaitForSeconds(shieldDuration);
        gameObject.tag = "Player";
        yield return new WaitForSeconds(shieldCooldown);
        canShield = true;
    }

    private IEnumerator Slow()
    {
        yield return new WaitForSeconds(slowDuration);
        currentSpeed = moveSpeed;
        isSlowed = false;
    }

    private IEnumerator Stun()
    {
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;
    }
}
