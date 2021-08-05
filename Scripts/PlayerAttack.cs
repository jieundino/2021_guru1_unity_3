using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public GameObject Magic;    //����ü
    public GameObject Doll;     //����
    public Transform pos;       //������ ���
    public float shootSpeed;    //���� �ӵ�
    float curTime;

    GameManager gm;
    MainMenu mm;

    //**�÷��̾� ���� ���� �����۾�
    public AudioClip audio_attacking;
    //**�÷��̾� ���� ����
    AudioSource audioSource_attacking;

    //**�䳢 ���� �����۾�
    public AudioClip audio_rabbit;
    //**�䳢 ����
    AudioSource audioSource_rabbit;

    //**�÷��̾� �ִϸ��̼�(����)
    Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        mm = GameObject.FindObjectOfType<MainMenu>();
        gm = GameObject.FindObjectOfType<GameManager>();
        curTime = 0;

        //**�÷��̾� ���� ����
        audioSource_attacking = GetComponent<AudioSource>();

        //**�䳢 ����
        audioSource_rabbit = GetComponent<AudioSource>();

        //**�÷��̾� �ִϸ��̼�(����)
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //���� �ӵ��� ���߾� ����ü �߻�
        curTime += Time.fixedDeltaTime;
        if (curTime >= shootSpeed)
        {
            if (Input.GetKey(KeyCode.Mouse0) && mm.isGameStart && !gm.isPauseOn && !gm.isDeath)
            {
                //**�÷��̾� �ִϸ��̼�(����)
                animator.SetTrigger("atk");

                Instantiate(Magic, pos.position, transform.rotation);
                curTime = 0;

                //**���� ȿ���� 
                //audioSource_attacking.clip = audio_attacking;
                //audioSource_attacking.Play();
                audioSource_attacking.PlayOneShot(audio_attacking, 1);

                
            }
        }

        //���� ������ ���
        if(Input.GetKeyDown(KeyCode.Alpha2) && gm.Doll>0 && mm.isGameStart && !gm.isPauseOn && !gm.isDeath)
        {
            gm.Doll--;
            Instantiate(Doll, pos.position, transform.rotation);

            //**�䳢 ����
            audioSource_rabbit.PlayOneShot(audio_rabbit, 1);
        }
    }
}
