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
        public State state = State.Running;
        public bool started = false;
        public string guid;

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
        
        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract State OnUpdate();
    }
}
