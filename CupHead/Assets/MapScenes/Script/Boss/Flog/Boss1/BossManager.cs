using UnityEngine;

public class BossManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static BossManager instance;

    private float animatorTime = 0f;
    private float setTime = 2.5f;
    private int atkChk = 1;
    public int atkCount1 = 0;// 키작은보스
    public int atkCount = 0; // 키큰보스
    public int BossChk = 0;// 보스 공격패턴
    public bool BoosDie = false;
    public int BossHp = 50;
    public int BossLv = 0;
    public bool ready = false;
    private void Awake()
    {
        if (instance == null)
        {
           
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


      
        if (BossHp <= 0) // 보스 hp체크 
        {
           

            if (BossLv == 0 )
            {
               

                BossHp = 50;
                atkChk = 0;
                animatorTime = 0;
                BossChk = 3;
                BossLv++;
              
            }
            else if (BossLv == 1)
            {
                

                ready = true;
             
                atkChk = 0;
                animatorTime = 0;
                BossChk = 3;
                BossLv++;
            }
            else if (BossLv == 2)
            {
                BoosDie = true;
            }
            

        }// 보스 hp체크 끝
        else
        {
            if (BossChk == 3) // 보스 LV 텀 
            {
                animatorTime += Time.deltaTime;
                if (animatorTime > 2)
                {
                    animatorTime = 0;
                    BossChk = 0;
                }
            }// 보스 공격 Lv 종료


            if (BossChk == 4)
            {
          
                animatorTime += Time.deltaTime;
                if (animatorTime >= 2)
                {
                    atkChk = 0;
                    animatorTime = 0;
                    BossChk = 0;

                    atkCount = 0;
                    atkCount1 = 0;
                }
            }

            if (BossChk == 0) // 공격 순서 체크 
            {
                animatorTime += Time.deltaTime;
                if (animatorTime > setTime)
                {
                    animatorTime = 0;
                    if (atkChk % 2 == 0)
                    {

                        BossChk = 1;
                    }
                    else
                    {

                        BossChk = 2;
                    }
                }
            } // 순서 체크 끝
        }


       
    }

    public void AtkChange(int i) //공격 순서 변경 
    {

        if (BossChk != 0 ) //2스테이지 
        {



            if (i == 0)
            {
                atkCount++;
            }
            else if (i == 1)
            {
                atkCount1++;
            }
      


            if (atkCount > 0 && atkCount1 > 0)
            {

                BossChk = 4; // 다음레벨로
            
            }
            else
            {
              
                BossChk = 0; // 공격변화
            }



            atkChk++;
        }


    }

    public void BossHpMinus(int i) // hp-
    {
       

        if (i == 0)
        {
            if(BossHp > 0)
            {
                BossHp -= 1;
            }
          
        }
        else if(i == 1)
        {

            if (BossHp > 0)
            {
                BossHp -= 5;
            }
        }
      
    }
}
