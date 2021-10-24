using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace AI.BehaviourTree
{
    public class BehaviourTreeView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<BehaviourTreeView, GraphView.UxmlTraits> { }
        BehaviourTree tree;

        public BehaviourTreeView()
        {
            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/ZAI/Editor/BehaviourTreeEditor.uss");
            styleSheets.Add(styleSheet);
        }

        internal void PopulateView(BehaviourTree tree)
        {
            this.tree = tree;

            DeleteElements(graphElements.ToList());

            tree.nodes.ForEach(n => CreateNodeView(n));
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            // base.BuildContextualMenu(evt);
            {
                var types = TypeCache.GetTypesDerivedFrom<ActionNode>();

                foreach(var type in types)
                {
                    evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
                }
            }

            {
                var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
                foreach(var type in types)
                {
                    evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
                }
            }

            {
                var types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
                foreach (var type in types)
                {
                    evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
                }
            }
        }

        private void CreateNode(System.Type type)
        {
            Node node = tree.CreateNode(type);
            CreateNodeView(node);
        }

        void CreateNodeView(Node node)
        {
            NodeView nodeView = new NodeView(node);
            AddElement(nodeView);
        }
    }
}
