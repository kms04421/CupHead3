using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingFx : MonoBehaviour
{
    private Animator endFx;
    // Start is called before the first frame update
    void Start()
    {
        endFx = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(endFx.GetCurrentAnimatorStateInfo(0).normalizedTime>=1f)
        {
            gameObject.SetActive(false);
        }
    }
}
