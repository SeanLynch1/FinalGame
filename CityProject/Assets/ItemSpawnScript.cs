using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class ItemSpawnScript : MonoBehaviour
{
    public Text gameStatusUI;
    public Text gameOverUI;
    public AudioSource[] audio;
    int item = 150;
    // Start is called before the first frame update
    GameManager gm;

    void Start()
    {
        audio = GetComponents<AudioSource>();
        // Instantiate GameManager Object
        gm = GameManager.Instance;
        gm.Start();


        for (int i = 0; i < item - gm.gameStatus.itemsCollected; i++)
        {
            Debug.Log("Item");
            Instantiate(Resources.Load("ITEM"), new Vector3(Random.Range(-400.0f, 400.0f), Random.Range(-100.0f, 1000.0f), Random.Range(-600.0f, 600.0f)), Quaternion.identity);
        }

        
    }
    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.name == "ITEM(Clone)")
        {

            Destroy(col.gameObject);
            gm.gameStatus.itemsCollected += 1;
            audio[1].Play();
        }
    }

    void FixedUpdate()
    {
        

        if (gm.gameStatus.itemsCollected >= item)
        {
            // Update gameoverUI with text 
            gameOverUI.text = "YOU WIN!";
            // Reset Gamemanager variuables
            gm.resetGame();

            SceneManager.LoadScene(+1);


            //MonoBehaviour has a gameObject property for the current game object
            Destroy(gameObject);

            // Destroy remaining AIEnemeys
            GameObject[] remainingAIBalls = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject go in remainingAIBalls)
            {
                Destroy(go);
            }
        }

        gameStatusUI.text = gm.UpdateStatus();


    }
}
