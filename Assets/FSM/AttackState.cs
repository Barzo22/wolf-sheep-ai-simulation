using UnityEngine;

public class AttackState : IState
{
    private Hunter _hunter;
    private FSM _fsm;
    private Boid _target;

    public AttackState(Hunter hunter, FSM fsm)
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

        float dist = Vector3.Distance(_hunter.transform.position, _target.transform.position);

        if (dist <= _hunter.attackRadius)
        {
            _target.Die(); 
            _fsm.ChangeState(FSM.State.Idle);
        }
        else
        {
            _fsm.ChangeState(FSM.State.Hunting);
        }
    }

    public void OnExit() { }
}

