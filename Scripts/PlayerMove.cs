using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//만약 떨어지는 속도가 마음에 들지 않으시다면 rigid 부분에서 gravity~ 수를 조정해 주세요
public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public float maxSpeed;
    SpriteRenderer sr;

    //gm
    GameManager manager;
    int currentStage;

    //점프 관련
    public float jumpPower = 10;
    public int maxJump = 1;
    int jumpCount = 0;

    public int maxHp;
    public int currentHp;

    //적 교전 관련
    public float invincibilityTime = 2;

    //NPC 인터랙션 관련
    NPCInteraction npc;
    public Text interText;
    public bool textShowed = false;
    GameObject npcScan;

    //시작 포인트 저장
    public Vector2[] startingPoint;

    //엔딩 관련
    VideoManager vm;

    //**파티클
    public ParticleSystem dust;

    //**플레이어 점프 사운드
    AudioSource audioSrc_jump;

    //**플레이어 맵 아웃 사운드 사전작업
    public AudioClip audio_out;
    //**플레이어 맵 아웃 사운드
    AudioSource audioSource_out;


    //**플레이어 애니메이션(걷기, 점프)
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

        //**플레이어 점프 사운드
        audioSrc_jump = GetComponent<AudioSource>();

        //**플레이어 맵 아웃 사운드
        audioSource_out = GetComponent<AudioSource>();


        //**플레이어 애니메이션(걷기, 점프)
        animator = GetComponent<Animator>();
    }




        // Update is called once per frame
        void Update()
        {
            if (Input.GetButton("Horizontal"))
            {
                rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
            //**플레이어 애니메이션(걷기)
            animator.SetBool("walk", true);
        }

            //방향 전환
            if (Input.GetButton("Horizontal"))
            {

            //sr.flipX = Input.GetAxisRaw("Horizontal") == -1;
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                CreateDust();

                //**플레이어 애니메이션(걷기)
                animator.SetBool("walk", true);

            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                CreateDust();

                //**플레이어 애니메이션(걷기)
                animator.SetBool("walk", true);


            }

            }

            //점프
            if (Input.GetButton("Jump") && jumpCount < maxJump && !manager.isTextOn)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
                jumpCount++;

                //**파티클
                CreateDust();

                //**플레이어 점프 사운드
                audioSrc_jump.Play();

                //**플레이어 애니메이션(점프)
                animator.SetTrigger("jump");
        }

            //텍스트 띄우기
            if (Input.GetKeyDown(KeyCode.E) && npcScan != null)
            {
                manager.NPCText(npcScan);
            }

            //스테이지 전환(starting point 전환)
            if (currentStage != manager.nowStage)
            {
                currentStage = manager.nowStage;
                gameObject.transform.position = startingPoint[currentStage];
            }

            //**플레이어 파티클
            void CreateDust()
            {
                dust.Play();
            }
        }

        private void FixedUpdate()
        {

            //컨트롤에 의한 이동
            float h = manager.isTextOn ? 0 : Input.GetAxisRaw("Horizontal");
            rigid.velocity = new Vector2(h * maxSpeed, rigid.velocity.y);

            //###레이케스트 확인
            //밟은 플렛폼 확인
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

            //InteractiveObject 확인
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

        //아이템과 충돌 시
        private void OnCollisionEnter2D(Collision2D collision)
        {
            GameObject collisionedObject = collision.gameObject;

            //아이템과 충돌 시
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
            //npc와 충돌시(Press Text 표시)
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
            //맵 밖으로 떨어짐
            else if (collisionedObject.CompareTag("StageEndPoint"))
            {
                Attacked(1, collisionedObject.transform.position);
                gameObject.transform.position = startingPoint[manager.nowStage];

                //**플레이어 맵 아웃 사운드
                audioSource_out.PlayOneShot(audio_out, 1);
            }
            //게임 끝나는 지점에 도착
            else if (collisionedObject.CompareTag("GameEndPoint"))
            {
                print("GameEnd");
                vm.PlayVideo();
                manager.GameOver();

            }
        }

        //공격 받음(플레이어 상태 변화)
    public void Attacked(int damage, Vector2 targetPos)
    {
        gameObject.layer = 25;
        sr.color = new Color(1, 1, 1, 0.4f);
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1), ForceMode2D.Impulse);


        //**플레이어 애니메이션(데미지)
        animator.SetTrigger("damaged");


        currentHp -= damage;
        if (currentHp < 0)
        {
            currentHp = 0;

        //**플레이어 애니메이션(데미지)
            animator.SetTrigger("damaged");
        }

        Invoke("offDamaged", invincibilityTime);
        //**플레이어 애니메이션(데미지)
        animator.SetTrigger("damaged");
    }

    void offDamaged()
        {
            gameObject.layer = 6;
            sr.color = new Color(1, 1, 1, 1);
        //**플레이어 애니메이션(데미지)
        animator.SetTrigger("damaged");
    }

        //Press Text 사라짐 관련
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

