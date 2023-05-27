using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameclearUI : MonoBehaviour
{
    public Text cleartitletext;
    public Text clearrewardtext;
    public Image enemyfrog;
    private int stage_num = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < GManager.instance.StageName.Length;)
        {
            if (GManager.instance.StageName[i].scene_name == GManager.instance.all_mission[GManager.instance.select_mission].scene_name)
            {
                stage_num = i;
                break;
            }
            i++;
        }
        enemyfrog.sprite = GManager.instance.StageName[stage_num].clear_enemyicon;
        if (GManager.instance.isEnglish == 0)
        {
            cleartitletext.fontSize = 28;
            cleartitletext.text = "「" + GManager.instance.all_mission[GManager.instance.select_mission].jp_missionname + "」\n任務達成！";
            clearrewardtext.fontSize = 32;
            if(GManager.instance.all_mission[GManager.instance.select_mission].open_missionID != -1)
                clearrewardtext.text = "獲得ゴールドフロッグ：" + GManager.instance.all_mission[GManager.instance.select_mission].get_missioncoin.ToString() + "GF\n\n\n次の依頼：「"+GManager.instance.all_mission[GManager.instance.all_mission[GManager.instance.select_mission].open_missionID].jp_missionname+"」";
            else if(GManager.instance.all_mission[GManager.instance.select_mission].clear_mission > 0)
                clearrewardtext.text = "獲得ゴールドフロッグ：ーーー" + "GF\n\n\n次の依頼：「ーーーー」";
            else
                clearrewardtext.text = "獲得ゴールドフロッグ：" + GManager.instance.all_mission[GManager.instance.select_mission].get_missioncoin.ToString() + "GF\n\n\n次の依頼：「ーーーー」";
        }
        else
        {
            cleartitletext.fontSize = 24;
            cleartitletext.text = "「" + GManager.instance.all_mission[GManager.instance.select_mission].en_missionname + "」\nMission accomplished!";
            clearrewardtext.fontSize = 28;
            if (GManager.instance.all_mission[GManager.instance.select_mission].open_missionID  != -1)
                clearrewardtext.text = "Reward Gold：" + GManager.instance.all_mission[GManager.instance.select_mission].get_missioncoin.ToString() + "GF\n\n\nNext mission：「" + GManager.instance.all_mission[GManager.instance.all_mission[GManager.instance.select_mission].open_missionID].en_missionname + "」";
            else if (GManager.instance.all_mission[GManager.instance.select_mission].clear_mission > 0)
                clearrewardtext.text = "Reward Gold：ーーー" + "GF\n\n\nNext mission：「ーーーー」";
            else
                clearrewardtext.text = "Reward Gold：" + GManager.instance.all_mission[GManager.instance.select_mission].get_missioncoin.ToString() + "GF\n\n\nNext mission：「ーーーー」";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
