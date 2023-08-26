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
    public GameObject BossAtK1; // ��1
    public GameObject BossAtK2; // ������
    public GameObject Boss2;
    List<GameObject> list;
    int AtkCount = 1;
    float[] AtkCountList;
    float[] AllAtkCountList;
    int AllAtkCount = 0;
    Vector2 vector2;

    public AudioClip Spit; // ����
    public AudioClip Worm; // ������
    public AudioClip Die;

    private Image imageComponent;
    private AudioSource audioSource; // ����� �ҽ� ������Ʈ
    private bool BossDieMove = false;


    //���� �ǰݽ� ���ڰŸ� 
    private Material originalMaterial; // ���� ���׸���
    public Material customMaterial; // ������ Ŀ���� ���׸���
    //���� �ǰݽ� ���ڰŸ� end

    private float originalValue = 0f;
    private float minRange = 0;
    private float maxRange = 0;
    private float BossHp = 50;
    private bool DieChk = false;


    // Start is called before the first frame update
    void Start()
    {
        // ������ ���� ���� ����
       

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
                originalValue = transform.position.y; // ���� ������Ʈ y�� ����

                transform.Translate(Vector3.down * 6 * Time.deltaTime); // ���� ����� �Ʒ��� ����������

                float normalizedValue = NormalizeValue(originalValue, minRange, maxRange); //�������鼭 ������Ʈ ��ġ�� ����Ͽ� Normalizeȭ ���� 0~1������ ǥ��
                imageComponent.fillAmount = 1 - normalizedValue; // ������Ʈ fillAmount �� ����



                if (imageComponent.fillAmount <= 0) // fillAmount �� 0�̸� ���� ���� ��
                {

                    Transform parentTransform = transform.parent;
                    Boss2.SetActive(true);
                    VeggieBossManager.instance.BossLvAdd();
                    parentTransform.gameObject.SetActive(false);
                }
            }



        }

        AttackTime += Time.deltaTime;
        if (BossSecondsTime <= AttackTime) // ������
        {

            animator.SetBool("Attack", true); // ���� �ִϸ��̼� ����
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            animator.speed = AllAtkCountList[AllAtkCount];// ���� ���ݼӵ�
            if (stateInfo.IsName("Boss_Attack") && stateInfo.normalizedTime >= AtkCountList[AtkCount] && stateInfo.normalizedTime <= 1f) // ����ȭ�� �ð��� 1 �̻��̸� �ִϸ��̼��� ����
            {
               
                AttackBurst(); // ��������
                AtkCount++;
            }


            if (stateInfo.IsName("Boss_Attack") && stateInfo.normalizedTime >= 1f) // �ִϸ��̼� ������
            {
                animator.SetBool("Attack", false);// ���� �ִϸ��̼� ��
                animator.speed = 0.8f;
                AtkCount = 0; // ����� ������ ���ݱ��й��
                BossSecondsTime = Random.Range(2f, 4f); // ���� ���� ���� ��������
                AttackTime = 0f; 
                AllAtkCount++;
            }
            if (AllAtkCount == 3) //������ �߻��� �������� ����
            {

                AllAtkCount = 0;
            }


        }


    }


    private void AttackBurst()
    {
        switch (AtkCount)// ���� ���� ī���Ϳ� ���� �߻�
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

    private IEnumerator rollBack(int num) // �������� ����ġ�� 
    {
        yield return new WaitForSeconds(2f);
        list[num].transform.position = vector2;
        list[num].SetActive(false);

    }

    private IEnumerator die() // ���� ����� ������ �������� ȿ��
    {
        audioSource.PlayOneShot(Die);
        animator.SetTrigger("die");
        yield return new WaitForSeconds(1.0f);
       
        BossDieMove = true;


    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �Ѿ˸�
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
    private float NormalizeValue(float value, float minValue, float maxValue) // ���� ������� ȿ���� ���� ��ǥ�� ��꿩 �ڿ���ȭ��Ű�� ����
    {
        float range = maxValue - minValue;
        return (value - minValue) / range;
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
    //��
}
