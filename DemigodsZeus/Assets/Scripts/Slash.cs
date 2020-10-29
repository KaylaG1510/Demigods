using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Slash : MonoBehaviour
{
    private float speed = 250f;
    public Rigidbody2D rb;
    private int Damage;


    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        Damage = 15;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //Debug.Log(hitInfo.name);
        /*Enemy enemy = hitInfo.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(30);
        }*/
        /*if (hitInfo.name = )
        {
            Destroy(gameObject);
        }*/

        if (hitInfo.CompareTag("Enemy") || hitInfo.CompareTag("Boss"))
        {
            hitInfo.SendMessage("TakeDamage", Damage);
            Destroy(gameObject);
        }
        
    }
}
