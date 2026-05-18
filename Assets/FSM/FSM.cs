using System.Collections.Generic;
using Unity.VisualScripting;

public class FSM
{
    public enum State
    {
        Idle,
        Patrol,
        Hunting,
        Attack
    }

    private Dictionary<State, IState> _allStates = new Dictionary<State, IState>();
    private IState _currentState;

    public void AddState(State name, IState newState)
    {
        if (_allStates.ContainsKey(name)) return;
        _allStates.Add(name, newState);
    }

    public void OnUpdateState()
    {
        if (_currentState != null)
            _currentState.OnUpdate();
    }

    public void ChangeState(State name)
    {
        if (!_allStates.ContainsKey(name)) return;

        if (_currentState != null)
            _currentState.OnExit();

        _currentState = _allStates[name];
        _currentState.OnStart();
    }
}

