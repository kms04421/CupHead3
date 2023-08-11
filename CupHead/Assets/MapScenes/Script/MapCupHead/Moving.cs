using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Android.Types;
using UnityEngine;

public class Moving : MonoBehaviour
{
    private float dirX;
    private float dirY;
    private bool isTalk;
    private Rigidbody2D headMoving;
    private float speed = 250f;
    private Animator ani;

    private bool isRight;
    private bool isDownDiagonal;
    private bool isUpDiagonal;
    private bool isUp;
    private bool isDown;

    public GameObject Z;
    private float diagonalSpeed;
    // Start is called before the first frame update
    void Start()
    {
        headMoving = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        diagonalSpeed = (float)(speed * 0.75);
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(dirX)>0 && Mathf.Abs(dirY)>0  )
        {
            headMoving.velocity = new Vector2(dirX * diagonalSpeed, dirY *diagonalSpeed);
        }
        else
        {
            headMoving.velocity = new Vector2(dirX * speed, dirY * speed);
        }
        #region 상하좌우 움직임
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isUp = true;
            ani.SetBool("UpWalk",true);
        }
        if(Input.GetKeyUp(KeyCode.UpArrow))
        {
            isUp = false;
            ani.SetBool("UpWalk", false);
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            isDown = true;
            ani.SetBool("DownWalk", true);
        }    
        if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            isDown = false;
            ani.SetBool("DownWalk", false);
        }
        if(Mathf.Abs(dirX) >0)
        {
            isRight = true;
            ani.SetBool("RightWalk", true);
        }
        else
        {
            isRight = false;
            ani.SetBool("RightWalk", false);
        }
        #endregion
        #region 대각선이동구현
        if(isUp == true&& isRight == true)
        {
            ani.SetBool("UpDiagonalWalk", true);
        }
        else
        {
            ani.SetBool("UpDiagonalWalk", false);
        }
        if (isDown == true && isRight == true)
        {
            ani.SetBool("DownDiagonalWalk", true);
        }
        else
        {
            ani.SetBool("DownDiagonalWalk", false);
        }
        #endregion
        #region 좌우반전구현
        if (dirX > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (dirX < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        #endregion

        #region
        if(Mathf.Abs(dirX) >0 && Mathf.Abs(dirY)>0)
        {
            ani.SetBool("idle", false);
        }
        else
        {
            ani.SetBool("idle", true);
        }

        #endregion
    }

    public void OnCollisionEnter(Collision collision)
    {
        Z.SetActive(true);
    }
    public void OnCollisionExit(Collision collision)
    {
        Z.SetActive(false);
    }
}
