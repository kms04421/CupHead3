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

 


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("PinkBossAtk")|| collision.tag.Equals("PinkObj"))
        {
            Debug.Log(collision.transform.localScale.x +" / "+ collision.transform.localScale.y);
            if (collision.tag.Equals("PinkObj") && collision.transform.localScale.Equals(Vector3.one))
            {
                Player.instance.parryAction();
                if (collision.GetComponent<PinkParryObs1>() != null)
                {
                    collision.GetComponent<PinkParryObs1>().ChangeObs();
                }
             
            }
            else
            {

                Debug.Log("½ÇÆÐ");
            }
        }
    }

}
