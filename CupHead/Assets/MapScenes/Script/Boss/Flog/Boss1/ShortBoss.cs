using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public AudioClip punch; // 펀치 사운드
    public AudioClip Ko2; // 위롭!
    //보스 피격시 깜박거림 
    private SpriteRenderer imageComponent;
    private Material originalMaterial; // 원래 마테리얼
    public Material customMaterial; // 적용할 커스텀 마테리얼
    //보스 피격시 깜박거림 end
    private AudioSource audioSource;
    List<GameObject> shortFrogList;// 펀치 리스트 
    float atkTime = 0f;
    int[] AtktimeList = new int[]
    {
        5,7
    };
    int number = 0;
    //스테이지1 end


    // Start is called before the first frame update
    void Start()
    {
        imageComponent = GetComponent<SpriteRenderer>();
        originalMaterial = imageComponent.material;
        audioSource = GetComponent<AudioSource>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        shortFrogList = new List<GameObject>();
        shortFrogVector = new Vector2[]
        {
            new Vector2(transform.position.x -3.5f,transform.position.y-2f),//하단
            new Vector2(transform.position.x -3.5f,transform.position.y-0.8f),//중간
            new Vector2(transform.position.x -3.5f,transform.position.y+1f)//상단
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


        if (BossManager.instance.BossHp <= 0)
        {
            gameObject.SetActive(false);
            Boss2.SetActive(true);
        }


        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("shortFrog") && stateInfo.normalizedTime >= 0.99f)//등장후 대기상태 로 전환
        {
            animator.SetTrigger("Idle");
        }


        if (BossManager.instance.BossChk == 2) // true일때 Tallfrog 패턴사용
        {
            animatorTime += Time.deltaTime;

            animator.SetBool("Atk1", true);
            AnimatorStateInfo stateInfoAtk = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("ShortFrogAtk") && stateInfo.normalizedTime <= 0.7f && stateInfo.normalizedTime >= 0.69f)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(punch);
                }
            }
            if (stateInfo.IsName("ShortFrogAtk") && stateInfo.normalizedTime >= 0.99f)
            {
                animator.SetBool("Atk2", true);
                number = Random.Range(0, 2);

                setTime = AtktimeList[number];// 공격시간 랜덤 설정
            }

            if (stateInfo.IsName("ShortFrogAtk2")) // 펀치 공격
            {

                shortFrogAtk();
            }

            if (animatorTime > setTime)
            {
                audioSource.Stop();            
                animatorTime = 0;
                animator.SetBool("Atk3", true);
            }
            if (stateInfo.IsName("ShortFrogAtk3") && stateInfo.normalizedTime >= 0.99f)
            {
                animator.SetBool("Atk1", false);
                animator.SetBool("Atk2", false);
                animator.SetBool("Atk3", false);
                setTime = 500f;
                if (Random.Range(0, 2) == 0)
                {
                    shortFrogCount = 0;
                }
                else
                {
                    shortFrogCount = 2;
                }

                BossManager.instance.AtkChange(20);
            }

        }
    }

    private void shortFrogAtk() // 펀치 공격 로직 
    {
        atkTime += Time.deltaTime;
        if (atkTime >= 0.7f)
        {
            atkTime = 0;
            for (int i = 0; i < shortFrogList.Count; i++)
            {
                if (!shortFrogList[i].activeSelf)
                {

                    if (shortFrogCount >= 3) // 펀치 위치값 이 3초과시  2 로 변경 
                    {
                        shortFrogCount = 2;
                    }
                    else if (shortFrogCount <= -1) // -1보다 작아지면 0으로 변경
                    {
                        shortFrogCount = 0;
                    }

                    shortFrogList[i].transform.position = shortFrogVector[shortFrogCount]; // 펀치 위치 리스트 값을 호출 

                    shortFrogList[i].SetActive(true);
                    break;

                }
            }

            if (shortUpDown == false) // 펀치 순서대로 부르기 위한 로직 
            {
                shortFrogCount++;
            }
            else
            {
                shortFrogCount--;
            }


            if (shortFrogCount >= 2 || shortFrogCount <= 0) // 최대값 최값일때 값 이 + 할지 -할지  변경하는 로직
            {
                if (shortUpDown == false)
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
