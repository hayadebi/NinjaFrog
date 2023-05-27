using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class localLnpc : MonoBehaviour
{
    public bool bgmplay = false;
    public string local = "local";
    public bool setP = false;
    public npcsay input_say = null;
    Flowchart flowChart;
    private npcsay ns = null;
    public GameObject BGM = null;
    public int bgm_index = 1;
    public float _time = 0;
    public int fademode = 0;
    public AudioSource bgmA;
    public sayTrigger sysc;
    // Start is called before the first frame update
    void Start()
    {
        flowChart = this.GetComponent<Flowchart>();
        if (GManager.instance.isEnglish == 1)
        {
            flowChart.SetBooleanVariable(local, true);
        }
        if (bgmplay && this.GetComponent<npcsay>() )
        {
            ns = this.GetComponent<npcsay>();
            
        }
        BGM = GameObject.Find("BGM");
        bgmA = BGM.GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if(flowChart.GetBooleanVariable(local) == true && GManager.instance.isEnglish == 0)
        {
            flowChart.SetBooleanVariable(local, false);
        }
        else if (flowChart.GetBooleanVariable(local) == false && GManager.instance.isEnglish == 1)
        {
            flowChart.SetBooleanVariable(local, true);
        }
        if (bgmplay && flowChart.GetBooleanVariable("bgm")  && BGM.GetComponent<AudioSource>())
        {
            if (fademode == 0 || fademode == 1)
            {
                fademode = -1;
                _time = 0.2f;
                flowChart.SetBooleanVariable("bgm", false);
            }
            else if (fademode == -1)
            {
                fademode = 1;
                _time = 0;
                flowChart.SetBooleanVariable("bgm", false);

                bgmA.loop = false;
                bgmA.Stop();
                if (bgm_index == 1)
                {
                    bgmA.clip = ns.bgm;
                    bgmA.loop = true;
                    bgmA.Play();
                }
                else if (bgm_index == 0)
                {
                    bgmA.clip = ns.eventsound;
                    bgmA.loop = true;
                    bgmA.Play();
                    bgm_index = 1;
                }
            }
        }
        else if (fademode == -1 && bgmA != null )
        {
            _time -= (Time.deltaTime / 15);
            bgmA.volume = _time;
            if (_time <= 0f)
            {
                fademode = 1;
                _time = 0;
                flowChart.SetBooleanVariable("bgm", false);

                bgmA.loop = false;
                bgmA.Stop();
                if (bgm_index == 1)
                {
                    bgmA.clip = ns.bgm;
                    bgmA.loop = true;
                    bgmA.Play();
                }
                else if (bgm_index == 0)
                {
                    bgmA.clip = ns.eventsound;
                    bgmA.loop = true;
                    bgmA.Play();
                    bgm_index = 1;
                }
            }
        }
        else if (fademode == 1 && bgmA != null)
        {
            _time += (Time.deltaTime / 30);
            bgmA.volume = _time;
            if (_time >= 0.2f)
            {
                fademode = 0;
                _time = 0.2f;
            }
        }

        if (setP && flowChart.GetBooleanVariable("setP"))
        {
            flowChart.SetBooleanVariable("setP",false);
            GManager.instance.instantP[0] = flowChart.GetFloatVariable("npcX");
            GManager.instance.instantP[1] = flowChart.GetFloatVariable("npcY");
            GManager.instance.instantP[2] = flowChart.GetFloatVariable("npcZ");
            flowChart.SetFloatVariable("npcX", 0);
            flowChart.SetFloatVariable("npcY", 0);
            flowChart.SetFloatVariable("npcZ", 0);
        }
        if (input_say && flowChart.GetIntegerVariable("input") != 0)
        {
            input_say._inputLocal = flowChart.GetIntegerVariable("input");
            flowChart.SetIntegerVariable("input", 0);
        }

    }
    
}
