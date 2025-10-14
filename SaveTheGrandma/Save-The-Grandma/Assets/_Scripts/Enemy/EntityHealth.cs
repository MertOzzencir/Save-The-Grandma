using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] private int _health;


    public void GetDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
            HandleDeath();
    }

    public void HandleDeath()
    {
        Destroy(gameObject);
    }
}
