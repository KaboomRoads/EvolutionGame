using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.U2D.IK;

namespace System.Runtime.CompilerServices
{
    internal class IsExternalInit
    {
    }
}

public class Spider : MonoBehaviour
{
    public float moveSpeed;
    public float legSpeed;
    public int legCount;
    public float anchorRadius;
    public float anchorThreshold;
    public IKManager2D legPrefab;
    private Leg[] legs;
    private Transform followTransform;
    private int movingLegLimit;
    private int movingLegCount;
    private int legOffset;

    private void Start()
    {
        followTransform = FindFirstObjectByType<Player>().transform;

        legs = new Leg[legCount];
        float radialIncrement = Mathf.PI * 2.0F / legCount;
        // var random = new Random();
        // float radialOffset = random.NextFloat() * Mathf.PI * 2.0F;
        float radialOffset = 0;
        for (var i = 0; i < legCount; i++)
        {
            IKManager2D leg = Instantiate(legPrefab, transform);
            leg.transform.position += Vector3.forward;
            float angle = radialOffset + i * radialIncrement;
            var anchor = new Vector2(Mathf.Sin(angle) * anchorRadius, Mathf.Cos(angle) * anchorRadius);
            Transform target = leg.solvers[0].GetChain(0).target;
            target.localPosition = anchor;
            target.SetParent(null);
            legs[i] = new Leg(target, anchor);
        }

        movingLegLimit = legCount / 2;
    }

    private void Update()
    {
        {
            float scaledSpeed = moveSpeed * Time.deltaTime;
            Vector3 followDelta = followTransform.position - transform.position;
            Vector3 scaledFollow = followDelta.normalized;
            scaledFollow *= Mathf.Min(scaledSpeed, followDelta.magnitude);
            transform.Translate(scaledFollow);
        }

        //  0 [1] 2
        //  0  1 [2]
        // [0] 1  2

        //  0 [1] 2 [3]
        // [0] 1 [2] 3
        //  0 [1] 2 [3]

        for (var i = 0; i < movingLegLimit; i++)
        {
            Leg leg = legs[(i * 2 + legOffset) % legCount];
            MoveState moveState = leg.moveState;
            Transform target = leg.target;
            Vector2 targetPos = target.position;
            if (moveState is null)
            {
                if (movingLegCount >= movingLegLimit) continue;
                var anchorPos = new Vector2(transform.position.x + leg.anchor.x, transform.position.y + leg.anchor.y);
                float dx = targetPos.x - anchorPos.x;
                float dy = targetPos.y - anchorPos.y;
                float distSqr = dx * dx + dy * dy;
                if (distSqr > anchorThreshold * anchorThreshold)
                {
                    Vector2 delta = anchorPos - targetPos;
                    delta.Normalize();
                    var destination = new Vector2(anchorPos.x + delta.x * (anchorThreshold - 0.01F), anchorPos.y + delta.y * (anchorThreshold - 0.01F));
                    leg.moveState = new MoveState(destination);
                    movingLegCount++;
                }
            }
            else
            {
                Vector2 destination = moveState.destination;
                float scaledSpeed = legSpeed * Time.deltaTime;
                Vector3 followDelta = destination - targetPos;
                if (scaledSpeed > followDelta.magnitude)
                {
                    target.position = new Vector3(destination.x, destination.y, target.position.z);
                    leg.moveState = null;
                    movingLegCount--;
                    if (movingLegCount <= 0) legOffset = (legOffset + 1) % legCount;
                }
                else
                {
                    Vector3 scaledFollow = followDelta.normalized;
                    scaledFollow *= scaledSpeed;
                    target.Translate(scaledFollow);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (legs is null) return;
        foreach (Leg leg in legs)
        {
            Gizmos.color = Color.blue;
            var anchorPos = new Vector2(transform.position.x + leg.anchor.x, transform.position.y + leg.anchor.y);
            Gizmos.DrawSphere(anchorPos, 0.1F);
            if (leg.moveState is not null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(leg.moveState.destination, 0.1F);
            }
        }
    }

    private class Leg
    {
        public Leg(Transform Target, Vector2 Anchor)
        {
            target = Target;
            anchor = Anchor;
        }

        public Transform target;
        public Vector2 anchor;
        [CanBeNull] public MoveState moveState = null;
    }

    private class MoveState
    {
        public MoveState(Vector2 Destination)
        {
            destination = Destination;
        }

        public Vector2 destination;
    }
}