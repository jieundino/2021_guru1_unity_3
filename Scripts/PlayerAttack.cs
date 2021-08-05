using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public GameObject Magic;    //투사체
    public GameObject Doll;     //인형
    public Transform pos;       //생성될 장소
    public float shootSpeed;    //연사 속도
    float curTime;

    GameManager gm;
    MainMenu mm;

    //**플레이어 공격 사운드 사전작업
    public AudioClip audio_attacking;
    //**플레이어 공격 사운드
    AudioSource audioSource_attacking;

    //**토끼 사운드 사전작업
    public AudioClip audio_rabbit;
    //**토끼 사운드
    AudioSource audioSource_rabbit;

    //**플레이어 애니메이션(공격)
    Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        mm = GameObject.FindObjectOfType<MainMenu>();
        gm = GameObject.FindObjectOfType<GameManager>();
        curTime = 0;

        //**플레이어 공격 사운드
        audioSource_attacking = GetComponent<AudioSource>();

        //**토끼 사운드
        audioSource_rabbit = GetComponent<AudioSource>();

        //**플레이어 애니메이션(공격)
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //연사 속도에 맞추어 투사체 발사
        curTime += Time.fixedDeltaTime;
        if (curTime >= shootSpeed)
        {
            if (Input.GetKey(KeyCode.Mouse0) && mm.isGameStart && !gm.isPauseOn && !gm.isDeath)
            {
                //**플레이어 애니메이션(공격)
                animator.SetTrigger("atk");

                Instantiate(Magic, pos.position, transform.rotation);
                curTime = 0;

                //**공격 효과음 
                //audioSource_attacking.clip = audio_attacking;
                //audioSource_attacking.Play();
                audioSource_attacking.PlayOneShot(audio_attacking, 1);

                
            }
        }

        //인형 아이템 사용
        if(Input.GetKeyDown(KeyCode.Alpha2) && gm.Doll>0 && mm.isGameStart && !gm.isPauseOn && !gm.isDeath)
        {
            gm.Doll--;
            Instantiate(Doll, pos.position, transform.rotation);

            //**토끼 사운드
            audioSource_rabbit.PlayOneShot(audio_rabbit, 1);
        }
    }
}
