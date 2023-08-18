using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObstacle : MonoBehaviour
{
    private int hp =10;
    private SpriteRenderer roofrenderer;
    public GameObject Roof1;
    public GameObject Roof2;
    // Start is called before the first frame update
    void Start()
    {
        roofrenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <=0)
        {
            roofrenderer.enabled = false;
            Destroy(Roof1);
            Destroy(Roof2);
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
