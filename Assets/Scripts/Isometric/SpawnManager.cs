using System;
using System.Collections.Generic;
using Brain;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Isometric
{
    public class SpawnManager : MonoBehaviour
    {
        public float spawnInterval;
        public float radius;
        public float randomRadiusPadding;
        public int spawnCount;
        public GameObject[] goons;
        public ControlledProjectile projectile;
        public IsoPlayer player;
        private CompiledBrainProgram program;
        private float nextSpawn = float.MinValue;

        private void Start()
        {
            player = FindAnyObjectByType<IsoPlayer>();
            program = Brain.Brain.ParseProgram(EvolutionData.instance.programLines);
        }

        private void Update()
        {
            if (nextSpawn == float.MinValue) nextSpawn = Time.time + spawnInterval;
            if (Time.time >= nextSpawn)
            {
                DoSpawn();
                nextSpawn += spawnInterval;
            }
        }

        public void DoSpawn()
        {
            Vector2 center = transform.position;
            for (var i = 0; i < spawnCount; i++)
            {
                Vector2 ranVector = Quaternion.EulerAngles(0, 0, MathF.PI * 2.0F * Random.value) * Vector2.up;
                ranVector *= radius + Random.value * randomRadiusPadding;
                Vector2 spawnPos = center + ranVector;
                GameObject goon = goons[Random.Range(0, goons.Length)];
                goon.transform.position = spawnPos;
                Instantiate(goon);

                projectile.transform.position = player.transform.position;
                ControlledProjectile instance = Instantiate(projectile);
                instance.program = new CompiledBrainProgram(program.functions);
            }

            {
                projectile.transform.position = player.transform.position;
                ControlledProjectile instance = Instantiate(projectile);
                instance.program = new CompiledBrainProgram(program.functions);
            }
        }
    }
}