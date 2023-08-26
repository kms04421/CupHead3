using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;


public class PotatoBoss : MonoBehaviour
{
    private float AttackTime = 0f;
    private float BossSecondsTime = 0f;
    private Animator animator;
    public GameObject BossAtK1; // 흙1
    public GameObject BossAtK2; // 지렁이
    public GameObject Boss2;
    List<GameObject> list;
    int AtkCount = 1;
    float[] AtkCountList;
    float[] AllAtkCountList;
    int AllAtkCount = 0;
    Vector2 vector2;

    public AudioClip Spit; // 진흙
    public AudioClip Worm; // 지렁이
    public AudioClip Die;

    private Image imageComponent;
    private AudioSource audioSource; // 오디오 소스 컴포넌트
    private bool BossDieMove = false;


    //보스 피격시 깜박거림 
    private Material originalMaterial; // 원래 마테리얼
    public Material customMaterial; // 적용할 커스텀 마테리얼
    //보스 피격시 깜박거림 end

    private float originalValue = 0f;
    private float minRange = 0;
    private float maxRange = 0;
    private float BossHp = 50;
    private bool DieChk = false;


    // Start is called before the first frame update
    void Start()
    {
        // 보스의 원래 색상 저장
       

        maxRange = transform.position.y - 7f;
        minRange = transform.position.y;
        imageComponent = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();
        originalMaterial = imageComponent.material;


        AllAtkCountList = new float[] { 0.8f, 1.3f, 1.8f };
        AtkCountList = new float[] { 0f, 0.18f, 0.5f, 0.8f, 3f };
        list = new List<GameObject>();
        BossSecondsTime = Random.Range(5, 8);
        animator = GetComponent<Animator>();

        vector2 = new Vector2(transform.position.x - 3, transform.position.y - 3);
        for (int i = 0; i <= 2; i++)
        {
            if (i < 2)
            {
                GameObject gameObject = Instantiate(BossAtK1, vector2, Quaternion.identity);
                list.Add(gameObject);

            }
            else
            {
                GameObject gameObject1 = Instantiate(BossAtK2, vector2, Quaternion.identity);
                list.Add(gameObject1);
            }

            list[i].SetActive(false);


        }



    }

    // Update is called once per frame
    void Update()
    {
        VeggieBossManager.instance.BossHpChk((int)BossHp);
        if (BossHp <= 0)
        {
            if (!DieChk)
            {
                DieChk = true;
              
                StartCoroutine(die());
            }
            if (BossDieMove)
            {
                GameManager_1.instance.BossDieEX(transform);
                originalValue = transform.position.y; // 현재 오브제트 y값 저장

                transform.Translate(Vector3.down * 6 * Time.deltaTime); // 보스 사망시 아래로 내려가도록

                float normalizedValue = NormalizeValue(originalValue, minRange, maxRange); //내려가면서 오브젝트 위치를 계산하여 Normalize화 시켜 0~1까지로 표시
                imageComponent.fillAmount = 1 - normalizedValue; // 오브젝트 fillAmount 값 변경



                if (imageComponent.fillAmount <= 0) // fillAmount 가 0이면 다음 보스 셋
                {

                    Transform parentTransform = transform.parent;
                    Boss2.SetActive(true);
                    VeggieBossManager.instance.BossLvAdd();
                    parentTransform.gameObject.SetActive(false);
                }
            }



        }

        AttackTime += Time.deltaTime;
        if (BossSecondsTime <= AttackTime) // 공격텀
        {

            animator.SetBool("Attack", true); // 공격 애니메이션 실행
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            animator.speed = AllAtkCountList[AllAtkCount];// 보스 공격속도
            if (stateInfo.IsName("Boss_Attack") && stateInfo.normalizedTime >= AtkCountList[AtkCount] && stateInfo.normalizedTime <= 1f) // 정규화된 시간이 1 이상이면 애니메이션이 종료
            {
               
                AttackBurst(); // 보스공격
                AtkCount++;
            }


            if (stateInfo.IsName("Boss_Attack") && stateInfo.normalizedTime >= 1f) // 애니메이션 종료후
            {
                animator.SetBool("Attack", false);// 공격 애니메이션 끝
                animator.speed = 0.8f;
                AtkCount = 0; // 진흙과 지렁이 공격구분방법
                BossSecondsTime = Random.Range(2f, 4f); // 보스 공격 간격 랜덤설정
                AttackTime = 0f; 
                AllAtkCount++;
            }
            if (AllAtkCount == 3) //지렁이 발사후 진흙으로 변경
            {

                AllAtkCount = 0;
            }


        }


    }


    private void AttackBurst()
    {
        switch (AtkCount)// 보스 공격 카운터에 따라 발사
        {
            case 1:
                list[0].SetActive(true);
                audioSource.PlayOneShot(Spit);
                StartCoroutine(rollBack(0));
                break;
            case 2:
                list[1].SetActive(true);
                audioSource.PlayOneShot(Spit);
                StartCoroutine(rollBack(1));
                break;
            case 3:
                list[2].SetActive(true);
                audioSource.PlayOneShot(Worm);
                StartCoroutine(rollBack(2));
                break;
        }


    }

    private IEnumerator rollBack(int num) // 보스공격 원위치로 
    {
        yield return new WaitForSeconds(2f);
        list[num].transform.position = vector2;
        list[num].SetActive(false);

    }

    private IEnumerator die() // 보스 사망시 밑으로 내려가는 효과
    {
        audioSource.PlayOneShot(Die);
        animator.SetTrigger("die");
        yield return new WaitForSeconds(1.0f);
       
        BossDieMove = true;


    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 총알명
        if (collision.tag.Equals("PlayerAttack"))
        {
            StartBlinkEffect();
            BossHp -= 1;
        }
        if (collision.tag.Equals("PlayerAttackEx"))
        {
            StartBlinkEffect();
            BossHp -= 5;
        }

    }
    private float NormalizeValue(float value, float minValue, float maxValue) // 보스 사라지는 효과를 위한 좌표를 계산여 자연수화시키는 로직
    {
        float range = maxValue - minValue;
        return (value - minValue) / range;
    }

    //깜박거림 
    public void StartBlinkEffect()
    {
        // 깜박임 효과 시작 코루틴 호출
        StartCoroutine(BlinkEffectCoroutine());
    }

    private IEnumerator BlinkEffectCoroutine()
    {
        // 깜박임 효과를 위한 임시 색상
        Material tempColor = customMaterial;

        // 색상 변경
        imageComponent.material = tempColor;

        // 일정 시간 동안 대기
        yield return new WaitForSeconds(0.02f);

        // 원래 색상으로 복원
        imageComponent.material = originalMaterial;
    }
    //ㄲ
}
