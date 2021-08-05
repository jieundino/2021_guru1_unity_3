using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll : MonoBehaviour
{
    //�̵� �ӵ�
    public float speed;



    // Start is called before the first frame update
    void Start()
    {
        //���� �� 2->1�� �� ����
        Invoke("DestroyDoll", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //�÷��̾ ���� �ִ� ���⿡ ���� ���� ���� ����
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
        //���Ϳ� �浹 �� ����
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
