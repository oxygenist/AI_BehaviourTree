using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.BehaviourTree
{
    public enum State
    {
        Running,
        Failure,
        Success,
    }

    public abstract class Node : ScriptableObject
    {
        [HideInInspector] public State state = State.Running;
        [HideInInspector] public bool started = false;
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;

        public State Update()
        {
            if(started.Equals(false))
            {
                OnStart();
                started = true; 
            }

            state = OnUpdate();

            if(state.Equals(State.Failure) || state == State.Success)
            {
                OnStop();
                started = false;
            }

            return state;
        }

        public virtual Node Clone()
        {
            return Instantiate(this);
        }
        
        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract State OnUpdate();
    }
}
