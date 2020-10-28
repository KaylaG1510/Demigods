using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject slashPrefab;
    //private Animator m_animator;

    /*void start()
    {
        m_animator = GetComponentInParent<Animator>();
    }*/
    // Update is called once per frame
    void Update()
    {
        Shoot();
        
    }

    void Shoot ()
    {
        Instantiate(slashPrefab, FirePoint.position, FirePoint.rotation); 
    }
}
