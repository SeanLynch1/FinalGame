using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName = "MGun";
    public float shotsPerSecond = 2;
    public GameObject ordinancePrefab;
    public WeaponBarrel weaponBarrel;
    float muzzleVelocity;
    float coolDownTimer = 0;

    AudioSource laserBeam;
    // Start is called before the first frame update
    void Start()
    {
        weaponBarrel = transform.GetComponentInChildren<WeaponBarrel>();
        muzzleVelocity = ordinancePrefab.GetComponent<Ordinance>().muzzleVelocity;
        laserBeam = GetComponent<AudioSource>();
    }

   public void Fire(Vector3 ParentVelocity)
   {
        float coolDownRate = 1 / shotsPerSecond;

        if (coolDownTimer <= Time.time)
        {
            coolDownTimer = Time.time + coolDownRate;


            GameObject newProjectile = Instantiate(ordinancePrefab, weaponBarrel.transform.position, weaponBarrel.transform.rotation) as GameObject;

            Rigidbody projRB = newProjectile.GetComponent<Rigidbody>();

            projRB.velocity = newProjectile.transform.forward * 300f;

            laserBeam.Play();
        }
   }

}
