using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public bool isGameStart;
    public bool isClickHowToPlay;



    //**클릭 사운드 사전작업
    public AudioClip audio_click;
    //**클릭 사운드
    AudioSource audioSource_click;


    // Start is called before the first frame update
    void Start()
    {
        isGameStart = false;
        isClickHowToPlay = false;

        //**클릭 사운드
        audioSource_click = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStart()
    {
        isGameStart = true;
        print("Game Start");
        //**클릭 사운드
        audioSource_click.PlayOneShot(audio_click, 1);

    }
    public void onClickHowToPlay()
    {
        isClickHowToPlay = true;
        print("How to Play");
        //**클릭 사운드
        audioSource_click.PlayOneShot(audio_click, 1);
    }
    public void onClickExit()
    {
        //**클릭 사운드
        audioSource_click.PlayOneShot(audio_click, 1);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnClickBack()
    {
        isClickHowToPlay = false;
        print("Back");
        //**클릭 사운드
        audioSource_click.PlayOneShot(audio_click, 1);
    }
}
