using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        transform.position = new Vector2(target.position.x, target.position.y);
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime*speed);
        transform.position = new Vector3(transform.position.x, transform.position.y, -500f);
    }
}
