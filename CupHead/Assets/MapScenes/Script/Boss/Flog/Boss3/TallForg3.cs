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
    //��ũ ������Ʈ
    public GameObject pinkobj;

    private GameObject saveObj;
    //���� ���� 
    public GameObject coinSpObj;
    public GameObject coin;
    private List<GameObject> coinList;
    private bool coinAtkStart = false;

    //����
    float[] stopSlotNumList;


    private AudioSource audioSource;

    public AudioClip armParry;
    public AudioClip armdown;
    public AudioClip morphed_attack;
    public AudioClip morphed_spin;
    public AudioClip morphed_dial_spin_loop;
    //���� �ǰݽ� ���ڰŸ� 
    private SpriteRenderer imageComponent;
    private Material originalMaterial; // ���� ���׸���
    public Material customMaterial; // ������ Ŀ���� ���׸���
    //���� �ǰݽ� ���ڰŸ� end
    public GameObject SlotList;
    public GameObject Slot1;
    public GameObject Slot2;
    public GameObject Slot3;
    public GameObject Slot1Atk_Up; // ȣ���� 
    public GameObject Slot1Atk_Down; // ȣ���� 
    public GameObject Slot2Atk; // ��
    public GameObject Slot3Atk; // ������ 
    private List<GameObject> Slot1AtkList; // ȣ���� ���ݸ���Ʈ
    private List<GameObject> Slot2AtkList; // �� ���ݸ���Ʈ
    private List<GameObject> Slot3AtkList; // ������ ���ݸ���Ʈ
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
        for (int i = 0; i < 8; i++) // ȣ���� ���� 
        {

            saveObj = Instantiate(Slot2Atk);
            Slot2AtkList.Add(saveObj);
            Slot2AtkList[i].SetActive(false);
        }// ȣ���� ����  ��

        for (int i = 0; i < 16; i++) // ȣ���� ���� 
        {
            if (i > 8)
            {
                saveObj = Instantiate(Slot1Atk_Up);//���� �ұ��
            }
            else
            {
                saveObj = Instantiate(Slot1Atk_Down);//�Ʒ� �ұ��
            }

            Slot1AtkList.Add(saveObj);
            Slot1AtkList[i].SetActive(false);

        }// �� ����  ��

        for (int i = 0; i < 8; i++) // ������ ���� 
        {
            saveObj = Instantiate(Slot3Atk);
            Slot3AtkList.Add(saveObj);
            Slot3AtkList[i].SetActive(false);
        }// ������ ����  ��





        stopSlotNumList = new float[] { -0.6f, 1.44f, 0.5f, -0.18f }; //ȣ���� �� ������ 

        coinList = new List<GameObject>();
        animator = GetComponent<Animator>();

        for (int i = 0; i < 8; i++)//���� ���� 
        {
            saveObj = Instantiate(coin);
            coinList.Add(saveObj);
            coinList[i].SetActive(false);
        }//���� ����  ��

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

        if (goSlot == true)// ���� �������� ȿ�� , ���� ��ȯ 
        {

            if (armEndChk == false)
            {
                animatorTime = 0;
                animator.SetBool("ArmEnd", true);
                armEndChk = true;
                slotRimeChk = false;
            }
         
            animatorTime += Time.deltaTime;
            if (animatorTime > 3f && slotChk && Slot1.transform.position.y > 1.5f) // �귿 ���� ���� ����� �ð��� ����
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
            }// �귿 ���� ���� ����� �ð��� ���㳡


            if (Slot1.transform.position.y < stopSlotNumList[slotNum1]) { slotChk = true; }// �ش���ġ���� ���� 
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

                slotRimeChk = true; // ������ slotNum3������ �������� ���� 
            }
            else
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(morphed_dial_spin_loop);
                }
                Slot3.transform.Translate(Vector3.down * 8 * Time.deltaTime);
            }// �귿 ���� ��


            if (stateInfo.IsName("Tallfrog_slotman_armEnd") && stateInfo.normalizedTime <= 0.1f) //��ũ���� ���� �ȿø��� 
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(armParry);
                }

            }

            if (stateInfo.IsName("Tallfrog_slotman_armEnd") && stateInfo.normalizedTime >= 0.99f) //��ũ���� ���� �ȿø��� 
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

            if (slotTime >= 1f) //������
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

            if (slotAtkStart == true) // ������ �� ���ݸ�� ��ŸƮ ������ �귿 ���ð��ʿ�
            {
                if (stateInfo.IsName("TallForgSlotAtk1") && stateInfo.normalizedTime >= 0.9f) //����
                {

                    animator.SetBool("Atk2", true);
                }

                if (animatorTime >= SetTime)
                {

                    animatorTime = 0;

                    //slotNum1 ������ ���� ����
                    if (stateInfo.IsName("TallForgSlotAtkLoop") && stateInfo.normalizedTime >= 0.2f) //������ġ�� ����
                    {

                        if (slotNum1 == 2)// ��
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
                                    StartCoroutine(slotAtkMove(Slot1AtkList[i], true));// �̵� ���� �� �Ʒ� ����

                                    break;
                                }

                            }
                        }

                        if (slotNum1 == 1)//ȣ����
                        {

                            for (int i = 0; i < Slot2AtkList.Count; i++)
                            {

                                if (!Slot2AtkList[i].activeSelf)
                                {
                                    Slot2AtkList[i].transform.position = new Vector3(transform.position.x - 1.9f, transform.position.y - 2, 0);
                                    Slot2AtkList[i].SetActive(true);
                                    StartCoroutine(slotAtkMove(Slot2AtkList[i], false));// �̵� ���� �� �Ʒ� ����
                                    break;
                                }
                            }
                        }

                        if (slotNum1 == 3)// ������
                        {
                            for (int i = 0; i < Slot3AtkList.Count; i++)
                            {
                                if (!Slot3AtkList[i].activeSelf)
                                {
                                    Slot3AtkList[i].transform.position = new Vector3(transform.position.x - 1.9f, transform.position.y - 2, 0);
                                    Slot3AtkList[i].SetActive(true);
                                    StartCoroutine(slotAtkMove(Slot3AtkList[i], false));// �̵� ���� �� �Ʒ� ����
                                    break;
                                }

                            }
                        }



                    }//������ġ�� ���� ��



                    if (stateInfo.IsName("TallForgSlotAtkLoop") && stateInfo.normalizedTime >= 20f) //����
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

            if (stateInfo.IsName("TallForgSlotAtkEnd") && stateInfo.normalizedTime >= 0.9f) //����
            {

                goSlot = false; // �������� off
                slotAtkStart = false;// ���� ���� �ð� �ٽ� �ֱ����� off

                coinAtkStart = false;// ���� ���� ��ŸƮ ����  
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


            if (stateInfo.IsName("TallForg3Start") && stateInfo.normalizedTime >= 0.99f) //����
            {
                animator.SetTrigger("Start");
            }

            if (ShortFrog2.transform.position.x > 1)
            {
                animator.SetTrigger("Start2");
                ShortFrog2.SetActive(false);
            }

            if (stateInfo.IsName("TallForg3Start2") && stateInfo.normalizedTime >= 0.9f) //3��ŸƮ
            {

                SlotList.SetActive(true);
                animator.SetTrigger("Idle");
            }

            if (stateInfo.IsName("TallForg3idle") && stateInfo.normalizedTime >= 0.5f) //������
            {

                coinAtkStart = true;

            }

            if (stateInfo.IsName("TallForg3idle") && stateInfo.normalizedTime >= 15f) //������ ��
            {
                animator.SetBool("Arm1", true);

            }

            if (stateInfo.IsName("Tallfrog_slotman_arm") && stateInfo.normalizedTime >= 0.99f) //���Ըӽ� �۵��غ�
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

    private IEnumerator CoinAtk()//���� ���� ����
    {

        coinSpObj.SetActive(true);

        for (int i = 0; i < coinList.Count; i++) // ���ι߻�
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




    Vector3 targetPosition;// �̵� ��ġ �����
    private IEnumerator slotAtkMove(GameObject bossSlotAtkType, bool Up)
    {

        if (Up)
        {
            targetPosition = new Vector3(bossSlotAtkType.transform.position.x - 3, bossSlotAtkType.transform.position.y - 0.5f, 0f); // ���ϴ� ��ǥ ���� (Up)

        }
        else
        {
            targetPosition = new Vector3(bossSlotAtkType.transform.position.x - 3, bossSlotAtkType.transform.position.y - 2, 0f); // ���ϴ� ��ǥ ����(down)

        }
        float moveTime = 1f; // �̵� �ð�
        float elapsedTime = 0f;
        Vector3 startingPosition = bossSlotAtkType.transform.position;

        while (elapsedTime < moveTime)
        {

            bossSlotAtkType.transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / moveTime); //������ġ���� �̵� 
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
            if (collision.tag.Equals("PlayerAttackEx"))//ex���� ���߽�
            {
                BossManager.instance.BossHpMinus(1);
                StartBlinkEffect();
            }
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
