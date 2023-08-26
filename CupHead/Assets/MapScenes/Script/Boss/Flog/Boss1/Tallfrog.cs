using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tallfrog : MonoBehaviour
{
    private Animator animator;

    private int allAtkCount = 0;
    private int LastFireFlySpNum = 0;
    private float animatorTime = 0f;
    private float setTime = 1.77f;
    int fireflyX = -8;
    int fireflyY = 0;
    public GameObject firefly;

    bool fireflyMove = false;
    List<GameObject> fireflyList;
    private GameObject SaveObj;

    public GameObject Tallforg2;
    public GameObject Wind;
    private AudioSource audioSource;
    float[] aniList;
    public AudioClip Fanstart; // ��ǳ�� ���� ����
    public AudioClip Fanend; // ��ǳ�� ���� ����

    private Material originalMaterial; // ���� ���׸���
    public Material customMaterial; // ������ Ŀ���� ���׸���
    private SpriteRenderer imageComponent;



    public AudioClip FireFlySpAudio; // ����!
    int BossHp = 100;
    // Start is called before the first frame update
    void Start()
    {
        imageComponent = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        originalMaterial = imageComponent.material;
        aniList = new float[] { 0.55f, 1.5f, 2.3f };

        fireflyList = new List<GameObject>();
        animator = GetComponent<Animator>();
        // ���̾� ���� ����
        for (int i = 0; i < 20; i++)
        {

            SaveObj = Instantiate(firefly, new Vector3(transform.position.x - 3, transform.position.y + 3, 0), Quaternion.identity);
            fireflyList.Add(SaveObj);
            fireflyList[i].SetActive(false);

        }
    }

    // Update is called once per frame
    void Update()
    {


        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("TallFrogAtk") && BossManager.instance.BossLv == 1) // ���� ����� ���ϸ��̼� ����
        {
            animator.SetBool("Die", true);
            audioSource.Stop();
        }


        if (stateInfo.IsName("tallfrog") && stateInfo.normalizedTime >= 0.99f) //����
        {
            animator.SetTrigger("Idle");
        }
        if (BossManager.instance.ready )
        {
            if (stateInfo.IsName("TallFrogFan"))
            {
                if (Wind.activeSelf)
                {
                    Wind.SetActive(false);
                }

            }
            animator.SetBool("Fan", false);
        }
        if (stateInfo.IsName("TallFrog_Idle") && BossManager.instance.ready)
        {

            gameObject.SetActive(false);
            Tallforg2.SetActive(true);
        }


        if (BossManager.instance.BossChk == 1 && BossManager.instance.BossLv == 1) // 2������ 
        {
            if (stateInfo.IsName("TallFrog_Idle") && stateInfo.normalizedTime >= 0.99f)
            {
                animator.SetBool("Fan", true);


            }

            AnimatorStateInfo stateInfoAtk2 = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfoAtk2.IsName("TallFrogFan") && stateInfoAtk2.normalizedTime >= 0.1f && stateInfoAtk2.normalizedTime <= 0.2f)
            {

                Wind.SetActive(true);

            }

            if (stateInfoAtk2.IsName("TallFrogFan") && stateInfoAtk2.normalizedTime >= 0.1f && stateInfoAtk2.normalizedTime <= 0.9f)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(Fanstart);
                }
            }
            if (stateInfoAtk2.IsName("TallFrogFan") && stateInfoAtk2.normalizedTime >= 0.99f)
            {
                audioSource.Stop();
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(Fanend);
                }
                animator.SetBool("Fan", false);

                BossManager.instance.AtkChange(0); // ���� �����
            }
            if (stateInfo.IsName("TallFrogAtk") && BossManager.instance.BossHp <= 0) // ���� ����� ���ϸ��̼� ����
            {
                animator.SetBool("Die", true);
                audioSource.Stop();
            }
        }


        if (BossManager.instance.BossChk == 1 && BossManager.instance.BossLv == 0) // 1������ 
        {


            animatorTime += Time.deltaTime;
            if (animatorTime >= setTime)
            {


                if (allAtkCount < 3)
                {

                    animator.SetBool("Atk", true);

                    AnimatorStateInfo stateInfoAtk = animator.GetCurrentAnimatorStateInfo(0);


                    if (stateInfoAtk.IsName("TallFrogAtk") && stateInfoAtk.normalizedTime >= aniList[allAtkCount])// ���ϸ��̼� �ݺ� �� �ҹ��� ���� ����
                    {

                        fireflyMove = true;
                        animatorTime = 0f;
                        Tallfrog_fireflyAtk();// �ҹ���


                    }



                    if (stateInfoAtk.IsName("TallFrogAtk") && stateInfoAtk.normalizedTime >= 0.999f + allAtkCount)
                    {
                        animator.SetBool("End2", false);
                        animatorTime = 0f;
                        allAtkCount += 1;

                        if (allAtkCount == 2) // �ִϸ��̼� 3ȸ��� �� �ִϸ��̼� ���� �� �ִϸ��̼� ī���� �ʱ�ȭ 
                        {
                            fireflyX = -8;
                            fireflyMove = false;
                            allAtkCount = 0;
                            animator.SetBool("End", true);

                        }

                    }


                    if (stateInfoAtk.IsName("TallFrogAtkEnd") && stateInfoAtk.normalizedTime >= 0.99999999f)
                    {
                        animator.SetBool("End2", true);
                        animator.SetBool("End", false);
                        animator.SetBool("Atk", false);


                        BossManager.instance.AtkChange(20); // ���������
                    }



                }

            }



        }



    }







    private void Tallfrog_fireflyAtk() //�� ���� ����
    {
        for (int i = 0; i < fireflyList.Count; i++)
        {
            if (!fireflyList[i].activeSelf)
            {
                fireflyList[i].transform.position = new Vector3(transform.position.x - 3, transform.position.y + 3, 0);
            }
        }



        StartCoroutine(fireflySp());


    }

    private IEnumerator fireflySp()// �� ���� ����
    {

        if(LastFireFlySpNum >= fireflyList.Count)
        {
            Debug.Log("�̻���");
            LastFireFlySpNum = 0;
        }

        for (int i = LastFireFlySpNum; i < fireflyList.Count; i++)
        {
            if (!fireflyList[i].activeSelf)
            {
                fireflyList[i].transform.position = new Vector3(transform.position.x - 1f, transform.position.y + 2.5f, 0);
                fireflyList[i].SetActive(true);
                StartCoroutine(fireflysMove(i));
                audioSource.PlayOneShot(FireFlySpAudio);
                LastFireFlySpNum = i;
                break;
            }
        }
        yield return new WaitForSeconds(0.3f);

        for (int i = LastFireFlySpNum; i < fireflyList.Count; i++)
        {
            if (!fireflyList[i].activeSelf)
            {
                StartCoroutine(fireflysMove(i));
                fireflyList[i].transform.position = new Vector3(transform.position.x - 1f, transform.position.y + 2.5f, 0);
                fireflyList[i].SetActive(true);
                audioSource.PlayOneShot(FireFlySpAudio);
                LastFireFlySpNum = i;
                break;
            }
        }
    }

    private IEnumerator fireflysMove(int i) // �ҹ��� �����̵� 
    {
        fireflyX += 1;
        if (fireflyX % 2 == 0)
        {
            fireflyY = 4;
        }
        else
        {
            fireflyY = 3;
        }

        
        Vector3 targetPosition = new Vector3(fireflyX + 2, fireflyY, 0f); // ���ϴ� ��ǥ ����
        float moveTime = 3f; // �̵� �ð�
        float elapsedTime = 0f;
        Vector3 startingPosition = fireflyList[i].transform.position;

        while (elapsedTime < moveTime)
        {
            if (fireflyList[i].activeSelf)
            {
                fireflyList[i].transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / moveTime); // ������ġ���� Coroutine�� �ð����� �̵� 


            }
            else
            {
                fireflyList[i].transform.position = new Vector3(transform.position.x - 1f, transform.position.y + 2.5f, 0); //��Ƽ�� ��Ȱ��ȭ�� ���� ������Ʈ ������ġ�� �̵�

            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        


        yield return new WaitForSeconds(1f);
    }


    private void OnTriggerEnter2D(Collider2D collision)// �÷��̾� ���� ����
    {
        if (collision.tag.Equals("PlayerAttack"))
        {
            BossManager.instance.BossHpMinus(0);
            StartBlinkEffect();
        }

        if (collision.tag.Equals("PlayerAttackEx"))//ex���� ���߽�
        {
            BossManager.instance.BossHpMinus(1);
          
            StartBlinkEffect();
        }
    }
    //���ڰŸ� 
    public void StartBlinkEffect()
    {
        // ������ ȿ�� ���� �ڷ�ƾ ȣ��
        StartCoroutine(BlinkEffectCoroutine());
    }

    private IEnumerator BlinkEffectCoroutine()
    {
        // ������ ȿ���� ���� �ӽ� ����
        Material tempColor = customMaterial;

        // ���� ����
        imageComponent.material = tempColor;

        // ���� �ð� ���� ���
        yield return new WaitForSeconds(0.02f);

        // ���� �������� ����
        imageComponent.material = originalMaterial;
    }
}
