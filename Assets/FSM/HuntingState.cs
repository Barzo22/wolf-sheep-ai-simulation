using UnityEngine;

public class HuntingState : IState
{
    private Hunter _hunter;
    private FSM _fsm;
    private Boid _target;

    public HuntingState(Hunter hunter, FSM fsm)
    {
        _hunter = hunter;
        _fsm = fsm;
    }

    public void OnStart()
    {
        _target = _hunter.FindNearestBoid();
    }

    public void OnUpdate()
    {
        if (_target == null)
        {
            _fsm.ChangeState(FSM.State.Patrol);
            return;
        }

        Vector3 futurePos = _target.transform.position + _target.Velocity * _hunter.predictionTime;
        Vector3 dir = futurePos - _hunter.transform.position;
        _hunter.AddForce(dir.normalized);

        _hunter.energy -= _hunter.energyDrain * Time.deltaTime;

        if (_hunter.energy <= 0)
        {
            _fsm.ChangeState(FSM.State.Idle);
        }

        if ((_target.transform.position - _hunter.transform.position).magnitude > _hunter.visionRadius)
        {
            _fsm.ChangeState(FSM.State.Patrol);
        }

        if (Vector3.Distance(_hunter.transform.position, _target.transform.position) <= _hunter.attackRadius)
        {
            _fsm.ChangeState(FSM.State.Attack);
        }
    }

    public void OnExit() 
    { 
    }
}

