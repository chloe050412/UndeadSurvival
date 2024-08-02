using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnDatas;

    float timer;
    int level;

    void Awake()
    {
        // 배열에 자식들의 transform을 가져와서 집어넣음
        // TODO) 강의에서는 Awake에 집어넣었지만, start에 집어넣어도 가능하다. 
        // 다만 Awake는 Spawner가 활성화 될 때마다 호출될 텐데 굳이 그럴 필요가 있을까 궁금하긴 함
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        // 스폰 시점
        timer += Time.deltaTime;

        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 30f), spawnDatas.Length - 1);
        if (timer > spawnDatas[level].spawnTime)
        {
            Spawn(0);
            timer = 0;
        }
    }

    void Spawn(int type)
    {
        GameObject enemy = GameManager.instance.PM.Get(type);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnDatas[level]);
    }
}

// 직렬화를 통해 Inspector로 뺄 수 있음
// 따로 컴포넌트를 빼지 않는 이유는 SpawnData를 Spawner에서만 사용하기 때문ㄴ
[System.Serializable]
public class SpawnData
{
    public int spriteType;
    
    public float spawnTime;
    
    public int hp;
    public float speed;
}