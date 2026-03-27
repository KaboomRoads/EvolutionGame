using System;
using Brain;
using UnityEngine;

public class ControlledProjectile : MonoBehaviour
{
    [NonSerialized] public Brain.Brain brain;
    public CompiledBrainProgram program;
    public float tickInterval;
    public float damage;
    public float speed;
    private float tickTimer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Potvora potvora))
        {
            potvora.Damage(damage);
            Destroy(this);
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
            RunningBrainProgram instance = brain.runningInstance;
            if (instance is not null)
            {
                if (instance.currentFunction is null) instance.currentFunction = instance.GetMain();
                instance.Step();
            }
        }

        transform.Translate(transform.up * (speed * Time.deltaTime));
    }
}