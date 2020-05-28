using UnityEngine;
using System.Collections;

public class CheckArrivalTask : BTNode
{
    IBehaviourAI myAI;

    public CheckArrivalTask(IBehaviourAI _myAI)
    {
        myAI = _myAI;

    }

    public override BTNodeStates Evaluate()
    {
        Vector3 agentPosition = myAI.GetAgentTransform().position;
        Vector3 targetPosition = myAI.GetTargetPosition();

        float distance = Vector3.Distance(agentPosition, targetPosition);

        if(distance < 100f)
        {
            Debug.Log("arrived");
            return BTNodeStates.SUCCESS;

        }
        else
        {
            Debug.Log("did not arrive, fail");
            return BTNodeStates.FAILURE;
        }
    }
}
