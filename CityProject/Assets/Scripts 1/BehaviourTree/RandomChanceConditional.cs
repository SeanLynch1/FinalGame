using UnityEngine;
using System.Collections;

public class RandomChanceConditional : BTNode
{
    int numberOfDice;
    int numberOfSides;
    int numberToBeat;
    public RandomChanceConditional(int _numberOfDice, int _numberOfSides, int _numberToBeat)
    {
        numberOfDice = _numberOfDice;
        numberOfSides = _numberOfSides;
        numberToBeat = _numberToBeat;
    }
   

    public override BTNodeStates Evaluate()
    {
        int total = 0;
        for(int i = 0; i < numberOfDice; i++)
        {
            total += Random.Range(i,(numberOfSides + 1));
        }

        if(total > numberToBeat)
        {
            return BTNodeStates.SUCCESS;
        }
        else
        {
            return BTNodeStates.FAILURE;
        }
    }

}