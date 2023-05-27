using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class clickbutton : MonoBehaviour
{
    public AudioClip[] se;
    private AudioSource audioSource;
    public GameObject ui;
    [Header("シーンチェンジ用設定")]
    public string scene_name = "title";
    public bool stagegame_set = false;
    public bool walktrg_set = false;
    public float scenechange_time = 1f;
    public bool say_event = false;
    private int selectstagemission = 0;
    [Header("UI表示用設定")]
    public int static_uinum = -1;
    public int get_uinum = 0;
    private AudioSource bgmobj;
    // Start is called before the first frame update
    void Start()
    {
        if(say_event)
        {
            for (int i = 0; i < GManager.instance.all_mission.Length;)
            {
                if (GManager.instance.all_mission[i].scene_name == SceneManager.GetActiveScene().name)
                {
                    selectstagemission = i;
                    break;
                }
                i++;
            }
            scene_name = GManager.instance.all_mission[selectstagemission].clearevent_scene;
            if (GameObject.Find("BGM") && GameObject.Find("BGM").GetComponent<AudioSource>())
            {
                bgmobj = GameObject.Find("BGM").GetComponent<AudioSource>();
                bgmobj.Stop();
                bgmobj.clip = GManager.instance.managerSE[1];
                bgmobj.Play();
            }

        }
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameQuit()
    {
        GManager.instance.saveevent.DataSave();
        Application.Quit();
    }
    public void SetUI()
    {
        if (GManager.instance.setmenu <= get_uinum)
        {
            if (audioSource != null && se.Length > 0 && se[0] != null)
                audioSource.PlayOneShot(se[0]);
            if (ui != null)
                Instantiate(ui, transform.position, transform.rotation);
            if (static_uinum == -1)
                GManager.instance.setmenu += 1;
            else
                GManager.instance.setmenu = static_uinum;
            GManager.instance.walktrg = false;
        }
    }
    public void DestroyData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Application.Quit();
    }
    public void DifficultyChange()
    {
        if (audioSource != null && se.Length > 0 && se[0] != null)
            audioSource.PlayOneShot(se[0]);
        if (GManager.instance.mode == 0)
            GManager.instance.mode = 1;
        else if (GManager.instance.mode == 1)
            GManager.instance.mode = 2;
        else if (GManager.instance.mode == 2)
            GManager.instance.mode = 0;
    }
    public void SceneChange()
    {
        if (audioSource != null && se.Length > 0 && se[0] != null)
            audioSource.PlayOneShot(se[0]);
        if (ui != null)
            Instantiate(ui, transform.position, transform.rotation);
        if(SceneManager.GetActiveScene().name != "load")
            GManager.instance.saveevent.DataSave();
        Invoke(nameof(InvokeScene), scenechange_time);
    }
    private void InvokeScene()
    {
        if (say_event)
        {
            GManager.instance.say_eventID = selectstagemission;
            GManager.instance.Triggers[GManager.instance.all_mission[selectstagemission].targettrg_id] = 0;
            if(GManager.instance.all_mission[selectstagemission].clear_mission < 1)
                GManager.instance.get_coin += GManager.instance.all_mission[selectstagemission].get_missioncoin;
            if(GManager.instance.all_mission[selectstagemission].get_itemid != -1)
                GManager.instance.ItemID[GManager.instance.all_mission[selectstagemission].get_itemid].gettrg += 1;
            GManager.instance.all_mission[selectstagemission].clear_mission += 1;
            if(GManager.instance.all_mission[selectstagemission].open_missionID != -1)
                GManager.instance.all_mission[GManager.instance.all_mission[selectstagemission].open_missionID].select_checktrg = 1;
        }
        GManager.instance.over = false;
        for(int i=0;i<GManager.instance.all_mission.Length;)
        {
            if (SceneManager.GetActiveScene().name == GManager.instance.all_mission[i].scene_name)
            {
                GManager.instance.Triggers[GManager.instance.all_mission[i].targettrg_id] = 0;
                break;
            }
            i++;
        }
        GManager.instance.Pstatus.hp = GManager.instance.Pstatus.maxHP;
        if (!say_event)
        {
            GManager.instance.setmenu = 0;
            GManager.instance.walktrg = walktrg_set;
        }
        GManager.instance.stagegame = stagegame_set;
        if (scene_name == "-1")
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else
            SceneManager.LoadScene(scene_name);
    }
}
