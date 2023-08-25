using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Second : MonoBehaviour
{
    public GameObject PressText;

    private RectTransform rectTrans;
    // Start is called before the first frame update
    void Start()
    {
        rectTrans = PressText.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectTrans.anchoredPosition = new Vector2(0, -300);
    }
}
