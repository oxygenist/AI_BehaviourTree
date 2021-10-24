using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AI.BehaviourTree
{
    [CreateAssetMenu(fileName = "BT", menuName = "AI/BehaviourTree")]
    public class BehaviourTree : ScriptableObject
    {
        public Node rootNode;
        public State treeState = State.Running; 
        public List<Node> nodes = new List<Node>();

        public State Update()
        {
            if(rootNode.state == State.Running)
            {
                treeState = rootNode.Update();
            }

            return treeState;
        }

        public Node CreateNode(System.Type type)
        {
            Node node = ScriptableObject.CreateInstance(type) as Node;
            node.name = type.Name;
            node.guid = GUID.Generate().ToString();

            nodes.Add(node);

            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();

            return node;
        }

        public void DeleteNode(Node node)
        {
            nodes.Remove(node);

            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }
    }
}