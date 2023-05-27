using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextManager : MonoBehaviour
{
    int oldis = 0;
    [Multiline]
    public string englishText;
    string jpText;
    int jpFontSize;
    public int englishFontSize;
    public bool nosetTrg = false;
    public Image textimage = null;
    public Sprite textsprite = null;
    public Font enfont;
    private Font jpfont;
    void Start()
    {
        Text text = GetComponent<Text>();
        jpText = text.text;
        jpFontSize = text.fontSize;
        jpfont = text.font;
        if (GManager.instance.isEnglish == 1 && !textimage)
        {
            text.text = englishText;
            if(enfont != null)
            {
                text.font = enfont;
            }
            {
                text.font = jpfont;
            }
            if (englishFontSize != 0)
            {
                text.fontSize = englishFontSize;
            }
            if(nosetTrg)
            {
                this.gameObject.SetActive(false);
            }
        }
        else if (GManager.instance.isEnglish == 1 && textimage)
        {
            textimage.sprite = textsprite;
            if (nosetTrg)
            {
                this.gameObject.SetActive(false);
            }
        }
        oldis = GManager.instance.isEnglish;
    }
    void Update()
    {
        if(oldis != GManager.instance.isEnglish && !textimage )
        {
            Text text = GetComponent<Text>();
            if (GManager.instance.isEnglish == 1)
            {
                text.text = englishText;
                
                if (enfont != null)
                {
                    text.font = enfont;
                }
                else
                {
                    text.font = jpfont;
                }
                if (englishFontSize != 0)
                {
                    text.fontSize = englishFontSize;
                }
                if (nosetTrg)
                {
                    this.gameObject.SetActive(false);
                }
            }
            else if (GManager.instance.isEnglish == 0)
            {
                text.text = jpText;
                text.font = jpfont;
                if (englishFontSize != jpFontSize)
                {
                    text.fontSize = jpFontSize;
                }
                if (nosetTrg)
                {
                    this.gameObject.SetActive(true);
                }
            }
            oldis = GManager.instance.isEnglish;
        }
    }
}