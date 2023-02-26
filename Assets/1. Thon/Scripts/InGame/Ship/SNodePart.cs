using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Catze
{
    /// <summary>
    /// 함선 내부의 노드들을 관리한다.
    /// </summary>
    public class SNodePart : Ship.Part
    {
        [SerializeField] private Transform _nodeParent;

        private List<Node> _nodes = new ();
        public List<Node> Nodes => _nodes;

        public int NodeCount => _nodes.Count;

        protected override void Awake()
        {
            base.Awake();
        }

        protected void Start()
        {
            _nodes = _nodeParent.GetComponentsInChildren<Node>().ToList();

            // Set Node Id
            int index = 0;
            foreach (var node in _nodes)
            {
                node.SetId(index++);
            }
        }
    }
}