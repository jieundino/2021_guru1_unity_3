using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll : MonoBehaviour
{
    //이동 속도
    public float speed;



    // Start is called before the first frame update
    void Start()
    {
        //생성 후 2->1초 뒤 삭제
        Invoke("DestroyDoll", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어가 보고 있는 방향에 따라 생성 방향 설정
        if (transform.rotation.y == 0)
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, 0);
           
        }
        else
        {
            transform.Translate(transform.right * -1 * speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 180, 0);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //몬스터와 충돌 시 삭제
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
            DestroyDoll();
        }
    }

    void DestroyDoll()
    {
        Destroy(gameObject);
    }
}
