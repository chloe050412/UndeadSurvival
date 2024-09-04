using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime;

    [Header("# Player Info")]
    public int LV;
    public int killCount;
    public int EXP;
    public int[] nextEXP;

    [Header("# Game Object")]
    public PoolManager PM;
    public Player player;

    void Awake()
    {
        instance = this;
        maxGameTime = 60;
        gameTime = 0;
        LV = 0;
        killCount = 0;
        EXP = 0;

        Debug.Log("nextEXP 배열 크기: " + nextEXP.Length); // 배열 크기를 로그로 출력
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
            gameTime = maxGameTime;
    }
    public void GetEXP()
    {
        EXP++;

        if(EXP >= this.nextEXP[0])
        {
            LV++;
            EXP = 0;
        }
    }
}
