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
        // �迭�� �ڽĵ��� transform�� �����ͼ� �������
        // TODO) ���ǿ����� Awake�� ����־�����, start�� ����־ �����ϴ�. 
        // �ٸ� Awake�� Spawner�� Ȱ��ȭ �� ������ ȣ��� �ٵ� ���� �׷� �ʿ䰡 ������ �ñ��ϱ� ��
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        // ���� ����
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

// ����ȭ�� ���� Inspector�� �� �� ����
// ���� ������Ʈ�� ���� �ʴ� ������ SpawnData�� Spawner������ ����ϱ� ������
[System.Serializable]
public class SpawnData
{
    public int spriteType;
    
    public float spawnTime;
    
    public int hp;
    public float speed;
}