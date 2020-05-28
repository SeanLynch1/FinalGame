using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public GameObject ship;
    Image healthBar;
    float maxHealth  = 2000f;
    public static float health = 2000f;
    // Start is called before the first frame update
    void Start()
    {

        ship.GetComponent<ShipDamagable>().HP = 2000f;

        Debug.Log(ship.GetComponent<ShipDamagable>().HP);
        healthBar = GetComponent<Image>();
        
        health = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("health percentage equals " + health / maxHealth);
        healthBar.fillAmount = health / maxHealth;
    }
}
