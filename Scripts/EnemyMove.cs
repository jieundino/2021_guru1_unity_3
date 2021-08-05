using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;

    //이동 관련
    public float speed;
    float currentSpeed;
    SpriteRenderer sp;
    bool flip = false;

    //공격 관련
    public int damage;
    PlayerMove player;

    //적의 체력
    public int health;


    public AudioClip audio_enemy;
    AudioSource audioSource_enemy;


    void Start()
    {
        rigid = GameObject.FindObjectOfType<Rigidbody2D>();
        player = GameObject.FindObjectOfType<PlayerMove>();
        sp = GameObject.FindObjectOfType<SpriteRenderer>();
        currentSpeed = speed;
        audioSource_enemy = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //기본 이동
        transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionedObject = collision.gameObject;


        //공격 투사체와 만날 시
        if (collisionedObject.CompareTag("Attack"))
        {
            if (health == 0)
                gameObject.SetActive(false);
            else
                health--;

            //**적과 만났을 때 사운드
            audioSource_enemy.PlayOneShot(audio_enemy, 1);
        }
        //몬스터 이동 범위의 끝에 닿았을 시
        else if (collisionedObject.CompareTag("EndPoint"))
        {
            //currentSpeed *= -1;
            //sp.flipX = (speed < 0);
            flip = !flip;
            transform.rotation = flip == false ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
        }

        //플레이어와 만났을 시
        else if (collisionedObject.CompareTag("Player"))
        {
            currentSpeed = 0;
            Attack();
        }
        //인형과 만났을 시
        else if (collisionedObject.CompareTag("Doll"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //플레이어와 만난 후 플레이어 넉백
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("Move", 1);
        }
    }

    void Attack()
    {
        player.Attacked(damage, transform.position);
    }

    void Move()
    {
        currentSpeed = speed;
    }
}
