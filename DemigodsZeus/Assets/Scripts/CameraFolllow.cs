using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolllow : MonoBehaviour
{
    private Vector2 velocity;
    public float smoothTimeX;   //smooth out camera follow

    public GameObject playerReference;  //GameObject camera follows

    // Start is called before the first frame update
    void Start()
    {
        //give reference to the correct GameObject in inspector
        playerReference = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        //set x position to reference players x position with offset of +500 pixels
        float posX = Mathf.SmoothDamp(transform.position.x, playerReference.transform.position.x + 500, ref velocity.x, smoothTimeX);
        //float posY = Mathf.SmoothDamp(transform.position.y, playerReference.transform.position.y - 500, ref velocity.y, smoothTimeY);

        //set and update new camera position
        transform.position = new Vector3(posX, transform.position.y, transform.position.z);
    }
}
