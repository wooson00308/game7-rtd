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
        [Header("Ship Node Part")]
        [SerializeField] private Transform _nodeParent;

        private List<Node> _nodes = new ();
        public List<Node> Nodes => _nodes;

        private Node _curNode;

        public Node CurNode => _curNode;

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
                node.SetNodePart(this);
            }
        }

        public Node GetNode(int id)
        {
            return _nodes[id];
        }

        public void TryRandomSpawn(SO_Tower tower)
        {
            if (IsNotEmptyNodes()) return;

            var node = _nodes[Random.Range(0, _nodes.Count)];

            var searchedNodes = new Node[_nodes.Count];
            int index = 0;

            while (!node.SpawnTower(tower))
            {
                searchedNodes[index++] = node; 
                node = _nodes[Random.Range(0, _nodes.Count)];

                foreach(var sNode in searchedNodes)
                {
                    if (sNode == null) break;

                    if(node.Id.Equals(sNode.Id))
                    {
                        node = _nodes[Random.Range(0, _nodes.Count)];
                    }
                }
            }
        }

        public bool IsNotEmptyNodes()
        {
            bool isNotEmptyNodes = true;
            _nodes.ForEach(x => {
                if (x.IsEmptyTower)
                {
                    isNotEmptyNodes = false;
                }
            });

            return isNotEmptyNodes;
        }

        public void OnNodeSelect(Node node)
        {
            if (_curNode != null)
            {
                bool isEqualsNode = _curNode.Id.Equals(node.Id);

                _curNode.SetSelect(false);
                _curNode = null;

                if (isEqualsNode)
                {
                    return;
                }
            }

            _curNode = node;
            _curNode.SetSelect();
        }

        public void DeselectedNode()
        {
            if(_curNode != null)
            {
                _curNode.SetSelect(false);
            }
        }
    }
}