using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Isometric
{
    public class IsoPlayer : MonoBehaviour
    {
        public float movementSpeed;

        [NonSerialized] public Vector2 rawInput;

        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Move(InputAction.CallbackContext context)
        {
            rawInput = context.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            Vector2 vel = rb.velocity;
            float speed = movementSpeed;
            Vector2 normalizedScaled = rawInput.normalized;
            vel.x = normalizedScaled.x * speed;
            vel.y = normalizedScaled.y * speed;
            rb.velocity = vel;
        }
    }
}