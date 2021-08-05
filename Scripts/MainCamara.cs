using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamara : MonoBehaviour
{
    //�÷��̾� ���󰡱�
    public Transform target;
    public float speed;

    //stage ���󰡱�
    public BoxCollider2D[] bound;
    private Vector3 minBound;
    private Vector3 maxBound;
    private float halfwidth;
    private float halfHeight;
    private Camera theCamera;

    //gm
    public GameManager gm;
    public GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        theCamera = GetComponent<Camera>();
        minBound = bound[gm.nowStage].bounds.min;
        maxBound = bound[gm.nowStage].bounds.max;
        halfHeight = theCamera.orthographicSize;
        halfwidth = halfHeight * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        //���������� ���� ī�޶� ���� ����
        minBound = bound[gm.nowStage].bounds.min;
        maxBound = bound[gm.nowStage].bounds.max;

        if (gm.nowStage < 2)
        { 
            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfwidth, maxBound.x - halfwidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
        else
        {
            //�������� 3�� ���� x���� ���� �������� ���ϵ�����.
            float clampedX = Mathf.Clamp(this.transform.position.x, boss.transform.position.x + 8 + halfwidth, maxBound.x - halfwidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }

        
        //�÷��̾� ���� ������
        //transform.position = new Vector3(target.position.x, target.position.y, -10f);
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);

    }
}
