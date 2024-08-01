using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;

    Rigidbody2D rigid;
    SpriteRenderer mesh;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        inputVec = transform.position;
        rigid = GetComponent<Rigidbody2D>();
        mesh = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    inputVec.x = Input.GetAxisRaw("Horizontal");
    //    inputVec.y = Input.GetAxisRaw("Vertical");
    //}

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        // 움직이고자 하는 방향을 정규화 * 속도 * dt
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        animator.SetFloat("Speed", inputVec.magnitude);
       
        if (inputVec.x != 0)
            mesh.flipX = inputVec.x < 0;

    }
}
