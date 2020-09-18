using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    LetsMove letsMove;

    public float MovementSpeed, Stationary, Jump;
    public bool Jumping;
    public Rigidbody2D RG2D;
    // SerializeField to make variable private but show up in the editor
    [SerializeField] LayerMask PlatformLayerMask;
    public BoxCollider2D Collider2D;
    // Movement Sounds
    AudioSource audioMovement;
    public bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        Jump = 20.0f;
        Collider2D = transform.GetComponent<BoxCollider2D>();
        letsMove = new LetsMove(10.0f, 0.0f, RG2D = GetComponent<Rigidbody2D>());
        audioMovement = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        letsMove.MoveCharacterHorizontal(MovementSpeed, RG2D.velocity.y);
        CharacterJump(RG2D.velocity.x, Jump);

        if(RG2D.velocity.x !=0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if(isMoving == true)
        {
            if(!audioMovement.isPlaying)
            {
                audioMovement.volume = Random.Range(0.4f, 0.6f);
                audioMovement.pitch = Random.Range(0.1f, 0.2f);
                audioMovement.Play();
            }
        }
        else
        {
            audioMovement.Stop();
        }

    }

    public Vector2 CharacterJump(float z, float h)
    {
        // Jumping 
        if (IsGrounded() && Input.GetKey(KeyCode.UpArrow) || (IsGrounded() && Input.GetKey(KeyCode.Space)))
        {
            RG2D.velocity = new Vector2(z, h);
        }
        return RG2D.velocity;
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

