using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//allows for a whole bunch of AI behaviours
public class Sequence : BTNode
{
    private List<BTNode> myNodes = new List<BTNode>();

//a sequence is made up of a bunch of nodes
    public Sequence(List<BTNode> nodes)
    {
        myNodes = nodes;
    }
    public override BTNodeStates Evaluate()
    {
        //need to make sure things are running or not
        bool childRunning = false;

        foreach(BTNode node in myNodes)
        {
            switch (node.Evaluate())
            {
                case BTNodeStates.FAILURE:
                    currentNodeState = BTNodeStates.FAILURE;
                    return currentNodeState;

                case BTNodeStates.SUCCESS:
                    continue;

                case BTNodeStates.RUNNING:
                    childRunning = true;
                    continue;

                default:
                    currentNodeState = BTNodeStates.SUCCESS;

                    return currentNodeState;
            }

        }

        currentNodeState = childRunning ? BTNodeStates.RUNNING : BTNodeStates.SUCCESS; //checking to see if we continue because the child is running or because of its SUCCESS
        return currentNodeState;


    }
}
