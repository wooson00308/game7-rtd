using Catze;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Awake 이 후로 검색 가능
/// </summary>
public class NodeStorage : MUnit<NodeStorage>
{
    readonly List<Node_Old> _nodes = new();
    public List<Node_Old> Nodes => _nodes;

    protected override void Awake()
    {
        base.Awake();

        _nodes.AddRange(gameObject.GetComponentsInChildren<Node_Old>().ToList());

        int i = 0;
        foreach (var node in _nodes)
        {
            node.Index = i++;
        }
    }

    public Node_Old GetNode(int index)
    {
        return _nodes[index];
    }
}
