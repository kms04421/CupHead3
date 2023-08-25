using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkFog : MonoBehaviour
{
    private float scaleRate = 2f;
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
        transform.position += new Vector3(0, 1f, 0) * Time.deltaTime;
    }
}
