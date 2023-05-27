using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorial_ui : MonoBehaviour
{
    public int event_mode = 0;
    private float time = 0;
    public GameObject[] background_ui;
    public Text button_text;
    public Animator anim;
    public int destroy_menunum = 0;
    public bool start_string = true;
    public string[] next_text = { "閉じる▼", "Close▼" };
    public int destroysetmenu = 0;
    public bool start_stoptrg = false;
    // Start is called before the first frame update
    void Start()
    {
        if(start_stoptrg )
        {
            GManager.instance.walktrg = false;
            GManager.instance.setmenu = 1;
        }
        if (start_string && GManager.instance.isEnglish == 0)
            button_text.text = "次へ→";
        else if (start_string)
            button_text.text = "Next→";
    }

    // Update is called once per frame
    void Update()
    {
        if(time >= 0.0f)
        {
            time -= Time.deltaTime;
        }
    }
    public void ViewUI()
    {
        if (event_mode == 0 && time <= 0)
        {
            event_mode = 1;
            time = 2f;
            background_ui[0].SetActive(false);
            background_ui[1].SetActive(true);
            if (background_ui.Length > 2)
                background_ui[2].SetActive(false);
            GManager.instance.setrg = 3;
        }
    }
    public void NextTutorial()
    {
        if (event_mode == 0 && time <= 0)
        {
            event_mode = 1;
            time = 2f;
            background_ui[0].SetActive(false);
            background_ui[1].SetActive(true);
            if (background_ui.Length > 2)
                background_ui[2].SetActive(false);
            GManager.instance.setrg = 3;
            if (GManager.instance.isEnglish == 0)
                button_text.text = next_text[0];
            else
                button_text.text = next_text[1];
        }
        else if (event_mode == 1 && time <= 0)
        {
            event_mode = 2;
            time = 999;
            GManager.instance.setrg = 3;
            anim.SetInteger("Anumber", 1);
            GManager.instance.saveevent.DataSave();
            Invoke(nameof(EscDestroy), 0.3f);
        }
        else if (event_mode < 2)
        {
            GManager.instance.setrg = 4;
        }
    }

    void EscDestroy()
    {
        GManager.instance.setmenu -= 1;
        GManager.instance.ESCtrg = false;
        if (GManager.instance.setmenu <= destroy_menunum)
            GManager.instance.walktrg = true;
        if (GManager.instance.setmenu < 0)
            GManager.instance.setmenu = 0;
        Destroy(anim.gameObject, 0.1f);
    }
}
