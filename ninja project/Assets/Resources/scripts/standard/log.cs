
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class log : MonoBehaviour
{
    Image imagecolor;
    
    public float FadeOutTime = 2.5f;
    
    public string NextScene;

    private float alphacolor = 1.0f;
    
    void Start()
    {
        imagecolor = this.GetComponent<Image>();
    }

    void Update()
    {
        
        FadeOutTime -= Time.deltaTime;
        if(FadeOutTime >0.1f)
        {
            alphacolor -= (FadeOutTime / 600);
            imagecolor.color = new Color(1.0f, 1.0f, 1.0f, alphacolor);
            imagecolor = this.GetComponent<Image>();
        }
        else if(FadeOutTime < 0.1)
        {
            SceneManager.LoadScene(NextScene);
        }

        
    }






}