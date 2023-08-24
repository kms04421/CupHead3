using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjOff : MonoBehaviour
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
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f) // 애니메이션 퍼센트 체크
        {
            gameObject.SetActive(false);

        }
        
    }
}
