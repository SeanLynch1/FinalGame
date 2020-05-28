using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviourAI 
{
    //What ever influence an interface has on a script, that script must implement ALL the methods below

    Vector3 SetTargetPosition(Vector3 targetPosition);
    Transform GetAgentTransform();
    Vector3 GetTargetPosition();
    GameObject SetTarget(GameObject gameObject);
    GameObject GetTarget();
    Transform GetTransform();

    bool GetAvoidingFlag();
    Vector3 SetTempTarget(Vector3 position);
    Vector3 ReturnToSaveTarget();
}
