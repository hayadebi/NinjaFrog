using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textfadeout : MonoBehaviour
{
    public float maxwaittime;
    float waittime;
    [SerializeField]
    public float Rcolor;
    [SerializeField]
    public float Gcolor;
    [SerializeField]
    public float Bcolor;
    [HideInInspector] public bool compFadeIn = false;   //フェードイン完了
    [HideInInspector] public bool compFadeOut = false;  //フェードイン完了

    private Text txt = null;
    private float timer = 0;
    private float wait = 0.5f;
    private bool fadeIn = false;
    private bool fadeOut = false;

    /// <summary>
    /// フェードインを開始する
    /// </summary>
    public void StartFadeIn()
    {
        if (fadeIn || fadeOut)
        {
            return;
        }
        fadeIn = true;
        compFadeIn = false;
        timer = 0.0f;
        txt.color = new Color(Rcolor, Gcolor, Bcolor, 1);
        txt.raycastTarget = true;
    }

    /// <summary>
    /// フェードアウトを開始する
    /// </summary>
    public void StartFadeOut()
    {
        if (fadeIn || fadeOut)
        {
            return;
        }
        fadeOut = true;
        compFadeOut = false;
        timer = 0.0f;
        txt.color = new Color(Rcolor, Gcolor, Bcolor, 0);
        txt.raycastTarget = true;
    }

    void Start()
    {
        txt = GetComponent<Text>();
        StartFadeIn();
    }

    void Update()
    {
        waittime += Time.deltaTime;
        if (waittime > maxwaittime)
        {
            if (fadeIn)
            {
                //フェードイン中
                if (timer < 1 + wait && timer > wait)
                {
                    txt.color = new Color(Rcolor, Gcolor, Bcolor, 1 - timer + wait);
                }
                //フェードイン完了
                else if (timer >= 1 + wait)
                {
                    txt.color = new Color(Rcolor, Gcolor, Bcolor, 0);
                    txt.raycastTarget = false;
                    timer = 0.0f;
                    fadeIn = false;
                    compFadeIn = true;
                }
                timer += Time.deltaTime / 1;
            }
            else if (fadeOut)
            {
                //フェードアウト中
                if (timer < 1)
                {
                    txt.color = new Color(Rcolor, Gcolor, Bcolor, timer);
                }
                //フェードアウト完了
                else if (timer >= 1)
                {
                    txt.color = new Color(Rcolor, Gcolor, Bcolor, 1);
                    txt.raycastTarget = false;
                    timer = 0.0f;
                    fadeOut = false;
                    compFadeOut = true;
                }
                timer += Time.deltaTime / 5;
            }
        }
    }
}