using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class mission : MonoBehaviour
{
    [Header("メイン依頼かどうか")]
    public bool view_maintrg = true;
    private List<int> view_allmission = new List<int>();
    private bool none_trg = true;
    private int select_page = 0;
    private float cooltime = 0;
    [Header("表示用")]
    public Text mission_name;
    public Text mission_script;
    public Text mission_charatext;
    public Image mission_charaimage;
    public Text get_coin;
    public Text in_stage;
    public Text stage_name;
    private int select_stagenameID;
    [Header("効果音等の演出用")]
    public AudioSource audioSource;
    [Header("0=on,1=no")] public AudioClip[] se;

    // Start is called before the first frame update
    void Start()
    {
        view_allmission = new List<int>();
        if (view_maintrg)
            for (int i = 0;i<GManager.instance.all_mission.Length;)
            {
                if (GManager.instance.all_mission[i].maintrg && GManager.instance.all_mission[i].select_checktrg > 0)
                    view_allmission.Add(i);
                i++;
            }
        else if (!view_maintrg)
            for (int i = 0; i < GManager.instance.all_mission.Length;)
            {
                if (!GManager.instance.all_mission[i].maintrg && GManager.instance.all_mission[i].select_checktrg > 0 )
                    view_allmission.Add(i);
                i++;
            }
        if (view_allmission.Count > 0)
        {
            select_page = 0;
            none_trg = false;
            PageView(0);
        }
    }
    private void Update()
    {
        if (cooltime >= 0f)
            cooltime -= Time.deltaTime;
    }
    public void NextPage()
    {
        if (!none_trg && select_page < view_allmission.Count-1 && cooltime <= 0 )
        {
            cooltime = 0.3f;
            audioSource.PlayOneShot(se[0]);
            select_page += 1;
            PageView(select_page);
        }
        else if(cooltime <= 0)
        {
            cooltime = 0.3f;
            audioSource.PlayOneShot(se[1]);
        }
    }
    public void ReturnPgae()
    {
        if (!none_trg && select_page > 0 && cooltime <= 0)
        {
            cooltime = 0.3f;
            audioSource.PlayOneShot(se[0]);
            select_page -= 1;
            PageView(select_page);
        }
        else if (cooltime <= 0)
        {
            cooltime = 0.3f;
            audioSource.PlayOneShot(se[1]);
        }
    }

    public void StartMission()
    {
        if (!none_trg && cooltime <= 0 && ((GManager.instance.all_mission[select_page].maintrg && GManager.instance.all_mission[select_page].clear_mission < 1)|| !GManager.instance.all_mission[select_page].maintrg ))
        {
            cooltime = 999f;
            GManager.instance.select_mission = select_page;
            audioSource.PlayOneShot(se[0]);
            Instantiate(GManager.instance.all_ui[2], transform.position, transform.rotation);
            Invoke(nameof(MissionScene), 1);
        }
        else if (cooltime <= 0)
        {
            cooltime = 0.3f;
            audioSource.PlayOneShot(se[1]);
            
        }
    }
    void MissionScene()
    {
        GManager.instance.setmenu = 0;
        GManager.instance.walktrg = true;
        GManager.instance.stagegame = true;
        SceneManager.LoadScene(GManager.instance.all_mission[select_page].scene_name);
    }
    private void PageView(int page_missionID = 0)
    {
        for(int i = 0;i<GManager.instance.StageName.Length;)
        {
            if (GManager.instance.StageName[i].scene_name == GManager.instance.all_mission[page_missionID].scene_name)
                select_stagenameID = i;
            i++;
        }
        if (GManager.instance.isEnglish == 0)
        {
            mission_name.text = GManager.instance.all_mission[page_missionID].jp_missionname;
            mission_script.text = GManager.instance.all_mission[page_missionID].jp_script;
            mission_charatext.text = "依頼主："+GManager.instance.all_mission[page_missionID].outputmission_charanamejp;
            get_coin.text = "報酬コイン："+GManager.instance.all_mission[page_missionID].get_missioncoin.ToString()+"GF";
            stage_name.text = "ステージ："+GManager.instance.StageName[select_stagenameID].jp_name;
            if (GManager.instance.all_mission[page_missionID].clear_mission < 1)
                in_stage.text = "任務開始！";
            else
                in_stage.text = "クリア済み";
        }
        else if (GManager.instance.isEnglish != 0)
        {
            mission_name.text = GManager.instance.all_mission[page_missionID].en_missionname;
            mission_script.text = GManager.instance.all_mission[page_missionID].en_script;
            mission_charatext.text = "Client：" + GManager.instance.all_mission[page_missionID].outputmission_charanameen;
            get_coin.text = "Reward Coin：" + GManager.instance.all_mission[page_missionID].get_missioncoin.ToString() + "GF";
            stage_name.text = "Stage：" + GManager.instance.StageName[select_stagenameID].en_name;
            if (GManager.instance.all_mission[page_missionID].clear_mission < 1)
                in_stage.text = "Start！";
            else
                in_stage.text = "Cleared.";
        }
        //get_itemimage.sprite = null;
        mission_charaimage.sprite = GManager.instance.all_mission[page_missionID].outputmission_charaimage;
    }
}
