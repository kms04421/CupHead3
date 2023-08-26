using UnityEngine;

public class BossManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static BossManager instance;

    private float animatorTime = 0f;
    private float setTime = 2.5f;
    private int atkChk = 1;
    public int atkCount1 = 0;// Ű��������
    public int atkCount = 0; // Űū����
    public int BossChk = 0;// ���� ��������
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


      
        if (BossHp <= 0) // ���� hpüũ 
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
            

        }// ���� hpüũ ��
        else
        {
            if (BossChk == 3) // ���� LV �� 
            {
                animatorTime += Time.deltaTime;
                if (animatorTime > 2)
                {
                    animatorTime = 0;
                    BossChk = 0;
                }
            }// ���� ���� Lv ����


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

            if (BossChk == 0) // ���� ���� üũ 
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
            } // ���� üũ ��
        }


       
    }

    public void AtkChange(int i) //���� ���� ���� 
    {

        if (BossChk != 0 ) //2�������� 
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

                BossChk = 4; // ����������
            
            }
            else
            {
              
                BossChk = 0; // ���ݺ�ȭ
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
