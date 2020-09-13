using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float MovementSpeed, Stationary, Jump;
    public bool Jumping;
    public Rigidbody2D RG2D;
    // SerializeField to make variable private but show up in the editor
    [SerializeField] LayerMask PlatformLayerMask;
    private BoxCollider2D Collider2D;

    // Start is called before the first frame update
    void Start()
    {
        RG2D = GetComponent<Rigidbody2D>();
        Collider2D = transform.GetComponent<BoxCollider2D>();
        MovementSpeed = 10.0f;
        Jump = 20.0f;
        Stationary = 0.0f;
        Jumping = true;

    }
    
    // Update is called once per frame
    void Update()
    {
        RG2D.velocity = new Vector2(Stationary, RG2D.velocity.y);

        // Horizontal Movement
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            RG2D.velocity = new Vector2(-MovementSpeed, RG2D.velocity.y);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            RG2D.velocity = new Vector2(MovementSpeed, RG2D.velocity.y);
        }
        
        // Jumping 
        if (IsGrounded() && Input.GetKey(KeyCode.UpArrow) || (IsGrounded() && Input.GetKey(KeyCode.Space)))
        {
            RG2D.velocity = new Vector2(RG2D.velocity.x, Jump);
        }
    }

    // To make object jump more than once
    void OnCollisionEnter2D(Collision2D collision)
    {
        Jumping = true;
    }

    private bool IsGrounded()
    {
        // BoxCast will only collide with platform layer
        // Make sure in editor to set platform layer to platform 
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(Collider2D.bounds.center, Collider2D.bounds.size, 0f, Vector2.down, .1f, PlatformLayerMask);
        return raycastHit2D.collider != null;
    }
}
