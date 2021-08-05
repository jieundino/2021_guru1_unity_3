using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    //투사체 속도
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyMagic", 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.y == 0)
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.right * -1 * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DestroyMagic();
        }
    }

    void DestroyMagic()
    {
        Destroy(gameObject);
    }
}
