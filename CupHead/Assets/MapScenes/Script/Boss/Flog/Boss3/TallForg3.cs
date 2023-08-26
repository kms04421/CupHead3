using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TallForg3 : MonoBehaviour
{
    public static TallForg3 Instance;
    private Animator animator;
    public GameObject ShortFrog2;

    public bool goSlot = false;
    private float animatorTime = 0f;
    private Transform target;
    //핑크 오브젝트
    public GameObject pinkobj;

    private GameObject saveObj;
    //코인 공격 
    public GameObject coinSpObj;
    public GameObject coin;
    private List<GameObject> coinList;
    private bool coinAtkStart = false;

    //슬롯
    float[] stopSlotNumList;


    private AudioSource audioSource;

    public AudioClip armParry;
    public AudioClip armdown;
    public AudioClip morphed_attack;
    public AudioClip morphed_spin;
    public AudioClip morphed_dial_spin_loop;
    //보스 피격시 깜박거림 
    private SpriteRenderer imageComponent;
    private Material originalMaterial; // 원래 마테리얼
    public Material customMaterial; // 적용할 커스텀 마테리얼
    //보스 피격시 깜박거림 end
    public GameObject SlotList;
    public GameObject Slot1;
    public GameObject Slot2;
    public GameObject Slot3;
    public GameObject Slot1Atk_Up; // 호랑이 
    public GameObject Slot1Atk_Down; // 호랑이 
    public GameObject Slot2Atk; // 소
    public GameObject Slot3Atk; // 개구리 
    private List<GameObject> Slot1AtkList; // 호랑이 공격리스트
    private List<GameObject> Slot2AtkList; // 소 공격리스트
    private List<GameObject> Slot3AtkList; // 개구리 공격리스트
    int slotNum1 = 0;
    int slotNum2 = 0;
    int slotNum3 = 0;
    private float slotTime = 0f;
    private bool slotRimeChk = false;
    private bool armEndChk = false;
    private bool slotChk = true;
    private float SetTime = 1.5f;
    private bool slotAtkStart = false;

    private bool bossDieExChk = false;
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        imageComponent = GetComponent<SpriteRenderer>();
        originalMaterial = imageComponent.material;
        audioSource = GetComponent<AudioSource>();
        Slot1AtkList = new List<GameObject>();
        Slot2AtkList = new List<GameObject>();
        Slot3AtkList = new List<GameObject>();
        for (int i = 0; i < 8; i++) // 호랑이 생성 
        {

            saveObj = Instantiate(Slot2Atk);
            Slot2AtkList.Add(saveObj);
            Slot2AtkList[i].SetActive(false);
        }// 호랑이 생성  끝

        for (int i = 0; i < 16; i++) // 호랑이 생성 
        {
            if (i > 8)
            {
                saveObj = Instantiate(Slot1Atk_Up);//위에 불기둥
            }
            else
            {
                saveObj = Instantiate(Slot1Atk_Down);//아래 불기둥
            }

            Slot1AtkList.Add(saveObj);
            Slot1AtkList[i].SetActive(false);

        }// 소 생성  끝

        for (int i = 0; i < 8; i++) // 개구리 생성 
        {
            saveObj = Instantiate(Slot3Atk);
            Slot3AtkList.Add(saveObj);
            Slot3AtkList[i].SetActive(false);
        }// 개구리 생성  끝





        stopSlotNumList = new float[] { -0.6f, 1.44f, 0.5f, -0.18f }; //호랑이 소 개구리 

        coinList = new List<GameObject>();
        animator = GetComponent<Animator>();

        for (int i = 0; i < 8; i++)//코인 생성 
        {
            saveObj = Instantiate(coin);
            coinList.Add(saveObj);
            coinList[i].SetActive(false);
        }//코인 생성  끝

    }

    // Update is called once per frame
    void Update()
    {
        if (BossManager.instance.BoosDie)
        {
            bossDieExChk = true;
                GameManager_1.instance.BossDieEX(transform);
           




            animator.SetTrigger("Die");
            return;
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (goSlot == true)// 슬롯 내려가는 효과 , 공격 전환 
        {

            if (armEndChk == false)
            {
                animatorTime = 0;
                animator.SetBool("ArmEnd", true);
                armEndChk = true;
                slotRimeChk = false;
            }
         
            animatorTime += Time.deltaTime;
            if (animatorTime > 3f && slotChk && Slot1.transform.position.y > 1.5f) // 룰렛 멈출 숫자 적용및 시간차 멈춤
            {
                animatorTime = 0;

                if (slotNum1 == 0)
                {
                    slotNum1 = Random.Range(1, 4);
                }

                slotChk = false;
            }

            if (animatorTime > 1f && slotChk && Slot2.transform.position.y > 1.5f && slotNum1 != 0)
            {

                animatorTime = 0;
                if (slotNum2 == 0)
                {
                    slotNum2 = slotNum1;
                }


                slotChk = false;
            }
            if (animatorTime > 1f && slotChk && Slot3.transform.position.y > 1.5f && slotNum2 != 0)
            {
            
              
                animatorTime = 0;
                if (slotNum3 == 0)
                {
                    slotNum3 = slotNum2;
                }
                slotChk = false;
            }// 룰렛 멈출 숫자 적용및 시간차 멈춤끝


            if (Slot1.transform.position.y < stopSlotNumList[slotNum1]) { slotChk = true; }// 해당위치에서 멈춤 
            else
            {
                Slot1.transform.Translate(Vector3.down * 8 * Time.deltaTime);
            }
            if (Slot2.transform.position.y < stopSlotNumList[slotNum2]) { slotChk = true; }
            else
            {
                Slot2.transform.Translate(Vector3.down * 8 * Time.deltaTime);
            }
            if (Slot3.transform.position.y < stopSlotNumList[slotNum3])
            {

                slotRimeChk = true; // 멈출경우 slotNum3에따라 공격패턴 수행 
            }
            else
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(morphed_dial_spin_loop);
                }
                Slot3.transform.Translate(Vector3.down * 8 * Time.deltaTime);
            }// 룰렛 돌기 끝


            if (stateInfo.IsName("Tallfrog_slotman_armEnd") && stateInfo.normalizedTime <= 0.1f) //핑크공격 성공 팔올리기 
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(armParry);
                }

            }

            if (stateInfo.IsName("Tallfrog_slotman_armEnd") && stateInfo.normalizedTime >= 0.99f) //핑크공격 성공 팔올리기 
            {
               
                animator.SetBool("Arm1", false);
                animator.SetBool("Arm2", false);
                animator.SetBool("ArmEnd", false);
                pinkobj.SetActive(false);


            }

            if (slotRimeChk)
            {
                slotTime += Time.deltaTime;
            }

            if (slotTime >= 1f) //대기상태
            {
                slotTime = 0;
                slotAtkStart = true;
                animator.SetBool("Atk1", true);
                slotRimeChk = false;

                if (slotNum3 == 3)
                {
                    SetTime = 0.5f;
                }
                else
                {
                    SetTime = 0.7f;
                }

            }

            if (slotAtkStart == true) // 대기상태 후 공격모션 스타트 이유는 룰렛 돌시간필요
            {
                if (stateInfo.IsName("TallForgSlotAtk1") && stateInfo.normalizedTime >= 0.9f) //등장
                {

                    animator.SetBool("Atk2", true);
                }

                if (animatorTime >= SetTime)
                {

                    animatorTime = 0;

                    //slotNum1 에따라 공격 변경
                    if (stateInfo.IsName("TallForgSlotAtkLoop") && stateInfo.normalizedTime >= 0.2f) //공격위치로 생성
                    {

                        if (slotNum1 == 2)// 소
                        {
                            for (int i = 0; i < Slot1AtkList.Count; i++)
                            {
                                if (Random.Range(0, 2) == 0)
                                {
                                    Slot1AtkList.Reverse();
                                }

                                if (!Slot1AtkList[i].activeSelf)
                                {
                                    Slot1AtkList[i].transform.position = new Vector3(transform.position.x - 1.9f, transform.position.y - 2, 0);
                                    Slot1AtkList[i].SetActive(true);
                                    StartCoroutine(slotAtkMove(Slot1AtkList[i], true));// 이동 로직 위 아래 구분

                                    break;
                                }

                            }
                        }

                        if (slotNum1 == 1)//호랑이
                        {

                            for (int i = 0; i < Slot2AtkList.Count; i++)
                            {

                                if (!Slot2AtkList[i].activeSelf)
                                {
                                    Slot2AtkList[i].transform.position = new Vector3(transform.position.x - 1.9f, transform.position.y - 2, 0);
                                    Slot2AtkList[i].SetActive(true);
                                    StartCoroutine(slotAtkMove(Slot2AtkList[i], false));// 이동 로직 위 아래 구분
                                    break;
                                }
                            }
                        }

                        if (slotNum1 == 3)// 개구리
                        {
                            for (int i = 0; i < Slot3AtkList.Count; i++)
                            {
                                if (!Slot3AtkList[i].activeSelf)
                                {
                                    Slot3AtkList[i].transform.position = new Vector3(transform.position.x - 1.9f, transform.position.y - 2, 0);
                                    Slot3AtkList[i].SetActive(true);
                                    StartCoroutine(slotAtkMove(Slot3AtkList[i], false));// 이동 로직 위 아래 구분
                                    break;
                                }

                            }
                        }



                    }//공격위치로 생성 끝



                    if (stateInfo.IsName("TallForgSlotAtkLoop") && stateInfo.normalizedTime >= 20f) //등장
                    {

                        animator.SetBool("End", true);
                        animator.SetBool("Atk1", false);
                        animator.SetBool("Atk2", false);
                        animator.SetBool("ArmEnd", false);

                        Slot1.transform.Translate(Vector3.down * 8 * Time.deltaTime);
                        Slot2.transform.Translate(Vector3.down * 8 * Time.deltaTime);
                        Slot3.transform.Translate(Vector3.down * 8 * Time.deltaTime);
                    }

                }
            }

            if (stateInfo.IsName("TallForgSlotAtkEnd") && stateInfo.normalizedTime >= 0.9f) //등장
            {

                goSlot = false; // 슬롯패턴 off
                slotAtkStart = false;// 슬롯 도는 시간 다시 주기위해 off

                coinAtkStart = false;// 코인 공격 스타트 오프  
                animator.SetBool("End", false);
                slotNum1 = 0;
                slotNum2 = 0;
                slotNum3 = 0;
                slotChk = true;
                armEndChk = false;


            }

        }
        else
        {


            if (stateInfo.IsName("TallForg3Start") && stateInfo.normalizedTime >= 0.99f) //등장
            {
                animator.SetTrigger("Start");
            }

            if (ShortFrog2.transform.position.x > 1)
            {
                animator.SetTrigger("Start2");
                ShortFrog2.SetActive(false);
            }

            if (stateInfo.IsName("TallForg3Start2") && stateInfo.normalizedTime >= 0.9f) //3스타트
            {

                SlotList.SetActive(true);
                animator.SetTrigger("Idle");
            }

            if (stateInfo.IsName("TallForg3idle") && stateInfo.normalizedTime >= 0.5f) //대기상태
            {

                coinAtkStart = true;

            }

            if (stateInfo.IsName("TallForg3idle") && stateInfo.normalizedTime >= 15f) //대기상태 끝
            {
                animator.SetBool("Arm1", true);

            }

            if (stateInfo.IsName("Tallfrog_slotman_arm") && stateInfo.normalizedTime >= 0.99f) //슬롯머신 작동준비
            {
                animator.SetBool("Arm2", true);
                pinkobj.SetActive(true);

            }

            if (coinAtkStart)
            {
                
                animatorTime += Time.deltaTime;
                if (animatorTime > 2f)
                {
                    target = FindObjectOfType<Player>().transform;
                    animatorTime = 0f;
                    StartCoroutine(CoinAtk());
                }

            }


        }




    }

    private IEnumerator CoinAtk()//코인 생성 로직
    {

        coinSpObj.SetActive(true);

        for (int i = 0; i < coinList.Count; i++) // 코인발사
        {
            if (!coinList[i].activeSelf)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(morphed_attack);
                }
                coinList[i].transform.position = new Vector3(coinSpObj.transform.position.x + 2.8f, coinSpObj.transform.position.y + 0.7f, 0);
                coinList[i].SetActive(true);
                Vector3 direction = (target.position - coinList[i].transform.position).normalized;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

                coinList[i].transform.rotation = targetRotation;


                break;
            }

        }
        yield return new WaitForSeconds(0.3f);


        coinSpObj.SetActive(false);

    }

    public void slotStart()
    {
        goSlot = true;
    }




    Vector3 targetPosition;// 이동 위치 저장용
    private IEnumerator slotAtkMove(GameObject bossSlotAtkType, bool Up)
    {

        if (Up)
        {
            targetPosition = new Vector3(bossSlotAtkType.transform.position.x - 3, bossSlotAtkType.transform.position.y - 0.5f, 0f); // 원하는 좌표 설정 (Up)

        }
        else
        {
            targetPosition = new Vector3(bossSlotAtkType.transform.position.x - 3, bossSlotAtkType.transform.position.y - 2, 0f); // 원하는 좌표 설정(down)

        }
        float moveTime = 1f; // 이동 시간
        float elapsedTime = 0f;
        Vector3 startingPosition = bossSlotAtkType.transform.position;

        while (elapsedTime < moveTime)
        {

            bossSlotAtkType.transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / moveTime); //지정위치까지 이동 
            elapsedTime += Time.deltaTime;
            yield return null;
        }




        yield return new WaitForSeconds(1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (goSlot)
        {
            if (collision.tag.Equals("PlayerAttack"))
            {
                BossManager.instance.BossHpMinus(0);
                StartBlinkEffect();
            }
            if (collision.tag.Equals("PlayerAttackEx"))//ex공격 적중시
            {
                BossManager.instance.BossHpMinus(1);
                StartBlinkEffect();
            }
        }
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
}
