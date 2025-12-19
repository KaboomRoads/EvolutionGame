using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float movementSpeed;
    public float jumpVelocity;
    [Range(0.0f, 180.0f)] public float groundAngleThreshold = 60.0F;
    public float airControl = 0.5F;
    public int groundDecayTicks = 10;

    [NonSerialized] public Vector2 rawInput;
    [NonSerialized] public bool onGround;
    [NonSerialized] public bool effectiveOnGround;

    private Rigidbody2D rb;
    private BoxCollider2D col;
    private List<ContactPoint2D> contactPoints = new();
    private int groundTimer;

    private void OnCollisionEnter2D(Collision2D _)
    {
        UpdateOnGround();
    }

    private void OnCollisionExit2D(Collision2D _)
    {
        UpdateOnGround();
    }

    private void UpdateOnGround()
    {
        col.GetContacts(contactPoints);
        var newOnGround = false;
        foreach (ContactPoint2D point in contactPoints)
            if (Math.Abs(Vector2.SignedAngle(point.normal, Vector2.up)) <= groundAngleThreshold)
                newOnGround = true;

        if (onGround && !newOnGround) groundTimer = groundDecayTicks;
        else effectiveOnGround = newOnGround;
        onGround = newOnGround;
    }

    private void OnDrawGizmosSelected()
    {
        var c = GetComponent<BoxCollider2D>();
        if (c is null) return;
        var start = new Vector2(transform.position.x, transform.position.y);
        float rad = groundAngleThreshold * Mathf.Deg2Rad;
        var end1 = new Vector2(transform.position.x - Mathf.Sin(rad), transform.position.y + Mathf.Cos(rad));
        var end2 = new Vector2(transform.position.x - Mathf.Sin(-rad), transform.position.y + Mathf.Cos(-rad));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(start, end1);
        Gizmos.DrawLine(start, end2);
    }

    public static Vector2 rotate(Vector2 v, float delta)
    {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        rawInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector2 vel = rb.velocity;
        float speed = movementSpeed;
        if (!effectiveOnGround) speed *= airControl;
        vel.x = rawInput.x * speed;
        if (effectiveOnGround && rawInput.y > 0 && vel.y < jumpVelocity)
        {
            vel.y = jumpVelocity;
            effectiveOnGround = false;
        }

        if (groundTimer > 0 && --groundTimer <= 0) effectiveOnGround = onGround;

        rb.velocity = vel;
    }
}