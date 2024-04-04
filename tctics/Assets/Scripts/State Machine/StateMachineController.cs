using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateMachineController : MonoBehaviour
{

    public static StateMachineController Instance;

    State _current;
    bool busy;
    public State current { get { return _current; } }

    public Transform selector;

    [Header("Choose Actions State")]
    public List<Image> chooseActionsButtons;
    public Image chooseActionSelection;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ChangeTo<LoadState>();
    }

    public void ChangeTo<T>() where T : State
    {
        State state = GetState<T>();
        if (_current != state)
        {
            ChangeState(state);
        }
    }

    public T GetState<T>() where T : State
    {
        T target = GetComponent<T>();
        if (target == null)
        {
            target = gameObject.AddComponent<T>();
        }
        return target;
    }

    protected void ChangeState(State value)
    {
        if (busy)
        {
            return;
        }
        busy = true;

        if (_current != null)
        {
            _current.Exit();
        }

        _current = value;

        if(_current != null)
        {
            _current.Enter();
        }
        busy = false;
    }
}
