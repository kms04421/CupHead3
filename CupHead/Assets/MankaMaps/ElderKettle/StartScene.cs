using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene: MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Start") && stateInfo.normalizedTime >= 1.0f)
        {
            gameObject.SetActive(false);
        }


    }
}
