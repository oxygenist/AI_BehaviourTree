using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Zombist.ZAI
{
    [CreateAssetMenu(fileName = "BT", menuName = "Zombist/ZAI")]
    public class BehaviourTree : ScriptableObject
    {
        public BTNode rootNode;
        public State treeState = State.Running; 
        public List<BTNode> nodes = new List<BTNode>();

        public State Update()
        {
            if(rootNode.state == State.Running)
            {
                treeState = rootNode.Update();
            }

            return treeState;
        }

        public BTNode CreateNode(System.Type type)
        {
            BTNode node = ScriptableObject.CreateInstance(type) as BTNode;
            node.name = type.Name;
            node.guid = GUID.Generate().ToString();

            nodes.Add(node);

            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();

            return node;
        }

        public void DeleteNode(BTNode node)
        {
            nodes.Remove(node);

            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }
    }
}