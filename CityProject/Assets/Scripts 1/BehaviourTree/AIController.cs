using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIController : MonoBehaviour, IControllerInput, IBehaviourAI
{
    public event InputEvent FireEvent;
    public event InputEventFloat SlideEvent;
    public event InputEventFloat ForwardEvent;
    public event InputEventFloat YawEvent;
    public event InputEventFloat PitchEvent;
    public event InputEventFloat RollEvent;
    public event InputEventFloat SideStrafeEvent;
    public event InputEventFloat VerticalStrafeEvent;
    public event InputEventVector3 TurnEvent;

    public Vector3 myTargetPosition = Vector3.zero;

    //behaviour
    public Selector rootAI;
    public Sequence CheckArrivalSequence;
    public Sequence MoveSequence;
    public Sequence DecideToAttack;
    public Selector SelectTargetType; // selects what target to go for

    bool avoiding = false;
    public float avoidDistance = 100f;
    public LayerMask avoidLayerMask;
    Vector3 temporaryTarget;
    Vector3 savedTargetPosition;




    GameObject target = null;
    public string enemyFaction = "PlayerFaction"; //making an enemy team, looks for objects tagged PlayerFaction


    void Start()
    {
        DecideToAttack = new Sequence(new List<BTNode>
        {

            new RandomChanceConditional(1,100,10),
            new FindNewTargetTask(this, enemyFaction),
        });

        SelectTargetType = new Selector(new List<BTNode>
        {
            DecideToAttack,
            new FindWanderPointTask(this, 1500f),

        });


        CheckArrivalSequence = new Sequence(new List<BTNode>
        {
            new CheckArrivalTask(this),
            SelectTargetType,
        });

       MoveSequence = new Sequence(new List<BTNode>
       {
            new ObstacleAvoidance(this, avoidDistance, TurnEvent, avoidLayerMask),//this will handle turning
            new MoveToTargetTask(this, 100f, ForwardEvent),
            new IsTargetVisible(this),
            new FireWeaponTask(this, FireEvent)
       });

        rootAI = new Selector(new List<BTNode>
        {
            CheckArrivalSequence,
            MoveSequence
        });


        new FindWanderPointTask(this, 100f).Evaluate();
    }
    void Update()
    {
        rootAI.Evaluate();
    }
    public Vector3 SetTargetPosition(Vector3 targetPosition)
    {
       
        myTargetPosition = targetPosition;
        return myTargetPosition;
    }

    public Transform GetAgentTransform()
    {
        return transform;
    }

    public Vector3 GetTargetPosition()
    {
        if (target != null)
        {
            return target.transform.position;
        }
        return myTargetPosition;
    }

    public GameObject SetTarget(GameObject newTarget)
    {
        target = newTarget;
        return target;
    }

    public GameObject GetTarget()
    {
        return target;
    }
    public Transform GetTransform()
    {
        return gameObject.transform;
    }

    //If avoidance starts to kick in we create a flage called avoiding
    public bool GetAvoidingFlag()
    {
        return avoiding;
    }

    public Vector3 SetTempTarget(Vector3 position)
    {
        avoiding = true;
        temporaryTarget = position;
        savedTargetPosition = myTargetPosition;
        return position;
    }

    public Vector3 ReturnToSaveTarget()
    {
        avoiding = false;
        temporaryTarget = Vector3.zero;
        myTargetPosition = savedTargetPosition;
        return savedTargetPosition;
    }
}
