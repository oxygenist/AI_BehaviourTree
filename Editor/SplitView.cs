using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace AI.BehaviourTree
{
    public class SplitView : TwoPaneSplitView
    {
        public new class UxmlFactory : UxmlFactory<SplitView, TwoPaneSplitView.UxmlTraits> { }
    }
}