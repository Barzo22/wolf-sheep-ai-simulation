using UnityEngine;

public class Hunter : Agent
{
    [SerializeField] public Transform[] waypoints;
    [SerializeField] public float nearWaypoint;
    [SerializeField] public float visionRadius;
    [SerializeField] public float predictionTime;
    [SerializeField] public float energy;
    [SerializeField] public float recoveryRate;
    [SerializeField] public float restTime;
    [SerializeField] public float energyDrain;
    [SerializeField] public float attackRadius;

    private FSM _fsm;

    private void Awake()
    {
        _fsm = new FSM();
        _fsm.AddState(FSM.State.Idle, new IdleState(this, _fsm));
        _fsm.AddState(FSM.State.Patrol, new PatrolState(this, _fsm));
        _fsm.AddState(FSM.State.Hunting, new HuntingState(this, _fsm));
        _fsm.AddState(FSM.State.Attack, new AttackState(this, _fsm));

        _fsm.ChangeState(FSM.State.Patrol);
    }

    private void Update()
    {
        _fsm.OnUpdateState();
        base.Update();
    }

    public Boid FindNearestBoid()
    {
        Boid nearest = null;
        float minDist = float.MaxValue;

        foreach (Boid b in GameManager.Instance.boids)
        {
            float dist = (b.transform.position - transform.position).sqrMagnitude;
            if (dist <= visionRadius * visionRadius && dist < minDist)
            {
                minDist = dist;
                nearest = b;
            }
        }

        return nearest;
    }
}

/*    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, visionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}*/

