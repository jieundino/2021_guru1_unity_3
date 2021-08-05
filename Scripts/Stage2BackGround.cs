using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2BackGround : MonoBehaviour
{
    //�÷��̾� ���󰡱�
    public Transform target;
    public float speed;
    public float zPosition;

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //�������� 2�� ���� ���󰡵���
        if (gm.nowStage == 1)
        {
            //transform.position = new Vector3(target.position.x, target.position.y, -10f);
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
            transform.position = new Vector3(transform.position.x, transform.position.y, zPosition);
        }
    }
}