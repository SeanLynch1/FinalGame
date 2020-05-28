using UnityEngine;
using System.Collections;

public class TurnToTargetTask : BTNode
{
    IBehaviourAI myAI;

    event InputEventVector3 TurnEvent;

    public TurnToTargetTask(IBehaviourAI _myAI,  InputEventVector3 _turnEvent)
    {
        myAI = _myAI;
        TurnEvent = _turnEvent;

    }

    public override BTNodeStates Evaluate()
    {
        Vector3 agentPosition = myAI.GetAgentTransform().position;
        Vector3 targetPosition = myAI.GetTargetPosition();

        Vector3 desiredHeading = (targetPosition - agentPosition);
        
        if(Vector3.Angle(agentPosition, desiredHeading) > 10)
        {
            TurnEvent(desiredHeading.x, desiredHeading.y, desiredHeading.z);
        }
        return BTNodeStates.SUCCESS;
    }
    }
