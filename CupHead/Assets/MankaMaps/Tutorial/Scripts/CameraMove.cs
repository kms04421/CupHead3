using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;
    private float minX = 0, maxX = 70;

    void LateUpdate()
    {
        float x = Mathf.Clamp(player.position.x, minX, maxX);

        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}
