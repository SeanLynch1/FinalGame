using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDamagable : MonoBehaviour
{
    public enum DamageableType { Hull, Armor, Shield, System }; //list of type values you can use, are TYPES not Objects
    public enum DamageableStatus { Ok, MinorDamage, Broken, Destroyed };

    public float maxHP;
    public float  HP = 2000f;

    public DamageableType damageableType = DamageableType.Hull;//big part of the ship
    public DamageableStatus damageableStatus = DamageableStatus.Ok;

    public GameObject explosionPrefab;

    void Start()
    {
        Debug.Log( "health is " + HealthBarScript.health);
    }
    private void OnCollisionEnter(Collision collision)
    {
        float damageAmount;

      
        if (collision.gameObject.GetComponent<Ordinance>())
        {
            damageAmount = GetDamageAmount(collision.gameObject.GetComponent<Ordinance>());
            HealthBarScript.health -= damageAmount;
        }
        else
        {
            damageAmount = 0f;
            HealthBarScript.health -= damageAmount;
        }
        if(collision.gameObject.tag == "EnemyFaction")
        {
            damageAmount = 100f;
             HealthBarScript.health -= damageAmount;
        }
        if (CheckForCriticalHit(damageAmount)) TakeCriticalHit();
        {
            if (damageableStatus == DamageableStatus.Destroyed) Destroyed();
            TakeDamage(damageAmount);
        }

    }

    private void TakeDamage(float damageAmount)
    {
        HP = HP - damageAmount;


        if (HP <= 0)
        {
            Destroyed();
        }
    }

    private void Destroyed()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private float GetDamageAmount(Ordinance ordinance)
    {
        return damageableType == DamageableType.Shield ? ordinance.shieldDamage : ordinance.armorDamage;
    }

    public bool CheckForCriticalHit(float damageAmount)
    {
        if (damageAmount > (HP * 0.66F))
        {
            return true;

        }
        else
        {
            return false;
        }
    }

    public void TakeCriticalHit()
    {
        int randomNumber = Random.Range(0, 100);
        if (randomNumber > 50) damageableStatus = DamageableStatus.MinorDamage;
        if (randomNumber > 80) damageableStatus = DamageableStatus.Broken;
        if (randomNumber >= 99) damageableStatus = DamageableStatus.Destroyed;

    }
}
