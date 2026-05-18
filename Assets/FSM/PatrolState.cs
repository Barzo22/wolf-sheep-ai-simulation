using UnityEngine;

public class PatrolState : IState
{
    private Hunter _hunter;
    private FSM _fsm;
    private int _currentWaypoint = 0;
    private bool _forward = true;

    public PatrolState(Hunter hunter, FSM fsm)
    {
        _hunter = hunter;
        _fsm = fsm;
    }

    public void OnStart() { }

    public void OnUpdate()
    {
        Vector3 target = _hunter.waypoints[_currentWaypoint].position;
        Vector3 dir = target - _hunter.transform.position;
        _hunter.AddForce(dir.normalized);

        if (dir.magnitude < _hunter.nearWaypoint)
        {
            if (_forward)
            {
                _currentWaypoint++;
                if (_currentWaypoint >= _hunter.waypoints.Length)
                {
                    _currentWaypoint = _hunter.waypoints.Length - 1;
                    _forward = false;
                }
            }
            else
            {
                _currentWaypoint--;
                if (_currentWaypoint < 0)
                {
                    _currentWaypoint = 0;
                    _forward = true;
                }
            }
        }

        _hunter.energy -= _hunter.energyDrain * Time.deltaTime;

        Boid nearestBoid = _hunter.FindNearestBoid();
        if (nearestBoid != null)
        {
            _fsm.ChangeState(FSM.State.Hunting);
        }

        if (_hunter.energy <= 0)
        {
            _fsm.ChangeState(FSM.State.Idle);
        }
    }

    public void OnExit() { }
}
