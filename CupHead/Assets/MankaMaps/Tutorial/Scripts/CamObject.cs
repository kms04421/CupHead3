using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CamObject : MonoBehaviour
{
    public GameObject player;
    float y;
    // Start is called before the first frame update
    void Start()
    {
        y = player.transform.position.y + 3.45f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(player.transform.position.x +0.2f, y);
    }
}
