using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public float RunSpeed;  //기본 이동 속도
    public float StunTime;  //스턴될 속도
    public float StunDistance;  //뒤로 밀릴 거리
    bool isStun = false;
    public float speed; //이동 속도(변화됨)
    public int damage;  //플레이어에게 줄 대미지
    PlayerMove player;

    //**보스 사운드 사전작업
    public AudioClip audio_boss;
    //**보스 사운드
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
        //기본 이동 
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        //스턴되었을 때 실행
        if (isStun)
        {
            isStun = false;
            Invoke("BossStun", StunTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //플레이어&인형와 충돌
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Doll"))
        {
            if(collision.gameObject.CompareTag("Player"))   //플레이어와 충돌 시 데미지 줌
                player.Attacked(damage, transform.position);
            transform.position = new Vector3(transform.position.x - StunDistance, transform.position.y, transform.position.z);
            speed = 0;
            isStun = true;
        }
        //그 이외와 충돌
        else
        {
            collision.gameObject.SetActive(false);
        }
    }

    //보스 스턴 풀기
    void BossStun()
    {
        speed = RunSpeed;
    }
}
