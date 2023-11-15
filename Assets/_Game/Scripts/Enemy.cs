using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;

    private void Start()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        health = 100;
    }
}
