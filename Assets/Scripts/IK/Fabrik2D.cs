using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using UnityEngine;

namespace IK
{
    public class Fabrik2D : MonoBehaviour
    {
        public Transform root;
        public int segmentCount;
        private Segment[] segments;
        public Transform target;
        public bool isFixed = true;
        public bool hasPole = false;
        [CanBeNull] public Transform pole = null;
        private bool reaching;
        private float completeLength;

        private void Start()
        {
            segments = new Segment[segmentCount];
            Transform cursor = root;
            for (var i = 0; i < segmentCount; i++)
            {
                Transform next = cursor.GetChild(0);
                float length = Vector2.Distance(cursor.position, next.position);
                segments[i] = new Segment(cursor, length);
            }

            completeLength = 0;
            foreach (Segment segment in segments) completeLength += segment.len;

            IK();
        }

        private void Update()
        {
            IK();
        }

        public void IK()
        {
            if (!isFixed)
            {
                calcumulate(segments[^1].transform.position);
            }
            else if (target is not null)
            {
                Vector2 targetPos = target.position;
                Vector2 firstPos = segments[0].transform.position;
                Vector2 delta = targetPos - firstPos;
                reaching = delta.sqrMagnitude >= completeLength * completeLength;
                if (reaching)
                {
                    Vector2 dir = delta - firstPos;
                    dir.Normalize();
                    for (var i = 1; i < segments.Length; i++)
                    {
                        Vector2 newPos = (Vector2)segments[i - 1].transform.position + dir * segments[i - 1].len;
                        Transform ipT = segments[i].transform;
                        ipT.position = new Vector3(newPos.x, newPos.y, ipT.position.z);
                    }
                }
                else
                {
                    calcumulate(targetPos);
                }
            }

            if (hasPole && pole is not null)
                for (var i = 1; i < segments.Length - 1; i++)
                {
                    Vector2 im1 = segments[i - 1].transform.position;
                    // Vector2 ip1 = segments[i + 1].pos;
                    Transform ipT = segments[i].transform;
                    Vector2 ip = ipT.position;

                    Vector2 a = ip - im1;
                    a.Normalize();
                    Vector3 polePosition = pole.position;
                    var b = new Vector2(polePosition.x - im1.x, polePosition.y - im1.y);
                    b.Normalize();

                    float angle = Mathf.Atan2(Cross(a, b), Vector2.Dot(a, b));

                    Vector2 result2 = Rotate(ip - im1, angle) + im1;

                    ipT.position = new Vector3(result2.x, result2.y, ipT.position.z);
                }
        }

        private static float Cross(Vector2 a, Vector2 b)
        {
            return a.x * b.y - a.y * b.x;
        }

        private static Vector2 Rotate(Vector2 v, float radians)
        {
            float c = Mathf.Cos(radians);
            float s = Mathf.Sin(radians);
            return new Vector2(c * v.x - s * v.y, s * v.x + c * v.y);
        }

        public void calcumulate(Vector2 target)
        {
            for (int i = segments.Length - 1; i > 0; i--)
                if (i == segments.Length - 1)
                {
                    Transform ipT = segments[i].transform;
                    ipT.position = new Vector3(target.x, target.y, ipT.position.z);
                }
                else
                {
                    Segment si = segments[i];
                    Segment si1 = segments[i + 1];
                    Vector3 siPos = si.transform.position;
                    Vector3 delta = siPos - si1.transform.position;
                    delta.Normalize();
                    Vector2 newPos = si1.transform.position + delta * si.len;
                    si.transform.position = new Vector3(newPos.x, newPos.y, siPos.z);
                }

            for (var i = 1; i < segments.Length; i++)
            {
                Segment si = segments[i];
                Segment si1 = segments[i - 1];
                Vector3 siPos = si.transform.position;
                Vector3 delta = siPos - si1.transform.position;
                delta.Normalize();
                Vector2 newPos = si1.transform.position + delta * si1.len;
                si.transform.position = new Vector3(newPos.x, newPos.y, siPos.z);
            }
        }
    }
}