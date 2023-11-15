using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Enemy enemy;
    [Range(1, 100)]
    public float damageAmount;
    public event Action <bool,float> onHealthChange;

    
    public void GetHit(bool isHeadShoot = false)
    {
        enemy.health -= damageAmount;
        onHealthChange?.Invoke(isHeadShoot,enemy.health);
    }
   
}
