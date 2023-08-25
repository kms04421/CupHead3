using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    private float scaleRate = 10f;
    // Start is called before the first frame update
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += new Vector3(scaleRate, scaleRate, 0) * Time.deltaTime;

        if ((Moving.cupHead.movement.y == 0) == true)
        {
            transform.position += new Vector3(40f, 40f, 0) * Time.deltaTime;
        }
    }
}
