using System;
using System.Linq;
using Brain;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class ControlledProjectile : MonoBehaviour
{
    [NonSerialized] public Brain.Brain brain;
    public CompiledBrainProgram program;
    public float tickInterval;
    public float damage;
    public float speed;
    [CanBeNull] public Transform target;
    private float tickTimer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Potvora potvora))
        {
            potvora.Damage(damage);
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        brain = new Brain.Brain();
        brain.Instantiate(program, this);
    }

    private void Update()
    {
        tickTimer += Time.deltaTime;
        if (tickTimer >= tickInterval)
        {
            tickTimer -= tickInterval;
            if (target == null)
            {
                var enemies = FindObjectsByType<Potvora>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
                var closestDist = float.MaxValue;
                Potvora closestEnemy = null;
                var closestDistT = float.MaxValue;
                Potvora closestEnemyT = null;
                foreach (Potvora potvora in enemies)
                {
                    float dist = Vector2.Distance(potvora.transform.position, transform.position);
                    if (potvora.targeted)
                    {
                        if (dist < closestDistT)
                        {
                            closestDistT = dist;
                            closestEnemyT = potvora;
                        }
                    }
                    else
                    {
                        if (dist < closestDist)
                        {
                            closestDist = dist;
                            closestEnemy = potvora;
                        }
                    }
                }

                if (closestEnemy is not null)
                {
                    target = closestEnemy.transform;
                    closestEnemy.targeted = true;
                }
                else if (closestEnemyT is not null)
                {
                    target = closestEnemyT.transform;
                }
            }

            RunningBrainProgram instance = brain.runningInstance;
            if (instance is not null)
            {
                if (instance.currentFunction is null) instance.currentFunction = instance.GetMain();
                while (instance.currentFunction is not null) instance.Step();
            }

            damage++;
        }

        transform.Translate(transform.up * (speed * Time.deltaTime));
    }
}