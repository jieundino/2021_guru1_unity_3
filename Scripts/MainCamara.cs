using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamara : MonoBehaviour
{
    //플레이어 따라가기
    public Transform target;
    public float speed;

    //stage 따라가기
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
        //스테이지에 따른 카메라 범위 설정
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
            //스테이지 3일 때는 x축의 끝이 보스몹에 비래하도록함.
            float clampedX = Mathf.Clamp(this.transform.position.x, boss.transform.position.x + 8 + halfwidth, maxBound.x - halfwidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }

        
        //플레이어 따라서 움직임
        //transform.position = new Vector3(target.position.x, target.position.y, -10f);
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);

    }
}
