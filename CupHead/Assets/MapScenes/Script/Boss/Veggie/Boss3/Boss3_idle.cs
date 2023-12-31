using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Boss3_idle : MonoBehaviour
{
    public static Boss3_idle instance;
    public GameObject Eye; //광눈
    public GameObject carrotFly; // 날아오르는 당근
    public GameObject carrotBoom;// 유도미사일
    public GameObject ring;// 링공격
    public GameObject charge;//차지
    public GameObject carrotFlyEmpty; // 당근을 넣을 부모
    public GameObject Boss3mpty;
    List<GameObject> ringList = new List<GameObject>();
    List<GameObject> carrotFlyList = new List<GameObject>();
    List<GameObject> carrotBoomList = new List<GameObject>();


    public AudioClip ringAudio;

    public AudioClip chargeAudio;

    public AudioClip dieAudio;

    private AudioSource audioSource;
    //보스 피격시 깜박거림 
    private Image imageComponent;
    private Material originalMaterial; // 원래 마테리얼
    public Material customMaterial; // 적용할 커스텀 마테리얼
    //보스 피격시 깜박거림 end
    private GameObject inputGameObj; // 생성용 오브젝트 
    private Transform target;
    private float atkTime = 0; //공격시간
    private float setTime = 0;
    private int atkCount = 0;
    private int allAtkCount = 0;
    private Animator animator;
    private int atkType = 0; // 공격 타입 
    private bool BossDie = false;
    private int BossHp = 50;
    Vector2 targetPos;
    // Start is called before the first frame update


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(instance);
        }
    }
    void Start()
    {
        imageComponent = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();
        setTime = 3;
        animator = GetComponent<Animator>();

        //날아오르는 당근
        for (int i = 0; i < 4; i++)
        {
            inputGameObj = Instantiate(carrotFly);
            carrotFlyList.Add(inputGameObj);
            carrotFlyList[i].SetActive(false);
            carrotFlyList[i].transform.SetParent(carrotFlyEmpty.transform);
        }

        //유도당근 리스트
        for (int i = 0; i < 10; i++)
        {
            inputGameObj = Instantiate(carrotBoom);
            carrotBoomList.Add(inputGameObj);
            carrotBoomList[i].SetActive(false);

        }
        for (int i = 0; i < 5; i++)
        {
            inputGameObj = Instantiate(ring, new Vector3(transform.position.x, transform.position.y + 1.7f, 0), Quaternion.identity);
            ringList.Add(inputGameObj);
            ringList[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        VeggieBossManager.instance.BossHpChk((int)BossHp);
        atkTime += Time.deltaTime;


       

        if (BossHp < 0)
        {
            if (!BossDie)
            {
                if (GameManager_1.instance.Clear == false)
                {
                    GameManager_1.instance.BossClear();
                }
             
                BossDie = true;
                audioSource.PlayOneShot(dieAudio);
                
            }
            GameManager_1.instance.BossDieEX(transform);
            animator.SetTrigger("Die");
            if (Eye.activeSelf)
            {
                Eye.SetActive(false);
            }
        }
        else
        {

            if (atkTime > setTime)
            {
                if (atkType == 0)// 유도 당근
                {
                    setTime = 4;
                    carrotBoomAtk();
                    atkTime = 0;

                    atkType = Random.Range(0, 2);
                }
                else if (atkType == 1)// 링공격
                {
                    setTime = 2;

                    animator.SetBool("Shoot", true);

                    AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

                    if (stateInfo.IsName("carrot_shoot_transition") && stateInfo.normalizedTime > 1f) // 링공격 준비
                    {

                        audioSource.PlayOneShot(chargeAudio);
                        animator.SetBool("Shat", true);

                        Eye.SetActive(true);

                    }
                    if (stateInfo.IsName("carrot_shoot") && stateInfo.normalizedTime > 1f)//링공격 시작
                    {
                        if (atkCount == 0)
                        {
                            audioSource.PlayOneShot(ringAudio);
                            target = FindObjectOfType<Player>().transform; // 타겟 포지션
                            targetPos = new Vector2(target.position.x, target.position.y); // 타겟 위치 저장

                            charge.SetActive(true);
                            setTime = 0.5f;
                            atkTime = 0;
                            atkCount++;
                            return;
                        }
                        if (atkCount < 6)
                        {
                            charge.SetActive(false);
                            atkCount++;
                            RingAtk();
                            setTime = 0.1f;
                            atkTime = 0;
                        }
                        else
                        {

                            audioSource.Stop();
                            allAtkCount++;
                            atkCount = 0;
                            if (allAtkCount == 3)
                            {
                             
                                Eye.SetActive(false);
                                animator.SetBool("Shoot", false);
                                animator.SetBool("Shat", false);
                                allAtkCount = 0;
                                atkType = Random.Range(0, 2);
                            }
                        }



                    }

                }



            }

            for (int i = 0; i < 4; i++) // 유도당근 공격준비
            {
                if (carrotFlyList[i].transform.position.y > 7f)
                {
                    if (carrotFlyList[i].activeSelf)
                    {
                        carrotFlyList[i].SetActive(false);
                        for (int j = 0; j < carrotBoomList.Count - 1; j++)
                        {
                            if (!carrotBoomList[j].activeSelf)
                            {
                                carrotBoomList[j].transform.position = carrotFlyList[i].transform.position;
                                carrotBoomList[j].SetActive(true);
                                break;
                            }
                        }
                    }

                }

            }

        }


    }
    Vector2[] vectorList = new Vector2[]
      {
            new Vector3 (-5, -3,0),
            new Vector3 (-8, -4,0),
            new Vector3 (5, -2,0),
            new Vector3 (8, -1, 0)
      }; // 랜덤 리스트 

    //날아오르는 당근세팅
    private void carrotBoomAtk()
    {

        int rnad1 = Random.Range(0, 2);
        int rnad2 = Random.Range(2, 4);

        carrotFlyList[0].transform.position = vectorList[rnad1]; // 날아오르는 당근 위치생성
        carrotFlyList[1].transform.position = vectorList[rnad2];
        carrotFlyList[0].SetActive(true);
        carrotFlyList[1].SetActive(true);
        carrotFlyList.Reverse();

    }

    private void RingAtk()
    {
        for (int i = 0; i < ringList.Count; i++)
        {
            if (!ringList[i].activeSelf)
            {

                Vector2 ringVector = new Vector2(transform.position.x, transform.position.y + 1.7f);

                ringList[i].transform.position = ringVector; //Ring 위치 설정

                Vector2 directionToTarget = targetPos - ringVector;


                float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

                ringList[i].transform.rotation = targetRotation;
                ringList[i].SetActive(true);
                break;
            }
        }
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
            BossHp -= 5;
            StartBlinkEffect(); ;
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
