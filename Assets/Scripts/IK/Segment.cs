using UnityEngine;

namespace IK
{
    public class Segment
    {
        public Transform transform;
        public float len;

        public Segment(Transform transform, float len)
        {
            this.transform = transform;
            this.len = len;
        }
    }
}