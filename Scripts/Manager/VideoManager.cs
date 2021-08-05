using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public GameObject BadEnding;
    public VideoPlayer BadEnd;
    public GameObject HappyEnding;
    public VideoPlayer HappyEnd;
    public GameObject EndPanel;

    GameObject FinalEnding;
    VideoPlayer finalEnd;

    bool isVideoEnd = false;

    GameManager gm;
    public GameObject inGameUI;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        BadEnding.SetActive(false);
        HappyEnding.SetActive(false);
        EndPanel.SetActive(false);
        inGameUI.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isVideoEnd)
        {
            isVideoEnd = false;
            finalEnd.Stop();
            EndPanel.SetActive(true);
        }
    }
    public void PlayVideo()
    {
        inGameUI.SetActive(false);
        if (gm.unicornHorns == 10)
        {
            FinalEnding = HappyEnding;
            finalEnd = HappyEnd;
        }
        else
        {
            FinalEnding = BadEnding;
            finalEnd = BadEnd;
        }

        FinalEnding.SetActive(true);
        finalEnd.loopPointReached += CheckOver;
    }
    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        print("Video is Over");
        isVideoEnd = true;
    }
}
