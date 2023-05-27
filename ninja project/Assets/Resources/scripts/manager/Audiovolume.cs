using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiovolume : MonoBehaviour
{
    public bool battletrg = false;
    private bool isadd = false;
    float oldvolume;
    public bool setrg = false;
    public bool voicetrg = false;
    void Start()
    {
        //アタッチされているAudioSource取得
        AudioSource audio = GetComponent<AudioSource>();
        if(GManager.instance.over)
        {
            audio.volume = GManager.instance.audioMax / 12;
            oldvolume = GManager.instance.audioMax / 12;
        }
        else if (!setrg)
        {
            audio.volume = GManager.instance.audioMax / 4;
            oldvolume = GManager.instance.audioMax / 4;
        }
        else if (setrg)
        {
            if (voicetrg)
            {
                audio.volume = 0.01f+GManager.instance.live_volume / 2;
                oldvolume = 0.01f + GManager.instance.live_volume / 2;
            }
            else
            {
                audio.volume = GManager.instance.seMax;
                oldvolume = GManager.instance.seMax;
            }
        }
    }
    // Update is called once per frame
    private void Update()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (GManager.instance.over || !GManager.instance.walktrg)
        {
            if (oldvolume != GManager.instance.audioMax / 16 && !setrg && !GManager.instance.over)
            {
                audio.volume = GManager.instance.audioMax / 16;
                oldvolume = GManager.instance.audioMax / 16;
            }
            else if (oldvolume != 0 && !setrg && GManager.instance.over)
            {
                audio.volume = 0;
                oldvolume = 0;
            }
        }
        else if (!setrg && oldvolume != GManager.instance.audioMax / 4)
        {
            audio.volume = GManager.instance.audioMax / 4;
            oldvolume = GManager.instance.audioMax / 4;
        }
        else if (setrg && oldvolume != GManager.instance.seMax)
        {
            if (voicetrg)
            {
                audio.volume = 0.01f + GManager.instance.live_volume / 2;
                oldvolume = 0.01f + GManager.instance.live_volume / 2;
            }
            else
            {
                audio.volume = GManager.instance.seMax;
                oldvolume = GManager.instance.seMax;
            }
        }
        //オンオフ
        if (!battletrg && !GManager.instance.walktrg && !isadd)
        {
            audio.enabled = false;
            isadd = true;
        }
        else if (!battletrg && GManager.instance.walktrg && isadd)
        {
            audio.enabled = true;
            isadd = false;
        }
    }
}
