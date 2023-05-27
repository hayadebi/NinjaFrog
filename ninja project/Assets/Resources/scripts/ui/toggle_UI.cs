using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//UI系のスクリプトを組むときは以下の追記を忘れずに
using UnityEngine.UI;

public class toggle_UI : MonoBehaviour
{
    //Toggle用のフィールド
    public Toggle toggle;
    public string _toggleMode = "";
    public Text _toggleText;
    private void Start()
    {
        if (_toggleMode == "reduction")
        {
            if (GManager.instance.reduction == 1)
                toggle.isOn = true;
            else if (GManager.instance.reduction == 0)
                toggle.isOn = false;
        }
        else if (_toggleMode == "localen")
        {
            if (GManager.instance.isEnglish == 1)
            {
                toggle.isOn = true;
                _toggleText.text = "English";
            }
            else if (GManager.instance.isEnglish == 0)
            {
                toggle.isOn = false;
                _toggleText.text = "日本語";
            }
        }
        else if (_toggleMode == "uivoice")
        {
            if (GManager.instance.ui_voice >= 1)
                toggle.isOn = true;
            else if (GManager.instance.ui_voice <= 0)
                toggle.isOn = false;
        }
    }
    public void OnToggleChanged()
    {
        if(toggle.isOn)
        {
            if (_toggleMode == "localen" && GManager.instance.isEnglish != 1)
            {
                GManager.instance.isEnglish = 1;
                _toggleText.text = "English";
            }
            else if (_toggleMode == "reduction" && GManager.instance.reduction!=1)
            {
                GManager.instance.reduction = 1;
            }
        }
        else if(!toggle.isOn)
        {
            if (_toggleMode == "localen" && GManager.instance.isEnglish != 0)
            {
                GManager.instance.isEnglish = 0;
                _toggleText.text = "日本語";
            }
            else if (_toggleMode == "reduction" && GManager.instance.reduction != 0)
            {
                GManager.instance.reduction = 0;
            }
            else if (_toggleMode == "uivoice" && GManager.instance.ui_voice != 0)
            {
                GManager.instance.ui_voice = 0;
            }
        }
    }
}