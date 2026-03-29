using System;
using UnityEngine;

public class Potvora : MonoBehaviour
{
    public float health;
    [NonSerialized] public bool targeted = false;

    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0) Destroy(gameObject);
    }
}