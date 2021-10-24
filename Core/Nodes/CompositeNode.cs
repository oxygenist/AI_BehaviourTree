using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.BehaviourTree
{
    public abstract class CompositeNode : Node
    {
        public List<Node> childeren = new List<Node>();
    }
}