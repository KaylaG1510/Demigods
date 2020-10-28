using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject slashPrefab;
    private float nextAttackTime;
    private float attackDelay = 2f;

    void start()
    {
        //Animator an = gameObject.GetComponent<Animator>();
        //an.SetTrigger("Charge");
        //an.SetBool("hold", true);
        nextAttackTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
                nextAttackTime = Time.time + attackDelay;
            }  
        }
    }

    void Shoot ()
    {
        GameObject test = Instantiate(slashPrefab, FirePoint.position, FirePoint.rotation);
        Destroy(test, 2f);
    }
}
