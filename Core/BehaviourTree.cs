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

        public void AddChild(Node parent, Node child)
        {
            DecoratorNode decorator = parent as DecoratorNode;
            if(decorator)
            {
                decorator.child = child;
            }

            RootNode rootNode = parent as RootNode;
            if (rootNode)
            {
                rootNode.child = child;
            }

            CompositeNode composite = parent as CompositeNode;
            if (composite)
            {
                composite.childeren.Add(child);
            }
        }

        public void RemoveChild(Node parent, Node child)
        {
            DecoratorNode decorator = parent as DecoratorNode;
            if (decorator)
            {
                decorator.child = null;
            }

            RootNode rootNode = parent as RootNode;
            if (rootNode)
            {
                rootNode.child = null;
            }

            CompositeNode composite = parent as CompositeNode;
            if (composite)
            {
                composite.childeren.Remove(child);
            }
        }

        public List<Node> GetChildern(Node parent)
        {
            List<Node> childern = new List<Node>();

            DecoratorNode decorator = parent as DecoratorNode;
            if (decorator && decorator.child != null)
            {
                childern.Add(decorator.child);
            }

            RootNode rootNode = parent as RootNode;
            if (rootNode && rootNode.child != null)
            {
                childern.Add(rootNode.child);
            }

            CompositeNode composite = parent as CompositeNode;
            if (composite)
            {
                return composite.childeren;
            }

            return childern;
        }

        public BehaviourTree Clone()
        {
            BehaviourTree tree = Instantiate(this);
            tree.rootNode = tree.rootNode.Clone();
            return tree;
        }
    }
}