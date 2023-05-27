using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DDDTextManager : MonoBehaviour
{
    private TextMesh text;
    int oldis = 0;
    [Multiline]
    public string englishText;
    string jpText;
    int jpFontSize;
    public int englishFontSize;
    public bool nosetTrg = false;
    public Font enfont;
    private Font jpfont;
    void Start()
    {
        text = GetComponent<TextMesh>();
        jpText = text.text;
        jpFontSize = text.fontSize;
        jpfont = text.font;
        if (GManager.instance.isEnglish == 1)
        {
            text.text = englishText;
            if (enfont != null)
                text.font = enfont;
            else
                text.font = jpfont;
            if (englishFontSize != 0)
                text.fontSize = englishFontSize;
            if (nosetTrg)
                this.gameObject.SetActive(false);
        }
        oldis = GManager.instance.isEnglish;
    }
    void Update()
    {
        if (oldis != GManager.instance.isEnglish)
        {
            if (GManager.instance.isEnglish == 1)
            {
                text.text = englishText;

                if (enfont != null)
                    text.font = enfont;
                else
                    text.font = jpfont;
                if (englishFontSize != 0)
                    text.fontSize = englishFontSize;
                if (nosetTrg)
                    this.gameObject.SetActive(false);
            }
            else if (GManager.instance.isEnglish == 0)
            {
                text.text = jpText;
                text.font = jpfont;
                if (englishFontSize != jpFontSize)
                    text.fontSize = jpFontSize;
                if (nosetTrg)
                    this.gameObject.SetActive(true);
            }
            oldis = GManager.instance.isEnglish;
        }
    }
}