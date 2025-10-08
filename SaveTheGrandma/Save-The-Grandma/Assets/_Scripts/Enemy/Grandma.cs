using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grandma : MonoBehaviour
{
    [SerializeField] private int _health;


    public void GetDamage(int damage)
    {
        _health -= damage;
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
