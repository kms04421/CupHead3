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
    //�������� 1 ���� 
    public GameObject shortFrog;// ��ġ������ ����
    public GameObject shortFrog_P;// ��ũ ��ġ������ ���� 
    Vector2[] shortFrogVector; // ��ġ ���ư���ġ ����
    int shortFrogCount = 0; // ��ġ ī���� 
    bool shortUpDown = false;
    private Rigidbody2D rigidbody2D;
    public GameObject Boss2;

    public AudioClip punch; // ��ġ ����
    public AudioClip Ko2; // ����!
    //���� �ǰݽ� ���ڰŸ� 
    private SpriteRenderer imageComponent;
    private Material originalMaterial; // ���� ���׸���
    public Material customMaterial; // ������ Ŀ���� ���׸���
    //���� �ǰݽ� ���ڰŸ� end
    private AudioSource audioSource;
    List<GameObject> shortFrogList;// ��ġ ����Ʈ 
    float atkTime = 0f;
    int[] AtktimeList = new int[]
    {
        5,7
    };
    int number = 0;
    //��������1 end


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
            new Vector2(transform.position.x -3.5f,transform.position.y-2f),//�ϴ�
            new Vector2(transform.position.x -3.5f,transform.position.y-0.8f),//�߰�
            new Vector2(transform.position.x -3.5f,transform.position.y+1f)//���
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
        if (stateInfo.IsName("shortFrog") && stateInfo.normalizedTime >= 0.99f)//������ ������ �� ��ȯ
        {
            animator.SetTrigger("Idle");
        }


        if (BossManager.instance.BossChk == 2) // true�϶� Tallfrog ���ϻ��
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

                setTime = AtktimeList[number];// ���ݽð� ���� ����
            }

            if (stateInfo.IsName("ShortFrogAtk2")) // ��ġ ����
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

    private void shortFrogAtk() // ��ġ ���� ���� 
    {
        atkTime += Time.deltaTime;
        if (atkTime >= 0.7f)
        {
            atkTime = 0;
            for (int i = 0; i < shortFrogList.Count; i++)
            {
                if (!shortFrogList[i].activeSelf)
                {

                    if (shortFrogCount >= 3) // ��ġ ��ġ�� �� 3�ʰ���  2 �� ���� 
                    {
                        shortFrogCount = 2;
                    }
                    else if (shortFrogCount <= -1) // -1���� �۾����� 0���� ����
                    {
                        shortFrogCount = 0;
                    }

                    shortFrogList[i].transform.position = shortFrogVector[shortFrogCount]; // ��ġ ��ġ ����Ʈ ���� ȣ�� 

                    shortFrogList[i].SetActive(true);
                    break;

                }
            }

            if (shortUpDown == false) // ��ġ ������� �θ��� ���� ���� 
            {
                shortFrogCount++;
            }
            else
            {
                shortFrogCount--;
            }


            if (shortFrogCount >= 2 || shortFrogCount <= 0) // �ִ밪 �ְ��϶� �� �� + ���� -����  �����ϴ� ����
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
