using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class text_fadeloop : MonoBehaviour
{
    public float speed = 1.0f;
    private Text text;
    public Shadow shadow;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        text = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.color = GetAlphaColor(text.color);
        if(shadow != null)
        {
            shadow.effectColor = GetAlphaColor(shadow.effectColor);
        }
    }

    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 5.0f * speed;
        color.a = Mathf.Sin(time);
        if(color.a <= 0.25f)
        {
            color.a = 0.25f;
            time = 0.5f;
        }
        return color;
    }
}
