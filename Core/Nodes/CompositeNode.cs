using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.BehaviourTree
{
    public abstract class CompositeNode : Node
    {
        [HideInInspector] public List<Node> childeren = new List<Node>();

        public override Node Clone()
        {
            CompositeNode node = Instantiate(this);
            node.childeren = childeren.ConvertAll(c => c.Clone());
            return node; 
        }
    }
}