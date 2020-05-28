using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

     public AudioSource[] spawn;

    public GameObject spawnPrefab;
    // Start is called before the first frame update
    void Start()
    {
        spawn = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        playerSpawn();
    }

    public void playerSpawn()
    {
        if (player.transform.position.y <= -200f)
        {
            player.transform.position = respawnPoint.transform.position;
            Instantiate(spawnPrefab, respawnPoint.transform.position, Quaternion.identity);
            spawn[2].Play();
        }
    }
}
