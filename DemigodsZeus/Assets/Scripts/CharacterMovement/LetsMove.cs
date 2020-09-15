using UnityEngine;

public class LetsMove
{
    public float MovementSpeed, Stationary, Jump;
    public bool Jumping;
    public Rigidbody2D RG2D;
    public BoxCollider2D Collider2D;
    // SerializeField to make variable private but show up in the editor
    [SerializeField] LayerMask PlatformLayerMask;

    public LetsMove(float Moving, float NotMoving, float Up, bool isJumping, Rigidbody2D Body2D, BoxCollider2D Collides)
    {
        MovementSpeed = Moving;
        Stationary = NotMoving;
        Jump = Up;
        Jumping = isJumping;
        RG2D = Body2D;
        Collider2D = Collides;
    }
    public Vector2 MoveCharacterHorizontal(float x, float y)
    {
        // Horizontal Movement
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            RG2D.velocity = new Vector2(-x, y);
            return RG2D.velocity;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            RG2D.velocity = new Vector2(x, y);
            return RG2D.velocity;
        }
        RG2D.velocity = new Vector2(Stationary, y);
        return RG2D.velocity;
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

    private bool IsGrounded()
    {
        // BoxCast will only collide with platform layer
        // Make sure in editor to set platform layer to platform 
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(Collider2D.bounds.center, Collider2D.bounds.size, 0f, Vector2.down, .1f, PlatformLayerMask);
        return raycastHit2D.collider != null;
    }

}


