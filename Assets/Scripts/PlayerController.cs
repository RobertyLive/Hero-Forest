using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _playerRb;
    private float GetInput;
    private Animator _animator;
    
    public float speed;

    public float jumpForce;

    [Header("Detected Ground")]
    public Transform StartRay;
    public float sizeRay;
    public LayerMask layerGround;


    [Header("AtackPlayer")]
    public Transform startCircle;
    public float radius;
    public LayerMask layerEnemie;



    private void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        GetInput = Input.GetAxisRaw("Horizontal");
        Flip();

        
        
        if (Input.GetButtonDown("Jump") && CheckRay())
        {
            Jump();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Atack();
        }

        _animator.SetFloat("SpeedY", _playerRb.velocity.y);
        _animator.SetBool("isGround", CheckRay());
    }


    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        _playerRb.velocity = new Vector2(GetInput * speed, _playerRb.velocity.y);
        _animator.SetInteger("move", (int)Mathf.Abs(GetInput));
    }
    private void Jump()
    {
        _playerRb.velocity = Vector2.up * jumpForce;
        //_playerRb.AddForce(Vector2.up * jumpForce);
    }
    private void Flip()
    {
        if (GetInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(GetInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void Atack()
    {
        _animator.SetTrigger("atack");
        Collider2D circle = Physics2D.OverlapCircle(startCircle.position, radius * sizeRay, layerEnemie);

        if(circle != null)
        {
            Debug.Log("Pegou em inimigoLayer");
        }
    }
    private bool CheckRay()
    {
        RaycastHit2D rayGround = Physics2D.Raycast(StartRay.position, Vector2.down * sizeRay,sizeRay, layerGround);
        
        Color ray = rayGround ? Color.green : Color.red;
        Debug.DrawRay(StartRay.position, Vector2.down * sizeRay, ray);
        return rayGround;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(startCircle.position, radius);
    }
}
