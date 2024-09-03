using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabID;
    public float damage;
    public int count;
    public float speed;

    float timer;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
       player = GetComponentInParent<Player>();

       Init();
    }

    // Update is called once per frame
    void Update()
    {
        switch (id)
        {
            case 0:
                {
                    transform.Rotate(Vector3.forward * speed * Time.deltaTime);
                }
                break;
            default:
                {
                    timer += Time.deltaTime;
                    if(timer > speed)
                    {
                        timer = 0;
                        Fire();
                    }
                }
                break;
        }
    }
    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
            SetWeaponPosition();
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                {
                    speed = -150;
                    SetWeaponPosition();
                }
                break;
            default:
                {
                    speed = 0.3f;
                }
                break;
        }
    }

    void SetWeaponPosition()
    {
        for (int index = 0; index < count; index++)
        {
            Transform bullet;

            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instance.PM.Get(prefabID).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity; // zero

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1, new Vector3(0, 0, 0)); // -1 is for infinity per.
        }
    }

    void Fire()
    {
        if (player.scanner.nearestTarget == null)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.PM.Get(prefabID).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}
