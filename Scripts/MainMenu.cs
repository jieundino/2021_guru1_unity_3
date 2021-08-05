using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public bool isGameStart;
    public bool isClickHowToPlay;



    //**Ŭ�� ���� �����۾�
    public AudioClip audio_click;
    //**Ŭ�� ����
    AudioSource audioSource_click;


    // Start is called before the first frame update
    void Start()
    {
        isGameStart = false;
        isClickHowToPlay = false;

        //**Ŭ�� ����
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
        //**Ŭ�� ����
        audioSource_click.PlayOneShot(audio_click, 1);

    }
    public void onClickHowToPlay()
    {
        isClickHowToPlay = true;
        print("How to Play");
        //**Ŭ�� ����
        audioSource_click.PlayOneShot(audio_click, 1);
    }
    public void onClickExit()
    {
        //**Ŭ�� ����
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
        //**Ŭ�� ����
        audioSource_click.PlayOneShot(audio_click, 1);
    }
}
