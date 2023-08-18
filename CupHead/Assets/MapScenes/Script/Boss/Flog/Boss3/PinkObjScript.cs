using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkObjScript : MonoBehaviour
{
    private CircleCollider2D circleCollider;
    private bool chk = false;
    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (chk) // ��ũ ������Ʈ �и��� �۵��Ͽ� ���� ��ŸƮ 
        {
            TallForg3.Instance.slotStart();
            chk = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.tag.Equals("Farring"))
        {
            Debug.Log("Pink");
            chk = true;
        }
    }
}
