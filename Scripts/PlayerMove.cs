using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//���� �������� �ӵ��� ������ ���� �����ôٸ� rigid �κп��� gravity~ ���� ������ �ּ���
public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public float maxSpeed;
    SpriteRenderer sr;

    //gm
    GameManager manager;
    int currentStage;

    //���� ����
    public float jumpPower = 10;
    public int maxJump = 1;
    int jumpCount = 0;

    public int maxHp;
    public int currentHp;

    //�� ���� ����
    public float invincibilityTime = 2;

    //NPC ���ͷ��� ����
    NPCInteraction npc;
    public Text interText;
    public bool textShowed = false;
    GameObject npcScan;

    //���� ����Ʈ ����
    public Vector2[] startingPoint;

    //���� ����
    VideoManager vm;

    //**��ƼŬ
    public ParticleSystem dust;

    //**�÷��̾� ���� ����
    AudioSource audioSrc_jump;

    //**�÷��̾� �� �ƿ� ���� �����۾�
    public AudioClip audio_out;
    //**�÷��̾� �� �ƿ� ����
    AudioSource audioSource_out;


    //**�÷��̾� �ִϸ��̼�(�ȱ�, ����)
    Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        npc = GetComponent<NPCInteraction>();
        manager = GameObject.FindObjectOfType<GameManager>();
        vm = GameObject.FindObjectOfType<VideoManager>();
        startingPoint[0] = gameObject.transform.position;
        currentStage = manager.nowStage;

        interText.color = new Color(interText.color.r, interText.color.g, interText.color.b, 0f);

        currentHp = maxHp;

        //**�÷��̾� ���� ����
        audioSrc_jump = GetComponent<AudioSource>();

        //**�÷��̾� �� �ƿ� ����
        audioSource_out = GetComponent<AudioSource>();


        //**�÷��̾� �ִϸ��̼�(�ȱ�, ����)
        animator = GetComponent<Animator>();
    }




        // Update is called once per frame
        void Update()
        {
            if (Input.GetButton("Horizontal"))
            {
                rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
            //**�÷��̾� �ִϸ��̼�(�ȱ�)
            animator.SetBool("walk", true);
        }

            //���� ��ȯ
            if (Input.GetButton("Horizontal"))
            {

            //sr.flipX = Input.GetAxisRaw("Horizontal") == -1;
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                CreateDust();

                //**�÷��̾� �ִϸ��̼�(�ȱ�)
                animator.SetBool("walk", true);

            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                CreateDust();

                //**�÷��̾� �ִϸ��̼�(�ȱ�)
                animator.SetBool("walk", true);


            }

            }

            //����
            if (Input.GetButton("Jump") && jumpCount < maxJump && !manager.isTextOn)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
                jumpCount++;

                //**��ƼŬ
                CreateDust();

                //**�÷��̾� ���� ����
                audioSrc_jump.Play();

                //**�÷��̾� �ִϸ��̼�(����)
                animator.SetTrigger("jump");
        }

            //�ؽ�Ʈ ����
            if (Input.GetKeyDown(KeyCode.E) && npcScan != null)
            {
                manager.NPCText(npcScan);
            }

            //�������� ��ȯ(starting point ��ȯ)
            if (currentStage != manager.nowStage)
            {
                currentStage = manager.nowStage;
                gameObject.transform.position = startingPoint[currentStage];
            }

            //**�÷��̾� ��ƼŬ
            void CreateDust()
            {
                dust.Play();
            }
        }

        private void FixedUpdate()
        {

            //��Ʈ�ѿ� ���� �̵�
            float h = manager.isTextOn ? 0 : Input.GetAxisRaw("Horizontal");
            rigid.velocity = new Vector2(h * maxSpeed, rigid.velocity.y);

            //###�����ɽ�Ʈ Ȯ��
            //���� �÷��� Ȯ��
            if (rigid.velocity.y < 0)
            {
                Debug.DrawLine(transform.position, new Vector3(0, 1, 0), new Color(0, 1, 0));
                RaycastHit2D rayHitDown = Physics2D.Raycast(rigid.position, Vector3.down, 3, LayerMask.GetMask("Platform"));

                if (rayHitDown.collider != null)
                {
                    if (rayHitDown.distance < 3)
                    {
                        jumpCount = 0;
                    }
                }
            }

            //InteractiveObject Ȯ��
            Debug.DrawLine(transform.position, new Vector3(1, 0, 0), new Color(1, 0, 0));
            RaycastHit2D rayHitNPC = Physics2D.Raycast(rigid.position, Vector3.right, 1, LayerMask.GetMask("InteractiveObject"));

            if (rayHitNPC.collider != null)
            {
                npcScan = rayHitNPC.collider.gameObject;
            }
            else
            {
                npcScan = null;
            }
        }

        //�����۰� �浹 ��
        private void OnCollisionEnter2D(Collision2D collision)
        {
            GameObject collisionedObject = collision.gameObject;

            //�����۰� �浹 ��
            if (collisionedObject.CompareTag("Item"))
            {
                collisionedObject.SetActive(false);
                switch (collisionedObject.layer)
                {
                    case 18:
                        manager.medicine++;
                        print("medicine");
                        break;
                }
            }
            //npc�� �浹��(Press Text ǥ��)
            else if (collisionedObject.CompareTag("NPC"))
            {
                collisionedObject.SetActive(false);

                if (!textShowed)
                {
                    textShowed = true;
                    interText.color = new Color(interText.color.r, interText.color.g, interText.color.b, 1f);
                    Invoke("startFade", 2);

                }
            }
            //�� ������ ������
            else if (collisionedObject.CompareTag("StageEndPoint"))
            {
                Attacked(1, collisionedObject.transform.position);
                gameObject.transform.position = startingPoint[manager.nowStage];

                //**�÷��̾� �� �ƿ� ����
                audioSource_out.PlayOneShot(audio_out, 1);
            }
            //���� ������ ������ ����
            else if (collisionedObject.CompareTag("GameEndPoint"))
            {
                print("GameEnd");
                vm.PlayVideo();
                manager.GameOver();

            }
        }

        //���� ����(�÷��̾� ���� ��ȭ)
    public void Attacked(int damage, Vector2 targetPos)
    {
        gameObject.layer = 25;
        sr.color = new Color(1, 1, 1, 0.4f);
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1), ForceMode2D.Impulse);


        //**�÷��̾� �ִϸ��̼�(������)
        animator.SetTrigger("damaged");


        currentHp -= damage;
        if (currentHp < 0)
        {
            currentHp = 0;

        //**�÷��̾� �ִϸ��̼�(������)
            animator.SetTrigger("damaged");
        }

        Invoke("offDamaged", invincibilityTime);
        //**�÷��̾� �ִϸ��̼�(������)
        animator.SetTrigger("damaged");
    }

    void offDamaged()
        {
            gameObject.layer = 6;
            sr.color = new Color(1, 1, 1, 1);
        //**�÷��̾� �ִϸ��̼�(������)
        animator.SetTrigger("damaged");
    }

        //Press Text ����� ����
    void startFade()
    {
        StartCoroutine(FadeTextToZero());
    }
    public IEnumerator FadeTextToZero()
    {
        interText.color = new Color(interText.color.r, interText.color.g, interText.color.b, 1);
        while (interText.color.a > 0.0f)
        {
            interText.color = new Color(interText.color.r, interText.color.g, interText.color.b, interText.color.a - (Time.deltaTime / 1));
            yield return null;
        }
    }

}

