using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManger : MonoBehaviour
{
    private AudioSource audio_BGM;
    public AudioClip[] audios;

    GameManager gm;
    private int currentStage;

    // Start is called before the first frame update
    void Start()
    {
        audio_BGM = gameObject.AddComponent<AudioSource>();
        gm = GameObject.FindObjectOfType<GameManager>();
        currentStage = gm.nowStage;

        audio_BGM.loop = true;
        PlayBGM(audios[0]);
    }

    private void Update()
    {
        if(currentStage != gm.nowStage)
        {
            currentStage = gm.nowStage;
            audio_BGM.Stop();
            PlayBGM(audios[currentStage]);
        }
    }

    public void PlayBGM(AudioClip audioClip)
    {
        audio_BGM.clip = audioClip;
        audio_BGM.Play();
    }
}