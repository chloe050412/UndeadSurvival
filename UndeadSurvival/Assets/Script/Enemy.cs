using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float HP;
    public float maxHP;

    public RuntimeAnimatorController[] AniCon;
    public Rigidbody2D target;

    bool isLive;

    Animator animator;
    Rigidbody2D rigid;
    SpriteRenderer spriter;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isLive) 
            return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!isLive) 
            return;

        Vector2 dir = target.position - rigid.position;
        spriter.flipX = dir.x < 0;
    }
    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();   
        isLive = true;
        HP = maxHP;
    }

    public void Init(SpawnData data)
    {
        animator.runtimeAnimatorController = AniCon[data.spriteType];
        speed = data.speed;
        HP = data.hp;
        maxHP = data.hp;
    }
}
