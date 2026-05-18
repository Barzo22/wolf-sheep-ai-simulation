    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class Agent : MonoBehaviour
    {
        protected Vector3 _velocity;
        public Vector3 Velocity { get { return _velocity; } }

        [SerializeField] public float _maxVelocity;
        [SerializeField][Range(0f, 1f)] public float _maxForce;
        protected virtual void Update()
        {
            transform.position = LimitManager.instance.ApplyBounds(transform.position + _velocity * Time.deltaTime);
            transform.forward = _velocity;
        }
        public void StopVelocity()
        {
            _velocity = Vector3.zero;
        }

        public virtual void AddForce(Vector3 dir)
        {
            _velocity = Vector3.ClampMagnitude(_velocity + dir, _maxVelocity);
        }
    }
