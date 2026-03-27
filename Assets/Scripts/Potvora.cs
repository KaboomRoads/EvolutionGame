using UnityEngine;

public class Potvora : MonoBehaviour
{
    public float health;

    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0) Destroy(this);
    }
}