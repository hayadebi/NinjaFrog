using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UItext : MonoBehaviour
{
    public string InputText = "";
    private Text scoreText = null;
    public Image Picon;
    private int oldInt = -1;
    private float oldFloat = 0;
    private string oldString = "";
    private Sprite oldSprite = null;
    private bool oldbool = true;
    private int oldEnglish = 0;
    public Animator textgetanim;
    private float stime = 0;
    private int newlv = 1;
    public Sprite[] set_icon;
    public GameObject active_obj;
    public bool gm_trg = false;
    private float tmpnum = 0;
    private int selectstagemission = 0;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        if (InputText == "voice")
        {
            if (GManager.instance.isEnglish == 0)
            {
                tmpnum = GManager.instance.live_volume * 100;
                scoreText.text = Mathf.Round(tmpnum).ToString() + "ゲロ";
                oldFloat = GManager.instance.live_volume;
            }
            else if (GManager.instance.isEnglish == 1)
            {
                tmpnum = GManager.instance.live_volume * 100;
                scoreText.text = Mathf.Round(tmpnum).ToString() + "ribbit";
                oldFloat = GManager.instance.live_volume;
            }
        }
        else if (InputText == "run")
        {
            for(int i=0;i<GManager.instance.all_mission.Length;)
            {
                if (GManager.instance.all_mission[i].scene_name == SceneManager.GetActiveScene().name)
                {
                    selectstagemission = i;
                    break;
                }
                i++;
            }
            if (GManager.instance.isEnglish == 0)
            {
                scoreText.text = GManager.instance.run_number.ToString() + "匹の敵蛙から逃走中！";
                oldInt = GManager.instance.run_number;
            }
            else if (GManager.instance.isEnglish == 1)
            {
                scoreText.text = string.Format("It is running away from {0} enemy frogs!", GManager.instance.run_number.ToString());
                oldInt = GManager.instance.run_number;
            }
        }
        else if (InputText == "getcoin")
        {
            scoreText.text = GManager.instance.get_coin.ToString() + "GF";
            oldInt = GManager.instance.get_coin;
        }
        else if (InputText == "scenemission")
        {
            for (int i = 0; i < GManager.instance.StageName.Length;)
            {
                if (SceneManager.GetActiveScene().name == GManager.instance.StageName[i].scene_name)
                {
                    if (GManager.instance.isEnglish == 0)
                        scoreText.text = "任務「" + GManager.instance.all_mission[GManager.instance.StageName[i].target_missionID].jp_missionname + "」開始！";
                    else if (GManager.instance.isEnglish == 0)
                        scoreText.text = "Mission「" + GManager.instance.all_mission[GManager.instance.StageName[i].target_missionID].jp_missionname + "」begins！";
                }
                i++;
            }
        }
        else if (InputText == "mode")
        {
            if (GManager.instance.mode == 0)
            {
                if (GManager.instance.isEnglish == 0)
                {
                    scoreText.text = "難易度：蝿";
                    scoreText.fontSize = 24;
                }
                else
                {
                    scoreText.text = "Difficulty：Fly";
                    scoreText.fontSize = 20;
                }
            }
            else if (GManager.instance.mode == 1)
            {
                if (GManager.instance.isEnglish == 0)
                {
                    scoreText.text = "難易度：蛙";
                    scoreText.fontSize = 24;
                }
                else
                {
                    scoreText.text = "Difficulty：Frog";
                    scoreText.fontSize = 20;
                }
            }
            else if (GManager.instance.mode == 2)
            {
                if (GManager.instance.isEnglish == 0)
                {
                    scoreText.text = "難易度：蛇";
                    scoreText.fontSize = 24;
                }
                else
                {
                    scoreText.text = "Difficulty：Snake";
                    scoreText.fontSize = 20;
                }
            }
            oldInt = GManager.instance.mode;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (oldEnglish != GManager.instance.isEnglish)
        {
            oldEnglish = GManager.instance.isEnglish;
            if (InputText == "voice")
            {
                tmpnum = GManager.instance.live_volume * 100;
                scoreText.text = Mathf.Round(tmpnum).ToString() + "ribbit";
                oldFloat = GManager.instance.live_volume;
            }
            else if (InputText == "run")
            {
                if (GManager.instance.isEnglish == 0)
                {
                    scoreText.text = GManager.instance.run_number.ToString() + "匹の敵蛙から逃走中！";
                    oldInt = GManager.instance.run_number;
                }
                else if (GManager.instance.isEnglish == 1)
                {
                    scoreText.text = string.Format("It is running away from {0} enemy frogs!", GManager.instance.run_number.ToString());
                    oldInt = GManager.instance.run_number;
                }
            }
           
            else if (InputText == "mode")
            {
                if (GManager.instance.mode == 0)
                {
                    if (GManager.instance.isEnglish == 0)
                    {
                        scoreText.text = "難易度：蝿";
                        scoreText.fontSize = 24;
                    }
                    else
                    {
                        scoreText.text = "Difficulty：Fly";
                        scoreText.fontSize = 20;
                    }
                }
                else if (GManager.instance.mode == 1)
                {
                    if (GManager.instance.isEnglish == 0)
                    {
                        scoreText.text = "難易度：蛙";
                        scoreText.fontSize = 24;
                    }
                    else
                    {
                        scoreText.text = "Difficulty：Frog";
                        scoreText.fontSize = 20;
                    }
                }
                else if (GManager.instance.mode == 2)
                {
                    if (GManager.instance.isEnglish == 0)
                    {
                        scoreText.text = "難易度：蛇";
                        scoreText.fontSize = 24;
                    }
                    else
                    {
                        scoreText.text = "Difficulty：Snake";
                        scoreText.fontSize = 20;
                    }
                }
                oldInt = GManager.instance.mode;
            }
        }
        if (InputText == "voice" && oldFloat != GManager.instance.live_volume)
        {
            if (GManager.instance.isEnglish == 0)
            {
                tmpnum = GManager.instance.live_volume * 100;
                scoreText.text = Mathf.Round(tmpnum).ToString() + "ゲロ";
                oldFloat = GManager.instance.live_volume;
            }
            else if (GManager.instance.isEnglish == 1)
            {
                tmpnum = GManager.instance.live_volume * 100;
                scoreText.text = Mathf.Round(tmpnum).ToString() + "ribbit";
                oldFloat = GManager.instance.live_volume;
            }
        }
        else if (InputText == "run" && (GManager.instance.run_number != oldInt || (GManager.instance.Triggers[GManager.instance.all_mission[GManager.instance.select_mission].targettrg_id] >= GManager.instance.all_mission[GManager.instance.select_mission].targettrg_num &&( scoreText.text!="目的は果たした！来た道を戻って離脱しよう！" && scoreText.text != "Let's go back the way we came and get out!"))))
        {
            if (GManager.instance.isEnglish == 0 && GManager.instance.Triggers[GManager.instance.all_mission[GManager.instance.select_mission].targettrg_id] >= GManager.instance.all_mission[GManager.instance.select_mission].targettrg_num)
            {
                scoreText.text = "目的は果たした！来た道を戻って離脱しよう！";
                oldInt = GManager.instance.run_number;
            }
            else if (GManager.instance.isEnglish == 1 && GManager.instance.Triggers[GManager.instance.all_mission[GManager.instance.select_mission].targettrg_id]>= GManager.instance.all_mission[GManager.instance.select_mission].targettrg_num)
            {
                scoreText.text = "Let's go back the way we came and get out!";
                oldInt = GManager.instance.run_number;
            }
            else if (GManager.instance.isEnglish == 0 )
            {
                scoreText.text = GManager.instance.run_number.ToString() + "匹の敵蛙から逃走中！";
                oldInt = GManager.instance.run_number;
            }
            else if (GManager.instance.isEnglish == 1 )
            {
                scoreText.text = string.Format("It is running away from {0} enemy frogs!", GManager.instance.run_number.ToString());
                oldInt = GManager.instance.run_number;
            }
           
        }
        else if (InputText == "mode" && oldInt != GManager.instance.mode)
        {
            if (GManager.instance.mode == 0)
            {
                if (GManager.instance.isEnglish == 0)
                {
                    scoreText.text = "難易度：蝿";
                    scoreText.fontSize = 24;
                }
                else
                {
                    scoreText.text = "Difficulty：Fly";
                    scoreText.fontSize = 20;
                }
            }
            else if (GManager.instance.mode == 1)
            {
                if (GManager.instance.isEnglish == 0)
                {
                    scoreText.text = "難易度：蛙";
                    scoreText.fontSize = 24;
                }
                else
                {
                    scoreText.text = "Difficulty：Frog";
                    scoreText.fontSize = 20;
                }
            }
            else if (GManager.instance.mode == 2)
            {
                if (GManager.instance.isEnglish == 0)
                {
                    scoreText.text = "難易度：蛇";
                    scoreText.fontSize = 24;
                }
                else
                {
                    scoreText.text = "Difficulty：Snake";
                    scoreText.fontSize = 20;
                }
            }
            oldInt = GManager.instance.mode;
        }
        else if (InputText == "getcoin" && oldInt != GManager.instance.get_coin)
        {
            scoreText.text = GManager.instance.get_coin.ToString() + "GF";
            oldInt = GManager.instance.get_coin;
        }
        else if (InputText == "mainhp" && oldInt != GManager.instance.Pstatus.hp )
        {
            scoreText.text = "　　:"+ GManager.instance.Pstatus.hp.ToString ();
            oldInt = GManager.instance.Pstatus.hp;
        }
        else if (InputText == "mainrock" && oldInt != GManager.instance.rock_num)
        {
            scoreText.text = "　　:" + GManager.instance.rock_num.ToString();
            oldInt = GManager.instance.rock_num;
        }
    }
}
