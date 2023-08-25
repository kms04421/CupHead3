using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMoving : MonoBehaviour
{
    private float speed = 4f;
    private Rigidbody2D ghostRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        ghostRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        ghostRigidbody.gravityScale = 0f;
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if(transform.position.y >5)
        {
            transform.position = new Vector2(transform.position.x, -8);
        }
        
    }
}
