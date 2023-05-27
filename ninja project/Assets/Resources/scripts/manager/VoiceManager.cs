using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using UnityEngine.Windows.Speech;
using System.Linq;
public class VoiceManager : MonoBehaviour
{
    public static VoiceManager instance = null;
    private bool isPlay = false;
    AudioSource aud;
    string devName;
    private int count = 0;
    private float count_time = 0;
    int minFreq, maxFreq;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        aud = GetComponent<AudioSource>();
        aud.loop = true;
        if ((aud != null) && (Microphone.devices.Length > 0)) // オーディオソースとマイクがある
        {
            devName = Microphone.devices[0]; // 複数見つかってもとりあえず0番目のマイクを使用
            Microphone.GetDeviceCaps(devName, out minFreq, out maxFreq); // 最大最小サンプリング数を得る
            aud.clip = Microphone.Start(devName, true, 1, minFreq); // 音の大きさを取るだけなので最小サンプリングで十分
            aud.Play(); //マイクをオーディオソースとして実行(Play)開始
        }
        isPlay = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (count_time > 0.000f)
        {
            count_time -= Time.deltaTime;
        }
        else if (isPlay && GManager.instance.walktrg && count_time <= 0.000f)
        {
            count += 1;
            if (count > 99)
            {
                count = 0;
                count_time = 0.01f;
            }
            GetAverageVolume();
        }
    }
    void GetAverageVolume()
    {
        float[] data = new float[256];
        float a = 0;
        aud.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        if (!GManager.instance.empty_player)
        {
            float tmpa = 0f;
            if (a / (GManager.instance.global_grain + (GManager.instance.shopitems[3].shopitem_lv * 4) - (GManager.instance.shopitems[2].shopitem_lv * 4)) > 1)
                tmpa = 1f;
            else
                tmpa = a / (GManager.instance.global_grain + (GManager.instance.shopitems[3].shopitem_lv * 4) - (GManager.instance.shopitems[2].shopitem_lv * 4));
            GManager.instance.voice_volume = tmpa;
            GManager.instance.live_volume = GManager.instance.voice_volume;
        }
        else
        {
            float tmpa = 0f;
            if (a / (GManager.instance.global_grain + (GManager.instance.shopitems[3].shopitem_lv * 4) - (GManager.instance.shopitems[2].shopitem_lv * 4)) > 1)
                tmpa = 1f;
            else
                tmpa = a / (GManager.instance.global_grain + (GManager.instance.shopitems[3].shopitem_lv * 4) - (GManager.instance.shopitems[2].shopitem_lv * 4));
            GManager.instance.voice_volume = 0;
            GManager.instance.live_volume = tmpa;
        }
    }
}