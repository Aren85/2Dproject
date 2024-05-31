using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    public PhysicsCheck physicsCheck;
    private Rigidbody2D rb;
    public Vector2 inputDirection;
    [Header("基本參數")]
    public float speed;
    public float jumpForce;

    public float hurtForce;
    [Header("狀態顯示")]
    public bool isHurt;
    public bool isDead;
    public bool isRun;
    public bool isJump;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();

        inputControl = new PlayerInputControl();


        inputControl.Gameplay.Jump.started += Jump;
    }


    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Update()
    {
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
        CheckMove();
        CheckJumpKeyIsPressed();
    }

    private void FixedUpdate()
    {
        if (!isHurt)
        {
            Move();
        }
    }
    //測試
    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     Debug.Log(other.name);
    // }
    public void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);

        int faceDir = (int)transform.localScale.x;

        if (inputDirection.x > 0)
            faceDir = 1;

        if (inputDirection.x < 0)
            faceDir = -1;
        //人物翻轉
        transform.localScale = new Vector3(faceDir, 1, 1);
    }

    private void CheckMove()
    {
        if (inputControl.Gameplay.Move.IsPressed())
        {
            isRun = true;
        }
        else
        {
            isRun = false;
        }

    }


    private void Jump(InputAction.CallbackContext context)
    {
        if (physicsCheck.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("jump");
        }
    }
    private void CheckJumpKeyIsPressed()
    {
        if (inputControl.Gameplay.Jump.IsPressed())
        {
            isJump = true;
        }
        else
        {
            isJump = false;
        }
    }

    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }

    public void PlayerDead()
    {
        isDead = true;
        inputControl.Gameplay.Disable();
    }
}
