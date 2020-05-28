using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeaponTask : BTNode
{
    IBehaviourAI myAI;
    InputEvent fireWeaponEvent;

   public FireWeaponTask(IBehaviourAI _myAi, InputEvent _fireWeaponEvent)
   {
        myAI = _myAi;
        fireWeaponEvent = _fireWeaponEvent;

   }
    public override BTNodeStates Evaluate()
    {
        if(fireWeaponEvent != null)
        {
            Debug.Log("Weapon Fired");
            fireWeaponEvent();

            return BTNodeStates.SUCCESS;
        }
        else
        {
            return BTNodeStates.FAILURE;
        }
    }
   
    
}
