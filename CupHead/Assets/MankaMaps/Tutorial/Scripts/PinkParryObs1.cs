using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkParryObs1 : MonoBehaviour
{
    [SerializeField]
    public GameObject pinkParry2;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeObs()
    {
        if (transform.localScale.Equals(Vector3.one))
        {
            card.instance.ChargeFillAdd();
            gameObject.GetComponent<Animator>().Play("ParrySpark");
            transform.localScale = Vector2.one * .99f;
            pinkParry2.transform.localScale = Vector3.one * .99f;
        }
    }

    public void enableNextObj()
    {
        pinkParry2.transform.localScale = Vector3.one;
    }
    public void disableObj()
    {
        gameObject.transform.localScale = Vector3.zero;
    }
}
