using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public float RunSpeed;  //�⺻ �̵� �ӵ�
    public float StunTime;  //���ϵ� �ӵ�
    public float StunDistance;  //�ڷ� �и� �Ÿ�
    bool isStun = false;
    public float speed; //�̵� �ӵ�(��ȭ��)
    public int damage;  //�÷��̾�� �� �����
    PlayerMove player;

    //**���� ���� �����۾�
    public AudioClip audio_boss;
    //**���� ����
    AudioSource audioSource_boss;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GameObject.FindObjectOfType<Rigidbody2D>();
        player = GameObject.FindObjectOfType<PlayerMove>();
        speed = 0;

    }

    // Update is called once per frame
    void Update()
    {
        //�⺻ �̵� 
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        //���ϵǾ��� �� ����
        if (isStun)
        {
            isStun = false;
            Invoke("BossStun", StunTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�÷��̾�&������ �浹
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Doll"))
        {
            if(collision.gameObject.CompareTag("Player"))   //�÷��̾�� �浹 �� ������ ��
                player.Attacked(damage, transform.position);
            transform.position = new Vector3(transform.position.x - StunDistance, transform.position.y, transform.position.z);
            speed = 0;
            isStun = true;
        }
        //�� �̿ܿ� �浹
        else
        {
            collision.gameObject.SetActive(false);
        }
    }

    //���� ���� Ǯ��
    void BossStun()
    {
        speed = RunSpeed;
    }
}
