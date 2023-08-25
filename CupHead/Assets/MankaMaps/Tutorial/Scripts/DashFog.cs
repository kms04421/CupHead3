using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashFog : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);

        if (Player.instance.movement.x < 0)
        {
            Vector3 currentRotation = gameObject.transform.eulerAngles;
            gameObject.transform.eulerAngles = new Vector3(currentRotation.x, currentRotation.y + 180f, currentRotation.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
