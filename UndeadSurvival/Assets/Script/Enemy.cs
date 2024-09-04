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
    Collider2D col;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
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

        col.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        animator.SetBool("Dead", false);
    }

    public void Init(SpawnData data)
    {
        animator.runtimeAnimatorController = AniCon[data.spriteType];
        speed = data.speed;
        HP = data.hp;
        maxHP = data.hp;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Bullet") || !isLive)
            return;

        this.HP -= other.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if (this.HP > 0)
            animator.SetTrigger("Hit");
        else
        {
            isLive = false;
            col.enabled = false; // 콜라이더 종료
            rigid.simulated = false; // 시뮬레이션 종료
            spriter.sortingOrder = 1;
            animator.SetBool("Dead", true);
            GameManager.instance.killCount++;
            GameManager.instance.GetEXP();
        }

    }

    void Dead()
    {
        gameObject.SetActive(false);
    }

    IEnumerator KnockBack()
    {
        yield return wait; // 다음 하나의 물리 프레임 딜레이

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);
    }
}
