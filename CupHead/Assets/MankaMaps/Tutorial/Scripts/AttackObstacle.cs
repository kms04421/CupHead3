using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObstacle : MonoBehaviour
{
    private int hp =10;
    private SpriteRenderer roofrenderer;
    public GameObject Roof1;
    public GameObject Roof2;
    public GameObject effect;
    private Animator effectAni;
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        roofrenderer = gameObject.GetComponent<SpriteRenderer>();
        effectAni = effect.GetComponent<Animator>();
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <=0)
        {
            roofrenderer.enabled = false;
            Destroy(Roof1);
            Destroy(Roof2);
            if (count <= 0)
            {
                effect.SetActive(true);
                count++;
            }
            if (effectAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                //Debug.LogFormat("들어오니?");
                // 애니메이션 재생이 완료되면 GameObject를 비활성화
                effect.SetActive(false);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerAttack")
        {
            hp -= 2;
        }
    }
}
