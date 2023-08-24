using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
public class Boss2_idel : MonoBehaviour
{
    public static Boss2_idel instance;

    float BossHp = 50f;
    private float AttackTime = 0f;
    private float BossSecondsTime = 0f;
    private Animator animator;
    float[] atkCount;
    int allAtkCount = 0;
    bool cryStart = false;

    private float originalValue = 0f;
    private float minRange = 0;
    private float maxRange = 0;

    private bool DieChk = false;


    private Image imageComponent;
    public AudioClip die;
    public AudioClip cry;

    private AudioSource audioSource;
    public bool Boss2Die = false;

    //보스 피격시 깜박거림 
    private Material originalMaterial; // 원래 마테리얼
    public Material customMaterial; // 적용할 커스텀 마테리얼
    //보스 피격시 깜박거림 end
    // 눈물 
    public GameObject cryL;
    public GameObject cryR;
    private List<GameObject> tearSp;
    private GameObject tearObj;
    public GameObject Tear;
    public GameObject Pink_Tear;
    public GameObject Boss3;
    bool bossDie = false;

    private void Awake()
    {
        if(instance == null)
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
        maxRange = transform.position.y - 7f;
        minRange = transform.position.y;

        imageComponent = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();

        tearSp = new List<GameObject>();
        for (int i = 0; i < 15; i++)
        {
            if (i == 3)
            {
                tearObj = Instantiate(Pink_Tear, transform.position, Quaternion.identity);
            }
            else
            {
                tearObj = Instantiate(Tear, transform.position, Quaternion.identity);

            }
            tearObj.name = "tear" + i;
            tearObj.SetActive(false);
            tearSp.Add(tearObj);
        }
        atkCount = new float[] { 4f, 5f, 7f };
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        VeggieBossManager.instance.BossHpChk((int)BossHp);
        AttackTime += Time.deltaTime;

        if (BossHp <= 0 && bossDie == false)
        {
            audioSource.Stop();
            GameManager_1.instance.BossDieEX(transform);
            Boss2Die = true;
           
            if (cryL.activeSelf)
            {
                cryL.SetActive(false);
                cryR.SetActive(false);
            }

            AttackTime = 0;
            animator.SetTrigger("Die");

            audioSource.PlayOneShot(die);
            bossDie = true;
        }

        if (bossDie && AttackTime >= 2)
        {

            originalValue = transform.position.y;

            transform.Translate(Vector3.down * 6 * Time.deltaTime);

            float normalizedValue = NormalizeValue(originalValue, minRange, maxRange);
            imageComponent.fillAmount = 1 - normalizedValue;



            if (imageComponent.fillAmount <= 0)
            {

                Transform parentTransform = transform.parent;
                Boss3.SetActive(true);
                VeggieBossManager.instance.BossLvAdd();
                parentTransform.gameObject.SetActive(false);
            }

            /////////////////////////////////////////////

          
           
           


        }

        if (AttackTime < 3 && cryStart == false)
        {
            return;
        }
        if (BossHp < 100 && BossHp > 0)
        {
            if (cryStart == false)
            {
                animator.SetTrigger("CryReady");
                cryStart = true;
            }

            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.normalizedTime >= 1f)
            {
                animator.SetBool("CryLV1", true);
            }

            if (stateInfo.IsName("Onion_Atk") && stateInfo.normalizedTime >= 0.6f && stateInfo.normalizedTime <= 0.7f) // 정규화된 시간이 1 이상이면 애니메이션이 종료
            {
                TearPosition();
                cryL.SetActive(true);
                cryR.SetActive(true);
                audioSource.clip = cry;
                audioSource.Play();
            }
            if (stateInfo.IsName("Onion_Atk") && stateInfo.normalizedTime >= 1f) // 정규화된 시간이 1 이상이면 애니메이션이 종료
            {

                animator.SetBool("CryLV2", true);
                AttackTime = 0;

            }
            if (stateInfo.IsName("Onion_Atk2") && atkCount[allAtkCount] < AttackTime) // 정규화된 시간이 1 이상이면 애니메이션이 종료
            {
                cryL.SetActive(false);
                cryR.SetActive(false);
                audioSource.Stop();
                AttackTime = 0;
                tearSp.Reverse();
                animator.SetBool("CryLV3", true);


            }
            if (stateInfo.IsName("Onion_AtkEnd") && stateInfo.normalizedTime >= 1f) // 정규화된 시간이 1 이상이면 애니메이션이 종료
            {

                Debug.Log("4");
                animator.SetBool("CryLV1", false);
                animator.SetBool("CryLV2", false);
                animator.SetBool("CryLV3", false);
                AttackTime = 0;


            }

        }
        //Debug.Log("여기");

    }

    Vector2[] TearSpPosition = new Vector2[] {
        new Vector2(-8.7f, 5),  new Vector2(-7.7f, 5) ,
        new Vector2(-6.7f, 5) , new Vector2(-4.7f, 5) ,
        new Vector2(-3.7f, 5) , new Vector2(-2.7f, 5) ,
        new Vector2(8.7f, 5),  new Vector2(7.7f, 5) ,
        new Vector2(6.7f, 5) , new Vector2(4.7f, 5) ,
        new Vector2(3.7f, 5) , new Vector2(2.7f, 5)
        ,  new Vector2(-7.7f, 5) ,  new Vector2(-7.7f, 5)

    };
    int[] RandYList = new int[] { 10, 12, 15, 20, 23 };
    private void TearPosition()
    {

        int Rand = 0;
        int tearRand = 0;
        int RandY = 0;
        List<int> XList = new List<int>();

        for (int i = 0; i < 6 - 1; i++)
        {
            tearRand = Random.Range(0, 11);
            if (tearRand > 3)
            {
                Rand = Random.Range(0, 10);
            }
            else
            {
                continue;
            }
            if (!XList.Contains(Rand))
            {
                XList.Add(Rand);
            }


        }

        for (int i = 0; i < XList.Count - 1; i++)
        {

            RandY = Random.Range(0, 4);
            Vector2 tearPos = new Vector2(TearSpPosition[XList[i]].x, RandYList[RandY]);
            tearSp[i].transform.position = tearPos;
            tearSp[i].SetActive(true);
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
            StartBlinkEffect();
            BossHp -= 5;
        }

    }

    private float NormalizeValue(float value, float minValue, float maxValue)
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
}
