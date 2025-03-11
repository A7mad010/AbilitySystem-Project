using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
   [Header("Manager")]
    public string playerName;
    public bool playerFocuse;

    [Header("Movement")]
    [SerializeField] float speedMove;
    [SerializeField] float durationSpeed = 3;
    [SerializeField] float slowDownDuration = 3;
    [SerializeField] AnimationCurve accelerationMove;
    [SerializeField] AnimationCurve decelerationCurve;
    float m_elapsedTime = 0;
    bool stop = false;

    [Header("Jump")]
    [SerializeField] float jumpForce;
    //[SerializeField] float cutMultyJump = 0.5f;

    //Coyote
    [SerializeField] float coyoteTimer = 0.2f;
    float m_coyoteLastTime;

    //Beffer
    [SerializeField] float bufferDuration = 0.2f;
    float m_bufferTime;

    ////Dubble Jump
    //private bool canJumpMore = false;
    //public bool learnDubleJump = false;

    [Header("Check Jump")]
    public LayerMask groundMask;
    [SerializeField] Transform checkPostion;
    [SerializeField] float checkRadius = 0.2f;
    private bool m_isGrounded = true;

    //[Header("Dash")]
    //[SerializeField] float dashSpeed;
    //[SerializeField] float dashDuration;
    //[SerializeField] float dashCoolDown = 3;
    //float m_dashTime;
    //bool m_isDashing = false;
    //bool m_canDash = true;

    public Rigidbody2D playerRig;

    private void Start()
    {
        playerRig = GetComponent<Rigidbody2D>();

        GameInput.Instance.jump.performed += PlayerJump;
        //GameInput.Instance.dash.performed += PlayerDash;
    }

    private void FixedUpdate()
    {
        if (!playerFocuse /*|| m_isDashing*/) return;
        PlayerMove(GameInput.Instance.MoveMent().x);
        PlayerOnGround();
    }

    public void PlayerMove(float move)
    {
        float magX = move;

        if (Mathf.Abs(magX) > 0)
        {
            m_elapsedTime += Time.deltaTime;
            float t = m_elapsedTime / durationSpeed;
            float targetSpeed = magX * (speedMove * accelerationMove.Evaluate(t));

            playerRig.linearVelocity = new Vector2(targetSpeed, playerRig.linearVelocity.y);

            transform.localScale = new Vector3(magX >= 0 ? 1 : -1, transform.localScale.y, transform.localScale.z);

            stop = false;
        }
        else
        {
            m_elapsedTime = 0;

            if (!stop)
            {
                StopCoroutine(Deceleration());
                StartCoroutine(Deceleration());

                stop = true;
            }
        }
    }
    IEnumerator Deceleration()
    {
        while(Mathf.Abs(playerRig.linearVelocity.x) < 0.1f)
        {
            float slowT = Mathf.Clamp01(Time.deltaTime / slowDownDuration);
            float slowFactor = decelerationCurve.Evaluate(slowT);

            playerRig.linearVelocity = new Vector2(playerRig.linearVelocity.x * slowFactor, playerRig.linearVelocity.y);

            yield return null;
        }
    }

    private void PlayerJump(InputAction.CallbackContext context)
    {
        //m_bufferTime = Time.time;
        if (!playerFocuse /*|| m_isDashing*/) return;

        if (PlayerOnGround() || (Time.time - m_coyoteLastTime) < coyoteTimer)
        {
            StartCoroutine(JumpBeffer());
        }
    }

    IEnumerator JumpBeffer()
    {
        float jump = jumpForce;
        m_bufferTime = bufferDuration;

        while (m_bufferTime > 0 && GameInput.Instance.jump.IsPressed())
        {
            playerRig.linearVelocity = new Vector2(playerRig.linearVelocity.x, jump);
            m_bufferTime -= Time.deltaTime;

            yield return null;
        }

        //if (learnDubleJump) canJumpMore = true;
    }

    private bool PlayerOnGround()
    {
        bool onGround = m_isGrounded = Physics2D.OverlapCircle(checkPostion.position, checkRadius, groundMask) && Mathf.Abs(playerRig.linearVelocity.y) < 0.01f;

        if (onGround)
            m_coyoteLastTime = Time.time;

        return onGround;
    }

    //public void PlayerDash(InputAction.CallbackContext context)
    //{
    //    if (!playerFocuse || m_isDashing) return;

    //    if (m_canDash)
    //    {
    //        Debug.Log("Dash");
    //        StartCoroutine(Dash());
    //    }
    //}

    //IEnumerator Dash()
    //{
    //    //Setup Dash
    //    m_canDash = false;
    //    m_isDashing = true;
    //    float originalGravity = m_playerRig.gravityScale;
    //    m_playerRig.gravityScale = 0;

    //    //Start Move Dash
    //    m_playerRig.linearVelocity = new Vector2(transform.localScale.x * dashSpeed,0);

    //    //Wait before stop
    //    yield return new WaitForSeconds(dashDuration);


    //    //Stop
    //    m_playerRig.linearVelocity = new Vector2(GameInput.Instance.MoveMent().x, m_playerRig.linearVelocity.y);
    //    m_playerRig.gravityScale = originalGravity;
    //    m_isDashing = false;

    //    yield return new WaitForSeconds(dashCoolDown);
    //    m_canDash = true;
    //}

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(checkPostion.position, checkRadius);
    }
#endif
}
