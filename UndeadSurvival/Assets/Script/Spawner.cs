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
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        level = Mathf.FloorToInt(GameManager.instance.gameTime / 10f);
        Debug.Log(level);

        if (timer > (level == 0 ? 0.5 : 0.2))
        {
            Spawn(level);
            timer = 0;
        }
    }

    void Spawn(int type)
    {
        if (level >= GameManager.instance.PM.prefabs.Length) return;

        GameObject enemy = GameManager.instance.PM.Get(level);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
    }
}

// 직렬화를 통해 Inspector로 뺄 수 있음
// 따로 컴포넌트를 빼지 않는 이유는 SpawnData를 Spawner에서만 사용하기 때문ㄴ
[System.Serializable]
public class SpawnData : MonoBehaviour
{
    public int spriteType;
    
    public float spawnTime;
    
    public int hp;
    public float speed;
}