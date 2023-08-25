using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFilter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject filterBack_1;

    private float randPosX_1;



    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        filterBack_1.transform.Translate(Vector3.right * 8 * Time.deltaTime);
        StartCoroutine("FadeIn");

        if (filterBack_1.transform.position.x > 15)
        {
           
            randPosX_1 = Random.Range(1, 10);

            filterBack_1.transform.position = new Vector3(filterBack_1.transform.position.x -(30+ randPosX_1), filterBack_1.transform.position.y);
          
          

        }
    }
    IEnumerator FadeIn()
    {
        if (filterBack_1.activeSelf)
        {
            filterBack_1.SetActive(false);
          
        }
        else
        {
            filterBack_1.SetActive(true);

        }
       
            yield return new WaitForSeconds(0.2f);
        
    }

}
