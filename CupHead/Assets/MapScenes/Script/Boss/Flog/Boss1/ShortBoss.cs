using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShortBoss : MonoBehaviour
{
    private Animator animator;
    private float animatorTime = 0f;
    private float setTime = 500f;
    private GameObject saveObj = default;
    //스테이지 1 공격 
    public GameObject shortFrog;// 펀치날리는 공격
    public GameObject shortFrog_P;// 핑크 펀치날리는 공격 
    Vector2[] shortFrogVector; // 펀치 날아갈위치 저장
    int shortFrogCount = 0; // 펀치 카운터 
    bool shortUpDown = false;
    private Rigidbody2D rigidbody2D;
    public GameObject Boss2;

    List<GameObject> shortFrogList;// 펀치 리스트 
    float atkTime = 0f;
    int[] AtktimeList = new int[]
    {
        6,8
    };
    int number = 0;
    //스테이지1 end
  

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        shortFrogList =new List<GameObject>();
        shortFrogVector = new Vector2[]
        {       
            new Vector2(transform.position.x -3.5f,transform.position.y-2f),
            new Vector2(transform.position.x -3.5f,transform.position.y-0.5f),
            new Vector2(transform.position.x -3.5f,transform.position.y+1f),
        };
        int p_shortFrog = Random.Range(0, 3);
        for (int i = 0; i < 6; i++)
        {
            saveObj = default;
            if (p_shortFrog == i)
            {
                
                saveObj = Instantiate(shortFrog_P, transform.position, shortFrog.transform.rotation);
            }
            else
            {
                saveObj = Instantiate(shortFrog, transform.position, shortFrog.transform.rotation);
            }
                 
            shortFrogList.Add(saveObj);
            shortFrogList[i].SetActive(false);
        }

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        if (BossManager.instance.BossHp <= 0 )
        {
            gameObject.SetActive(false);
            Boss2.SetActive(true);
        }


        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("shortFrog") && stateInfo.normalizedTime >= 0.99f)
        {
            animator.SetTrigger("Idle");
        }


        if (BossManager.instance.BossChk == 2) // true일때 Tallfrog 패턴사용
        {
            animatorTime += Time.deltaTime;

            animator.SetBool("Atk1", true);
            AnimatorStateInfo stateInfoAtk = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("ShortFrogAtk") && stateInfo.normalizedTime >= 0.99f)
            {
                animator.SetBool("Atk2", true);
                number = Random.Range(0, 2);
                setTime = AtktimeList[number];
            }

            if (stateInfo.IsName("ShortFrogAtk2"))
            {
                shortFrogAtk();
            }

            if (animatorTime > setTime)
            {
                animatorTime = 0;
                animator.SetBool("Atk3", true);
            }
            if (stateInfo.IsName("ShortFrogAtk3") && stateInfo.normalizedTime >= 0.99f)
            {
                animator.SetBool("Atk1", false);
                animator.SetBool("Atk2", false);
                animator.SetBool("Atk3", false);
                setTime = 500f;

                BossManager.instance.AtkChange(20);
            }

        }
    }

    private void shortFrogAtk()
    {
        atkTime += Time.deltaTime;
        if(atkTime >= 0.7f)
        {
            atkTime = 0;
            for(int i = 0; i < shortFrogList.Count; i++)
            {
                if (!shortFrogList[i].activeSelf)
                {
                    shortFrogList[i].transform.position = shortFrogVector[shortFrogCount];

                    shortFrogList[i].SetActive(true);
                    break;

                }
            }

            if (shortUpDown == false)
            {
                shortFrogCount++;
            }
            else
            {
                shortFrogCount--;
            }


            if (shortFrogCount == 2 || shortFrogCount == 0)
            {
              if(shortUpDown == false)
                {
                    shortUpDown = true;
                }
                else
                {
                    shortUpDown = false;
                }
            }
        }

       
    }
    private void OnTriggerEnter2D(Collider2D collision)// 플레이어 공격 명중
    {
        if (collision.tag.Equals("atk"))
        {
            BossManager.instance.BossHpMinus();
        }
    }
}
