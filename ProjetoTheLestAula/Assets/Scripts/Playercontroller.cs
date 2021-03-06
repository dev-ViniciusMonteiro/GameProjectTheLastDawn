using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    private int ParamSpeed = Animator.StringToHash("speed");
    private int ParamJump = Animator.StringToHash("jump");

    public float speedForce = 12.0f;

    public bool isGrounded = false;
    public Vector3 offSet = Vector2.zero;
    public float radios = 10.0f;
    public LayerMask layer;


    public Vector2 _moviment = Vector2.zero;
    private Rigidbody2D _body = null;
    private Animator _animation = null;
    private SpriteRenderer _render = null;


    public Vector2 velocity;

    // Despertado ? chamado quando a inst?ncia do script for carregada
    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _animation = GetComponent<Animator>();
        _render = GetComponent<SpriteRenderer>();
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        _moviment = new Vector2(Input.GetAxisRaw("Horizontal") * speedForce, 0.0f);

        if (_moviment.sqrMagnitude > 0.1f)
        {
            bool xRight = Input.GetAxis("Horizontal") > 0;
            if (_render.flipX == xRight)
                _render.flipX = !xRight;
        }

            if (Input.GetButtonDown("Jump")&&isGrounded)
        {
            _body.AddForce(Vector2.up*5, ForceMode2D.Impulse);
        }

        _animation.SetBool(ParamJump, isGrounded);

        float speed = Mathf.Abs(_body.velocity.x);
        _animation.SetFloat(ParamSpeed, speed);

    }

    private void FixedUpdate()
    {
        velocity = _body.velocity;

        isGrounded = Physics2D.OverlapCircle((this.transform.position + offSet), radios, layer);

        if (_moviment.sqrMagnitude > 0.1f)
        {

            _body.AddForce(_moviment, ForceMode2D.Force);

        }
    }

    // Implemente OnDrawGizmosSelected se desejar desenhar utens?lios somente se o objeto for selecionado
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((this.transform.position + offSet), radios);
    }


}
