using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boid : Agent
{
    [SerializeField] float _radiusSeparation;
    [SerializeField] public float _radiusDetect;
    [SerializeField] public float _radiusFood;
    [SerializeField] public float _radiusHunter;
    [SerializeField] Node rootNode;
    private bool isDead = false;


    private void Start()
    {
        GameManager.Instance.boids.Add(this);
        AddForce(new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * _maxVelocity);
    }

    protected override void Update()
    {
        if (isDead) return;
        if (rootNode != null)
        {
            rootNode.Execute(this);
        }
        else
        {
            Flocking();
        }

        base.Update();
    }

    public void Flocking()
    {
        if (isDead) return;
        AddForce(Separation(GameManager.Instance.boids, _radiusSeparation) * GameManager.Instance.weightSeparation);
        AddForce(Aligment(GameManager.Instance.boids, _radiusDetect) * GameManager.Instance.weightAligment);
        AddForce(Cohesion(GameManager.Instance.boids, _radiusDetect) * GameManager.Instance.weightCohesion);
    }
    Vector3 Separation(List<Boid> boids, float radius)
    {
        Vector3 desired = Vector3.zero;

        foreach (Boid boid in boids)
        {
            var dir = transform.position - boid.transform.position;

            if (dir.magnitude > radius || boid == this) continue;

            desired += dir;
        }

        if (desired == Vector3.zero)
            return desired;

        desired.Normalize();
        desired.y = 0;
        desired *= _maxVelocity;

        var steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, _maxForce);

        return steering;
    }

    Vector3 Aligment(List<Boid> boids, float radius)
    {
        Vector3 desired = Vector3.zero;
        int count = 0;
        foreach (Boid boid in boids)
        {
            var dist = (transform.position - boid.transform.position).magnitude;

            if (dist > radius || boid == this) continue;

            desired += boid.Velocity;
            count++;
        }

        if (count <= 0)
            return Vector3.zero;

        desired.Normalize();
        desired.y = 0;
        desired *= _maxVelocity;

        var steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, _maxForce);

        return steering;
    }

    Vector3 Cohesion(List<Boid> boids, float radius)
    {
        Vector3 sum = Vector3.zero;
        int count = 0;
        foreach (Boid boid in boids)
        {
            var dist = (transform.position - boid.transform.position).magnitude;

            if (dist > radius || boid == this) continue;

            sum += boid.transform.position;
            count++;
        }

        if (sum == Vector3.zero)
            return Vector3.zero;

        var center = sum / count;
        var dir = center - transform.position;

        var desired = dir.normalized;
        desired.y = 0;
        desired *= _maxVelocity;

        float distance = dir.magnitude;
        desired *= distance / radius;

        var steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, _maxForce);

        return steering;
    }
    public Vector3 Seek(Vector3 target)
    {
        var desired = target - transform.position;
        desired.y = 0;
        desired.Normalize();
        desired *= _maxVelocity;

        var steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, _maxForce);

        return steering;
    }
    public Vector3 Arrive(Vector3 target)
    {
        var desired = target - transform.position;
        desired.y = 0;
        float dist = desired.magnitude;

        if (dist > _radiusFood)
            return Seek(target);

        desired.Normalize();
        desired *= _maxVelocity * (dist / _radiusFood);

        var steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, _maxForce);

        return steering;
    }
    public void Die()
    {
        isDead = true;
        StopVelocity();
        rootNode = null;
        GameManager.Instance.boids.Remove(this);

        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.material.color = Color.red;
        }
    }
}

  /* private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radiusSeparation);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radiusDetect);

        Gizmos.color= Color.blue;
        Gizmos.DrawWireSphere(transform.position, _radiusFood);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _radiusHunter);
    }
}*/