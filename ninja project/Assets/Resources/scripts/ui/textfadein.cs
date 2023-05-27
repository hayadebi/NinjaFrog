using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class textfadein : MonoBehaviour
{
    public bool scenetrg = true;
    Text imagecolor;
    public float MaxOutTime;

    private float FadeOutTime = 0f;

    public string NextScene;

    private float alphacolor = 0.0f;

    void Start()
    {
        imagecolor = this.GetComponent<Text>();
    }

    void Update()
    {

        FadeOutTime += Time.deltaTime;
        if (FadeOutTime < MaxOutTime)
        {
            alphacolor = FadeOutTime / 2;
            imagecolor.color = new Color(1.0f, 1.0f, 1.0f, alphacolor);
            imagecolor = this.GetComponent<Text>();
        }
        else if (FadeOutTime > MaxOutTime)
        {
            if (scenetrg == true)
            {
                SceneManager.LoadScene(NextScene);
            }
        }


    }
}
