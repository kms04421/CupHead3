using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowFire : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxCollider2D collider;
    Vector2 currentSize;
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        currentSize = collider.size;
    }

    // Update is called once per frame
    void Update()
    {
        // y축 크기를 2배로 만들기
        currentSize.y *= 2f;

        // 크기 적용
        collider.size = currentSize;
    }
}
