using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParringScript : MonoBehaviour
{

    private CircleCollider2D circleCollider;
    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
     
    }

 


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag.Equals("PinkBossAtk")|| collision.tag.Equals("PinkObj"))
        {
            Player.instance.parryAction();
        }
    }

}
