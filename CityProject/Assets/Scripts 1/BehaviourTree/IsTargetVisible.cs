using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IsTargetVisible : BTNode
{

    IBehaviourAI myAI;

    public IsTargetVisible(IBehaviourAI _myAI)
    {
        myAI = _myAI;
    }

    public override BTNodeStates Evaluate()
    {
        if(myAI.GetTarget() == null)
        {
            return BTNodeStates.FAILURE;
        }

        RaycastHit hit;

        //How far the raycast can see, WE WANT THE AI to be facing the ting it wants to attack
        if (Physics.Raycast(myAI.GetTransform().position, myAI.GetTransform().forward, out hit, 1000f))
        {
            
            //every ray cast has a hit collider it wants to collide with

            //Looking for the parent object
            if (hit.collider.transform.root.gameObject == myAI.GetTarget())
            {
                return BTNodeStates.SUCCESS;
            }
        }
        else
        {
            return BTNodeStates.FAILURE;
        }

    return BTNodeStates.SUCCESS;

        

    }
}
