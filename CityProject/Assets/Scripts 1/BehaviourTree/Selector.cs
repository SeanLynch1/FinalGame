using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Selector : BTNode
{

    protected List<BTNode> myNodes = new List<BTNode>();

    public Selector(List<BTNode> nodes)
    {
        myNodes = nodes;
    }
    public override BTNodeStates Evaluate()
    {
        foreach (BTNode node in myNodes)
        {
            switch (node.Evaluate())
            {
                case BTNodeStates.FAILURE:
                    continue; //if we fail in a selector we continue to go down the list

                case BTNodeStates.SUCCESS:
                    currentNodeState = BTNodeStates.SUCCESS;
                    return currentNodeState;


                default:
                    continue;
            }

        }

        currentNodeState = BTNodeStates.FAILURE;
        return currentNodeState;
    }
}
