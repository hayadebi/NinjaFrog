using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escDestory : MonoBehaviour
{
    public int seTrg = -1;
    public bool mouseesctrg = false;
    [Header("1個前のUIに戻す指定変数(+1される)")] public int inputUInumber = -1;
    public bool mousetrg = false;
    public Animator ui = null;
    public string animname;
    public float destroytime = 0.1f;
    bool inputon = false;
    public Canvas canvas = null;
    // Start is called before the first frame update
    void Start()
    {
        if (mousetrg)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if(canvas != null)
        {
            canvas.worldCamera = Camera.main;
        }
        Invoke("trgOn", 1);
    }
    void trgOn()
    {
        inputon = true;
    }
    void flowerSave()
    {
        GManager.instance.ESCtrg = false;
        PlayerPrefs.SetFloat("audioMax", GManager.instance.audioMax);
        PlayerPrefs.SetFloat("seMax", GManager.instance.seMax);
        PlayerPrefs.Save();
        
        GManager.instance.walktrg = true;
        Destroy(gameObject, destroytime);
        if (GManager.instance.setmenu < 1)
        {
            GManager.instance.setmenu = 0;
            GManager.instance.ESCtrg = false;
            GManager.instance.walktrg = true;
        }
        //-----------------
        if (ui != null)
        {
            ui.Play(animname);
            if (seTrg != -1)
            {
                GManager.instance.setrg = seTrg;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (inputon && inputUInumber == -1)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || GManager.instance.ESCtrg)
            {
                GManager.instance.setmenu = 0;
                flowerSave();
            }
            else if (mouseesctrg)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Return))
                {
                    GManager.instance.setmenu = 0;
                    flowerSave();
                }
            }
        }
        else if(inputon && (inputUInumber + 1) > GManager.instance.setmenu)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || GManager.instance.ESCtrg)
            {
                GManager.instance.setmenu -= 1;
                flowerSave();
                //-----------------
            }
            else if(mouseesctrg)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Return))
                {
                    GManager.instance.setmenu -= 1;
                    flowerSave();
                }
            }
        }
    }
}
