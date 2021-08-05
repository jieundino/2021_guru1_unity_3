using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;

    //�̵� ����
    public float speed;
    float currentSpeed;
    SpriteRenderer sp;
    bool flip = false;

    //���� ����
    public int damage;
    PlayerMove player;

    //���� ü��
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
        //�⺻ �̵�
        transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionedObject = collision.gameObject;


        //���� ����ü�� ���� ��
        if (collisionedObject.CompareTag("Attack"))
        {
            if (health == 0)
                gameObject.SetActive(false);
            else
                health--;

            //**���� ������ �� ����
            audioSource_enemy.PlayOneShot(audio_enemy, 1);
        }
        //���� �̵� ������ ���� ����� ��
        else if (collisionedObject.CompareTag("EndPoint"))
        {
            //currentSpeed *= -1;
            //sp.flipX = (speed < 0);
            flip = !flip;
            transform.rotation = flip == false ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
        }

        //�÷��̾�� ������ ��
        else if (collisionedObject.CompareTag("Player"))
        {
            currentSpeed = 0;
            Attack();
        }
        //������ ������ ��
        else if (collisionedObject.CompareTag("Doll"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //�÷��̾�� ���� �� �÷��̾� �˹�
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
