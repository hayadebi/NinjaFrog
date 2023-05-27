using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GManager : MonoBehaviour
{
    public int get_coin = 0;
    public string get_word;
    public bool title = false;
    public int ui_voice = 0;
    public float voice_volume = 0;
    public float live_volume = 0;
    public static GManager instance = null;
    public bool walktrg = false; //動ける状態か
    public bool stagegame = false;
    public bool saytrg = false;
    public bool ESCtrg = false; //Escを押してるかどうか、または強制的にEscさせるための
    public bool over = false; //ゲームオーバーかどうか
    public int setmenu = 0; //UIの表示状態、0はUIが無い時を示す
    public string txtget; //様々なとこから一時的に格納する文章
    public bool endtitle = false; //いずれ使う
    public int[] EventNumber; //各イベント状態、0はそのイベントが進行していないことを示す
    public float[] freenums; //各々のスクリプトが使う、一時的な数値
    public bool pushtrg = false; //一時的な変数

    public int setrg = -1;
    //設定
    public float audioMax = 0.16f; //音量設定に使用
    public float seMax = 0.16f;//効果音設定に使用
    public int mode = 1; //難易度設定に使用
    public int isEnglish = 0; //言語設定に使用
    public bool dashtrg = false;
    public int run_number = 0;
    [SerializeField, Range(0f, 100f)] public int global_grain = 20;
    public int add_grain = 0;
    public int reduction = 0; //画面効果設定に使用

    [System.Serializable]
    public struct item
    {
        //各アイテム情報
        public string itemname;
        [Multiline]
        public string itemscript;
        public Sprite itemimage;
        public string itemname2;
        [Multiline]
        public string itemscript2;
        public int gettrg;
    }
    public item[] ItemID;
    //------------------------------
    public int itemselect; //現在選択しているアイテム
    [System.Serializable]
    public struct player
    {
        //各プレイヤーの情報
        public Sprite pimage;
        public string pname;
        public string pname2;
        [Multiline]
        public string pscript;
        [Multiline]
        public string pscript2;
        public int maxHP;
        public int hp;
        public float speed;
        public int defense;
        public int attack;
        public int Lv;
        public int maxExp;
        public int inputExp;
        public GameObject pobj;
        public float loadtime;
        public float maxload;
        public int changemode;
        public int getpl;
    }
    public player Pstatus;
    
    public GameObject[] effectobj; //汎用的なエフェクトを格納
    public int animmode = -1; //一時的な、アニメーションを再生するための変数
    public int[] Triggers; //各トリガーの状態。イベントとは違い、この宝箱は一度取ってあるのか、この敵は討伐した奴かどうかなどを格納
    public string SceneText; //一時的なステージ名を指定する用
    public GameObject[] all_ui;
    public AudioClip ase; //一時的な効果音を格納する用
    public string sayobjname; //会話イベントで一時的に使用
    [System.Serializable]
    public struct achievements
    {
        //各実績情報
        [Multiline]
        public string name;
        [Multiline]
        public string name2;
        [Multiline]
        public string script;
        [Multiline]
        public string script2;
        public int gettrg;
        public Sprite image;
    }
    public achievements[] achievementsID;

    public Vector3 mouseP; //現在のマウス位置

    [System.Serializable]
    public struct StageID
    {
        public string scene_name;
        public string jp_name;
        public string en_name;
        [Multiline] public string jp_script;
        [Multiline] public string en_script;
        public int target_missionID;
        public Sprite clear_enemyicon;
    }
    public StageID[] StageName;//new！
    [System.Serializable]
    public struct MissionID
    {
        public string jp_missionname;
        public string en_missionname;
        [Multiline] public string jp_script;
        [Multiline] public string en_script;
        public string outputmission_charanamejp;
        public string outputmission_charanameen;
        public Sprite outputmission_charaimage;
        public int get_missioncoin;
        public int get_itemid;
        public int targettrg_id;
        public int targettrg_num;
        public int select_checktrg;
        public int clear_mission;
        public string scene_name;
        public bool maintrg;
        public string[] next_mission;
        public int open_missionID;
        public string clearevent_scene;
    }
    public MissionID[] all_mission;//new！
    public int select_mission = 0;

    public string storyUI = ""; //章の始終で使用する、一時的な短い文章

    public bool[] colTrg;//一時的な、コライダー取得用
    public AudioClip[] managerSE; //汎用的な効果音を格納

    public float[] instantP;//(一時的な、会話中に位置情報を保存する用)

    public bool empty_player = false;
    public bool event_on = false;

    public int say_eventID = 0;
    public GameObject start_enemy = null;
    public int parent_runtrg = 0;
    public int child_runtrg = 0;
    public float runbgm_starttime = 48f;

    [System.Serializable]
    public struct frogID
    {
        public string jp_name;
        public string en_name;
        [Multiline] public string jp_script;
        [Multiline] public string en_script;
        public int normalhp;
        public int hardhp;
        public int easyhp;
        public string at;
        public string speed;
        public string runtime;
        public string area;
        public string voice;
        public int check_trg;
        public Sprite frog_image;
    }
    public frogID[] all_frog;//new！
    public GameObject coinobj;

    public string daycount = "";
    [System.Serializable]
    public struct ShopItem
    {
        public string[] shopitem_name;
        [Multiline]
        public string[] shopitem_script;
        public Sprite shopitem_image;
        public int shopitem_eventid;
        public int shopitem_lv;
        public int shopitem_maxlv;
        public int shopitem_startprice;
    }
    public ShopItem[] shopitems;
    public int[] day_shopitemID;
    public int[] temp_shopitemID;

    private DateTime TodayNow;
    //購入イベント
    public int rock_num = 0;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Delete ))
        {
            PlayerPrefs.DeleteAll();
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        TodayNow = DateTime.Now;
        instance.daycount=PlayerPrefs.GetString("daycount", "");
        for (int i = 0; i < instance.day_shopitemID.Length;)
        {
            instance.day_shopitemID[i] = PlayerPrefs.GetInt("day_shopitem" + i, 0);
            i++;
        }
        for (int i = 0; i < instance.shopitems.Length;)
        {
            instance.shopitems[i].shopitem_lv = PlayerPrefs.GetInt("shopitems.lv" + i.ToString(), 0);
            i++;
        }
        if (instance.daycount != TodayNow.Year.ToString() + "年 " + TodayNow.Month.ToString() + "月" + TodayNow.Day.ToString() + "日" || CheckShop())
        {
            instance.daycount = TodayNow.Year.ToString() + "年 " + TodayNow.Month.ToString() + "月" + TodayNow.Day.ToString() + "日";
            PlayerPrefs.SetString("daycount", instance.daycount);

            for(int i=0; i < instance.day_shopitemID.Length;)
            {
                bool temp_check = false;
                while (!temp_check)
                {
                    bool for_check = false;
                    int temp_random = UnityEngine.Random.Range(0, instance.shopitems.Length);
                    for (int l = 0; l < instance.temp_shopitemID.Length;)
                    {
                        if (instance.temp_shopitemID[l] == temp_random)
                            for_check = true;
                        l++;
                    }
                    if(!for_check )
                    {
                        instance.day_shopitemID[i] = temp_random;
                        instance.temp_shopitemID[i] = temp_random;
                        temp_check = true;
                    }
                }
                PlayerPrefs.SetInt("day_shopitem" + i, instance.day_shopitemID[i]);
                i++;
            }
        }
    }
    bool CheckShop()
    {
        int tmpnum = -1;
        for (int i = 0; i < instance.day_shopitemID.Length;)
        {
            if (tmpnum != -1 && instance.day_shopitemID[i] == instance.day_shopitemID[tmpnum])
                return true;
            tmpnum = i;
            i++;
        }
        return false;
    }
    public struct SaveEvent
    {
        public void DataSave()
        {
            PlayerPrefs.SetInt("get_coin", instance.get_coin);
            for (int i = 0; i < instance.EventNumber.Length;)
            {
                PlayerPrefs.SetInt("EventNumber" + i.ToString(), instance.EventNumber[i]);
                i++;
            }
            for (int i = 0; i < instance.freenums.Length;)
            {
                PlayerPrefs.SetFloat("freenums" + i.ToString(), instance.freenums[i]);
                i++;
            }
            PlayerPrefs.SetFloat("audioMax", instance.audioMax);
            PlayerPrefs.SetFloat("seMax", instance.seMax);
            PlayerPrefs.SetInt("mode", instance.mode);
            PlayerPrefs.SetInt("reduction", instance.reduction);
            for (int i = 0; i < instance.ItemID.Length;)
            {
                PlayerPrefs.SetInt("ItemID.gettrg" + i.ToString(), instance.ItemID[i].gettrg);
                i++;
            }
            for (int i = 0; i < instance.Triggers.Length;)
            {
                PlayerPrefs.SetInt("Triggers" + i.ToString(), instance.Triggers[i]);
                i++;
            }
            for (int i = 0; i < instance.achievementsID.Length;)
            {
                PlayerPrefs.SetInt("achievementsID.gettrg" + i.ToString(), instance.achievementsID[i].gettrg);
                i++;
            }
            for (int i = 0; i < instance.all_mission.Length;)
            {
                PlayerPrefs.SetInt("all_mission.crear_mission" + i.ToString(), instance.all_mission[i].clear_mission);
                PlayerPrefs.SetInt("all_mission.select_checktrg" + i.ToString(), instance.all_mission[i].select_checktrg);
                i++;
            }
            for (int i = 0; i < instance.all_frog.Length;)
            {
                PlayerPrefs.SetInt("frogID.checktrg" + i.ToString(), instance.all_frog[i].check_trg);
                i++;
            }
            for (int i = 0; i < instance.shopitems.Length;)
            {
                PlayerPrefs.SetInt("shopitems.lv" + i.ToString(), instance.shopitems[i].shopitem_lv);
                i++;
            }

            PlayerPrefs.Save();
        }
        public void DataLoad()
        {
            instance.get_coin = PlayerPrefs.GetInt("get_coin", 0);
            instance.get_word = "";
            instance.title = false;
            instance.walktrg = true;
            instance.stagegame = false;
            instance.saytrg = false;
            instance.ESCtrg = false;
            instance.over = false;
            instance.setmenu = 0;
            instance.txtget = "";
            for (int i = 0; i < instance.EventNumber.Length;)
            {
                instance.EventNumber[i] = PlayerPrefs.GetInt("EventNumber" + i.ToString(), 0);
                i++;
            }
            for (int i = 0; i < instance.freenums.Length;)
            {
                instance.freenums[i] = PlayerPrefs.GetFloat("freenums" + i.ToString(), 0);
                i++;
            }
            instance.pushtrg = false;
            instance.audioMax = PlayerPrefs.GetFloat("audioMax", 0.1f);
            instance.seMax = PlayerPrefs.GetFloat("seMax", 0.08f);
            instance.mode = PlayerPrefs.GetInt("mode", 1);
            instance.dashtrg = false;
            instance.reduction = PlayerPrefs.GetInt("reduction", 0);
            for (int i = 0; i < instance.ItemID.Length;)
            {
                instance.ItemID[i].gettrg = PlayerPrefs.GetInt("ItemID.gettrg" + i.ToString(), 0);
                i++;
            }
            instance.animmode = -1;
            for (int i = 0; i < instance.Triggers.Length;)
            {
                instance.Triggers[i] = PlayerPrefs.GetInt("Triggers" + i.ToString(), 0);
                i++;
            }
            instance.SceneText = "";
            for (int i = 0; i < instance.achievementsID.Length;)
            {
                instance.achievementsID[i].gettrg = PlayerPrefs.GetInt("achievementsID.gettrg" + i.ToString(), 0);
                i++;
            }
            for (int i = 0; i < instance.all_mission.Length;)
            {
                instance.all_mission[i].clear_mission = PlayerPrefs.GetInt("all_mission.crear_mission" + i.ToString(), 0);
                if (0 == i)
                    instance.all_mission[i].select_checktrg = PlayerPrefs.GetInt("all_mission.select_checktrg" + i.ToString(), 1);
                else
                    instance.all_mission[i].select_checktrg = PlayerPrefs.GetInt("all_mission.select_checktrg" + i.ToString(), 0);
                i++;
            }
            for (int i = 0; i < instance.all_frog.Length;)
            {
                if (0 == i)
                    instance.all_frog[i].check_trg = 1;
                else
                    instance.all_frog[i].check_trg = PlayerPrefs.GetInt("frogID.checktrg" + i.ToString(), 0);

                i++;
            }

            for (int i = 0; i < instance.day_shopitemID.Length;)
            {
                instance.day_shopitemID[i] = PlayerPrefs.GetInt("day_shopitem" + i, 0);
                i++;
            }
            for (int i = 0; i < instance.shopitems.Length;)
            {
                instance.shopitems[i].shopitem_lv=PlayerPrefs.GetInt("shopitems.lv" + i.ToString(), 0);
                i++;
            }
            instance.empty_player = false;
            instance.event_on = false;
        }
    }
    public SaveEvent saveevent;
}