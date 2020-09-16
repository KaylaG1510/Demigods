using UnityEngine;

public class LetsMove
{
    public float MovementSpeed, Stationary;
    public Rigidbody2D RG2D;
    public BoxCollider2D Collider2D;
    // SerializeField to make variable private but show up in the editor
    [SerializeField] LayerMask PlatformLayerMask;

    public LetsMove(float Moving, float NotMoving, Rigidbody2D Body2D, BoxCollider2D Collides)
    {
        MovementSpeed = Moving;
        Stationary = NotMoving;
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


}


