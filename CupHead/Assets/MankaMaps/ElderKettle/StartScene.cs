using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene: MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.Play("start");
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("start") && stateInfo.normalizedTime >= 1.0f)
        {
            gameObject.SetActive(false);
        }


    }
}
