using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStart: MonoBehaviour
{
    private Animator startAni;
    // Start is called before the first frame update
    void Start()
    {
        startAni= gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(startAni.GetCurrentAnimatorStateInfo(0).normalizedTime >=1f)
        {
            gameObject.SetActive(false);
        }
    }
}
