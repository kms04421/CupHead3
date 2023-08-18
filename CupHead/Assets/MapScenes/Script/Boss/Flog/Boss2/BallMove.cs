using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BallMove : MonoBehaviour
{
    public GameObject imptShow;

    private bool iptShow = false;
    private float animatorTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
      
    }

    // Update is called once per frame
    void Update()
    {


        if(iptShow == true )
        {
            animatorTime += Time.deltaTime;
            if (animatorTime >= 0.2f)
            {
                animatorTime = 0f;
                imptShow.SetActive(false);
                iptShow = false;
            }
        }

        if(transform.position.x > 8)
        {
            Debug.Log("지금이니?");
            gameObject.SetActive(false);
        }


        transform.Translate(Vector3.right * 8 * Time.deltaTime);

        if (transform.position.y >= 4.5f )
        {
            if(iptShow == false)
            {
                imptShow.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f,0);
                imptShow.SetActive(true);
                
                iptShow = true;
                
            }
          
            Vector3 currentRotation = transform.eulerAngles; //현재 오브젝트 각도 반전 
            currentRotation.z *= -1;//현재 오브젝트 각도 반전 
            transform.eulerAngles = currentRotation;//현재 오브젝트 각도 반전 
        }
      
        if(transform.position.y <= -3)
        {
            if (iptShow == false)
            {
                imptShow.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f,0);
                imptShow.SetActive(true);

                iptShow = true;
            }
           
            Vector3 currentRotation = transform.eulerAngles;//현재 오브젝트 각도 반전 
            currentRotation.z *= -1;//현재 오브젝트 각도 반전 
            transform.eulerAngles = currentRotation;//현재 오브젝트 각도 반전 
        }
    }
}
