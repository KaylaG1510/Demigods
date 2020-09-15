using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    public float MovementSpeed, Stationary, Jump;
    public bool Jumping;
    public Rigidbody2D RG2D;
    // SerializeField to make variable private but show up in the editor
    [SerializeField] LayerMask PlatformLayerMask;
    public BoxCollider2D Collider2D;
    LetsMove letsMove;

    // Start is called before the first frame update
    void Start()
    {
        letsMove = new LetsMove(10.0f, 0.0f, 20.0f, true, RG2D = GetComponent<Rigidbody2D>(), Collider2D = transform.GetComponent<BoxCollider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        letsMove.MoveCharacterHorizontal(MovementSpeed, RG2D.velocity.y);
        letsMove.CharacterJump(RG2D.velocity.x, Jump);
    }

    // To make object jump more than once
    void OnCollisionEnter2D(Collision2D collision)
    {
        Jumping = true;
    }
}

