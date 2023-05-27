using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class event_visible : MonoBehaviour
{
    public int event_id = -1;
    public int trg_id = -1;
    public int event_overnum = 0;
    public int trg_overnum = 0;
    public Image[] color;
    public Text[] text;
    public Button button;
    public GameObject tutorial_ui;
    // Start is called before the first frame update
    void Start()
    {
        if (tutorial_ui != null && event_id != -1 && GManager.instance.EventNumber[event_id] <= event_overnum)
            tutorial_ui.SetActive(true);
        else if (tutorial_ui != null && event_id != -1 && GManager.instance.EventNumber[event_id] > event_overnum)
            tutorial_ui.SetActive(false);
        if (event_id != -1 && GManager.instance.EventNumber[event_id] <= event_overnum)
        {
            ColorChange();
        }
        else if (trg_id != -1 && GManager.instance.Triggers[trg_id] <= trg_overnum)
        {
            ColorChange();
        }
        else if (event_id == -1 && trg_id == -1)
        {
            ColorChange();
        }
    }
    void ColorChange()
    {
        if (color != null)
        {
            for (int i = 0; i < color.Length;)
            {
                var tmpc = color[i].color;
                tmpc.a /= 2;
                color[i].color = tmpc;
                i++;
            }
        }
        if (text != null)
        {
            for (int i = 0; i < text.Length;)
            {
                var tmpc = text[i].color;
                tmpc.a /= 2;
                text[i].color = tmpc;
                i++;
            }
        }
        if (button != null)
            button.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
