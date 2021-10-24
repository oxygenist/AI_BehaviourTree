using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.BehaviourTree
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        public Node node;

        public NodeView(Node node)
        {
            this.node = node;
            this.title = node.name;
        }
    }
}
