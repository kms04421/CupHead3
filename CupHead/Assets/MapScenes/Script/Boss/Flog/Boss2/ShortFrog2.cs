using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShortFrog2 : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider;
    bool startChk = false;
    private float animatorTime = 0f;
    private Rigidbody2D rigidbody2D;
    public GameObject Ball;
    //���� �ǰݽ� ���ڰŸ� 
    
    private Material originalMaterial; // ���� ���׸���
    public Material customMaterial; // ������ Ŀ���� ���׸���
    //���� �ǰݽ� ���ڰŸ� end
    private GameObject saveObj;
    private List<GameObject> BallList;
    int atkCount = 0;
    public AudioClip startAudio; // ���� ����
    public AudioClip loopAudio; // �ݺ� ����
    public AudioClip endAudio; // ���� ����
    //�� ���� ����
    public AudioClip clipAudio; // �ڼ� ����


    private AudioSource audioSource;
    public bool chkball = true;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        BallList = new List<GameObject>();
        for (int i = 0; i < 5; i++)
        {
            saveObj = Instantiate(Ball);
            BallList.Add(saveObj);
            BallList[i].SetActive(false);


        }

        capsuleCollider = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        AnimatorStateInfo stateInfoAtk = animator.GetCurrentAnimatorStateInfo(0);

        if (BossManager.instance.BossHp <= 0 && BossManager.instance.ready)
        {
            animator.SetBool("Start3", true);

            BossManager.instance.BossHp = 10;
            Vector2 newSize = new Vector2(4f, capsuleCollider.size.y / 2f);
            capsuleCollider.size = newSize; //���� �ݶ��̴� ���� ������ ����

        }

        if (BossManager.instance.ready)

        {

            animator.SetBool("BallEnd", true);

        }

        if (stateInfoAtk.IsName("ShortFrog2") && stateInfoAtk.normalizedTime <= 0.1f)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(startAudio);
            }
        }
        if (stateInfoAtk.IsName("ShortFrog2") && stateInfoAtk.normalizedTime >= 0.15f && stateInfoAtk.normalizedTime <= 0.2f)
        {
            audioSource.Stop();
        }
        if (stateInfoAtk.IsName("ShortFrog2") && stateInfoAtk.normalizedTime >= 0.21f && stateInfoAtk.normalizedTime <= 0.4f)
        {

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(loopAudio);
            }
        }

        if (stateInfoAtk.IsName("ShortFrog2") && stateInfoAtk.normalizedTime >= 0.4f && stateInfoAtk.normalizedTime <= 0.85f && gameObject.transform.position.x > -12f)
        {


            if (BossManager.instance.ready)
            {
                transform.Translate(Vector3.right * 10 * Time.deltaTime);


            }
            else
            {
                transform.Translate(Vector3.left * 10 * Time.deltaTime);
            }


        }

        if (stateInfoAtk.IsName("ShortFrog2") && stateInfoAtk.normalizedTime >= 0.85f && gameObject.transform.position.x < -12f && startChk == false)
        {
            audioSource.Stop();
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(endAudio);
            }
            transform.position = new Vector3(transform.position.x + 5, transform.position.y);
            spriteRenderer.flipX = !spriteRenderer.flipX;

            startChk = true;
            animator.SetTrigger("Idle");
            Vector2 newSize = new Vector2(4f, capsuleCollider.size.y * 2f);
            capsuleCollider.size = newSize; //���� �ݶ��̴� ���� ������ ����
        }

        if (BossManager.instance.BossChk == 1 && BossManager.instance.BossLv == 1)
        {


            if (stateInfoAtk.IsName("ShortFrog2_idle") && stateInfoAtk.normalizedTime >= 0.99f)
            {
                audioSource.Stop();
                animator.SetBool("BallEnd", false);
                animator.SetBool("Ball1", true);
            }

            AnimatorStateInfo stateInfoBallAtk = animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfoBallAtk.IsName("ShortFrog2_Ball") && stateInfoBallAtk.normalizedTime >= 0.99f)
            {
                animator.SetBool("Ball1", false);
                animator.SetBool("Ball2", true);

            }
            if (stateInfoBallAtk.IsName("ShortFrog2_Ball") && stateInfoBallAtk.normalizedTime >= 0.8f && stateInfoBallAtk.normalizedTime <= 0.85f)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(clipAudio);
                }
                // ����
                for (int i = 0; i < BallList.Count; i++)
                {
                    if (!BallList[i].activeSelf && chkball == true)
                    {
                        BallList[i].transform.position = new Vector3(transform.position.x + 2, transform.position.y, 0f);
                        BallList[i].SetActive(true);
                        chkball = false;
                        break;
                    }
                }
                // 
            }
            if (stateInfoBallAtk.IsName("ShortFrog2_Ball2") && stateInfoBallAtk.normalizedTime >= 4f)
            {

                chkball = true;
                if (atkCount >= 1)
                {

                    animator.SetBool("Ball3", true);
                }
                else
                {
                    animator.SetBool("Ball2_Atk", true);
                }
            }

            if (stateInfoBallAtk.IsName("ShortFrog2_Ball2_Atk") && stateInfoBallAtk.normalizedTime >= 0.99f)
            {
                animator.SetBool("Ball2_Atk", false);
                atkCount++;


            }
            if (stateInfoBallAtk.IsName("ShortFrog2_Ball2_Atk") && stateInfoBallAtk.normalizedTime >= 0.8f && stateInfoBallAtk.normalizedTime <= 0.85f)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(clipAudio);
                }
                // ����
                for (int i = 0; i < BallList.Count; i++)
                {
                    if (!BallList[i].activeSelf && chkball == true)
                    {
                        BallList[i].transform.position = new Vector3(transform.position.x + 2, transform.position.y, 0f);
                        BallList[i].SetActive(true);
                        chkball = false;
                        break;
                    }
                }

                // 
            }

            if (stateInfoBallAtk.IsName("ShortFrog2_Ball3") && stateInfoBallAtk.normalizedTime >= 0f && stateInfoBallAtk.normalizedTime <= 0.1f)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(clipAudio);
                }
            }

            if (stateInfoBallAtk.IsName("ShortFrog2_Ball3") && stateInfoBallAtk.normalizedTime >= 0.99f)
            {

                atkCount = 0;
                animator.SetBool("Ball2", false);
                animator.SetBool("Ball3", false);
                animator.SetBool("BallEnd", true);
                // ����
                for (int i = 0; i < BallList.Count; i++)
                {
                    if (!BallList[i].activeSelf && chkball == true)
                    {
                        BallList[i].transform.position = new Vector3(transform.position.x + 2, transform.position.y, 0f);
                        BallList[i].SetActive(true);

                        break;
                    }
                }
                // 
                BossManager.instance.AtkChange(1);
            }




        }

    }

    private void OnTriggerEnter2D(Collider2D collision) // �÷��̾� ���� ����
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
        spriteRenderer.material = tempColor;

        // ���� �ð� ���� ���
        yield return new WaitForSeconds(0.02f);

        // ���� �������� ����
        spriteRenderer.material = originalMaterial;
    }
}
