﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get 
        {
            if (instance == null)
            {
                Debug.LogError("GameManager is NULL!");
            }
            return instance;
        }
    }

    public bool HasKeyToCastle { get; set; }

    public Player Player { get; private set; }
    private void Awake()
    {
        instance = this;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

   
}
