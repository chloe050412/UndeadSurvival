using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float gameTime;
    public float maxGameTime;

    public PoolManager PM;
    public Player player;

    void Awake()
    {
        instance = this;
        maxGameTime = 60;
        gameTime = 0;
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
            gameTime = maxGameTime;
    }
}
