using UnityEngine;

public class IdleState : IState
{
    private Hunter _hunter;
    private FSM _fsm;
    private float _timer;

    public IdleState(Hunter hunter, FSM fsm)
    {
        _hunter = hunter;
        _fsm = fsm;
    }

    public void OnStart()
    {
        _timer = 0f;
        _hunter.StopVelocity();
    }

    public void OnUpdate()
    {
        _timer += Time.deltaTime;
        _hunter.energy += _hunter.recoveryRate * Time.deltaTime;

        if (_timer >= _hunter.restTime)
        {
            _fsm.ChangeState(FSM.State.Patrol);
        }
    }

    public void OnExit()
    {

    }
}
