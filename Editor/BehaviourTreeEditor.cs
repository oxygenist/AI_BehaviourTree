using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace AI.BehaviourTree
{
    public class BehaviourTreeEditor : EditorWindow
    {
        BehaviourTreeView treeView;
        InspectorView inspectorView;

        [MenuItem("Zombist/BehaviourTreeEditor")]
        public static void OpenWindow()
        {
            BehaviourTreeEditor wnd = GetWindow<BehaviourTreeEditor>();
            wnd.titleContent = new GUIContent("BehaviourTreeEditor");
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/ZAI/Editor/BehaviourTreeEditor.uxml");
            visualTree.CloneTree(root);

            // A stylesheet can be added to a VisualElement.
            // The style will be applied to the VisualElement and all of its children.
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/ZAI/Editor/BehaviourTreeEditor.uss");
            root.styleSheets.Add(styleSheet);

            treeView = root.Q<BehaviourTreeView>();
            inspectorView = root.Q<InspectorView>();
        }

        private void OnSelectionChange()
        {
            BehaviourTree tree = Selection.activeObject as BehaviourTree;

            if(tree)
            {
                treeView.PopulateView(tree);
            }
        }
    }
}