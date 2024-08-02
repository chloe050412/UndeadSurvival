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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                {
                    speed = -150;

                }
                break;
            case 1:
                {

                }
                break;
            default:
                {

                }
                break;
        }
    }

    void SetWeaponPosition()
    {

    }
}
