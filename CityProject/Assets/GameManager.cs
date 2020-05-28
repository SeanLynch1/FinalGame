using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;


//game status data structure
[Serializable]
//A struct encapsulates data
public struct GameStatus
{

    public int itemsCollected;

}
public class GameManager : MonoBehaviour
{
    // Declare Struct for GameStatus (HUD Data)
    public GameStatus gameStatus;
  
 

    // Make a Public (single) instance of itself
    public static readonly GameManager instance = new GameManager();

    // private Constructor 
    private GameManager()
    {
    }

    // a readonly property that returns a reference to the single instance
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    // Use this for initialization
    public void Start()
    {
      
        gameStatus = new GameStatus();
       
    }

    //build our UI controls- a simple label
    public string UpdateStatus()
    {
        //building the formatted string to be shown to the user
        string message = "";

        message += "ITEMS COLLECTED: " + gameStatus.itemsCollected + "\n";
        return message;
    }

    public void resetGame()
    {
 
        gameStatus.itemsCollected = 0;

    }
}
