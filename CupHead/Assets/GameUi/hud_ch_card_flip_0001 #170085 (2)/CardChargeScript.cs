using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardChargeScript : MonoBehaviour
{
    private Image cardImage;
    private Animator animator;
    private bool FullChk = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
      cardImage =GetComponent<Image>();   
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (cardImage.fillAmount >= 1)
        {

            animator.SetBool("Apperar", true);
          
        }
        else
        {
            animator.SetBool("Apperar", false);
        }

    }

}
