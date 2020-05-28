using UnityEngine;
using System.Collections;

public class MoveToTargetTask : BTNode
{
    IBehaviourAI myAI;
    Transform agentTransform;
    Vector3 targetPosition;
    float range;//how far you want to be before you slow down
    event InputEventFloat ForwardEvent;


    public MoveToTargetTask (IBehaviourAI _myAI, float _range, InputEventFloat _forwardEvent)
    {
        myAI = _myAI;
        range = _range;
        ForwardEvent = _forwardEvent;

    }
    public override BTNodeStates Evaluate()
    {
        Vector3 agentPosition = myAI.GetAgentTransform().position;
         targetPosition = myAI.GetTargetPosition();

        float distance = Vector3.Distance(agentPosition, targetPosition);
        float thrust = distance / range;
        if (ForwardEvent != null) ForwardEvent(thrust);

        return BTNodeStates.SUCCESS;
    }
}
