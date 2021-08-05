using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_Sfx : MonoBehaviour
{

    static AudioSource audioSource_boss;
    public static AudioClip audioclip_boss;

    // Start is called before the first frame update
    void Start()
    {
        audioSource_boss = GetComponent<AudioSource>();
        audioclip_boss = Resources.Load<AudioClip>("BOSS");
    }

    // Update is called once per frame
    public static void Boss_SoundPlay()
    {
        audioSource_boss.PlayOneShot(audioclip_boss);
    }
}
