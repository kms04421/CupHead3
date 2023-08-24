using UnityEngine;
using UnityEngine.UIElements;

public class AppearBurst2 : MonoBehaviour
{
    // Start is called before the first frame update
    RectTransform rectTransform;
    float AppearTime = 0.0f;
    private Animator animator;
    public GameObject onion;
    public GameObject GroundBack;

    public AudioClip audioClip; // 등장
    private AudioSource audioSource; // 오디오 소스 컴포넌트

    private bool audioChk = false;

    private bool potatoActive = false;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
    
        if (audioChk == false)
        {
            Debug.Log("x");
            audioSource.PlayOneShot(audioClip);

            audioChk =true;

        }
        AppearTime += Time.deltaTime;

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f) // 애니메이션 퍼센트 체크
        {
           
            if (potatoActive == false)
            {
                onion.SetActive(true);
             
                potatoActive = true;
            }
            
            

        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.69f) // 애니메이션 퍼센트 체크
        {


            GroundBack.SetActive(true);

        }

    }
  
}
