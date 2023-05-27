using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Flowchart))]
public class npcsay : MonoBehaviour
{
    public localLnpc llnpc = null;
    public sayTrigger saySc = null;
    public Transform P = null;
    public float py = 0;
    //public Animator eanim = null;
    public bool audiostop = false;
    public int npctype = 0;
    public float startstoptime = 1f;
    public int gmeventID = 0;
    public int inputNumber;
    public int defaulteventadd = 1;
    public int[] addevent;
    public int[] addEnumber;
    public AudioClip bgm;
    public AudioClip eventsound;
    public int GetCoin;
    public int Trigger = -1;
    public float returnTime = 3;
    public GameObject[] UI;
    public bool nulleventdestroy = false;
    public int nullversion = 1;
    //-----------
    public int EventNumber = -1;
    public string DestroyOBJtext = "";
    public bool nextevent = true;
    public bool sayreturn;
    public string PlayerTag = "player";
    bool saytrg = false;
    public string message = "test";
    bool isTalking = false;
    Flowchart flowChart;
    public string storyText = "";
    public string storyText2 = "";
    public bool UIsummon = false;
    public int allID;

    public int _inputLocal = 0;
    private bool eventstart = false;
    public string target_scene;
    public int select_mission = -1;
    // Start is called before the first frame update
    void Start()
    {
        if (Trigger != -1)
        {
            if (GManager.instance.Triggers[Trigger] == 1)
            {
                Destroy(gameObject);
            }
        }
        flowChart = this.GetComponent<Flowchart>();
        if (nulleventdestroy && inputNumber != GManager.instance.EventNumber[gmeventID] && nullversion == 1)
        {
            Destroy(gameObject);
        }
        else if (nulleventdestroy && inputNumber > GManager.instance.EventNumber[gmeventID] && nullversion == 2)
        {
            Destroy(gameObject);
        }
        else if (nulleventdestroy && inputNumber < GManager.instance.EventNumber[gmeventID] && nullversion == 3)
        {
            Destroy(gameObject);
        }
        llnpc = this.GetComponent<localLnpc>();
        StartCoroutine(nameof(StopEvent));
    }
    private IEnumerator StopEvent() //コルーチン関数の名前
    {
        yield return new WaitForSeconds(startstoptime);
        eventstart = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!saytrg && npctype == 2 && eventstart && (select_mission == -1 ||(　select_mission != -1 && GManager.instance.select_mission == select_mission )))
        {
            saytrg = true;
            StartCoroutine(Talk());
        }
        else if (!saytrg && npctype == 3 && inputNumber == GManager.instance.EventNumber[gmeventID] && GManager.instance.walktrg && eventstart)
        {
            saytrg = true;
            StartCoroutine(Talk());
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == PlayerTag && !saytrg && npctype == 0 && GManager.instance.walktrg)
        {
            if (saySc == null)
            {
                saytrg = true;
                StartCoroutine(Talk());
            }
            else if (saySc != null && !saySc.saystop)
            {
                saytrg = true;
                StartCoroutine(Talk());
            }
        }
        else if (col.tag == PlayerTag && !saytrg && npctype == 1 && inputNumber == GManager.instance.EventNumber[gmeventID] && GManager.instance.walktrg)
        {
            if (saySc == null)
            {
                saytrg = true;
                StartCoroutine(Talk());
            }
            else if (saySc != null && !saySc.saystop)
            {
                saytrg = true;
                StartCoroutine(Talk());
            }
        }
    }

    public IEnumerator Talk()
    {
        GManager.instance.saytrg = true;
        if (saySc != null)
        {
            saySc.saystop = true;
        }
        if (P != null)
        {
            P.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            P.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX| RigidbodyConstraints.FreezeRotationZ;
        }
        GManager.instance.walktrg = false;
        if(P != null)
        {
            var ppos = P.position;
            ppos.y = py+0.1f;
            P.position = ppos;
        }
        if (EventNumber == 1 )
        {
            SaveDate();
            GManager.instance.Pstatus.hp = GManager.instance.Pstatus.maxHP;
        }
        if(bgm != null )
        {
            GameObject BGM = GameObject.Find("BGM");
            AudioSource bgmA = BGM.GetComponent<AudioSource>();
            bgmA.Stop();
        }
        if (isTalking)
        {
            yield break;
        }
        isTalking = true;
        flowChart.SendFungusMessage(message);
        yield return new WaitUntil(() => flowChart.GetExecutingBlocks().Count == 0);
        isTalking = false;
        GManager.instance.saytrg = false;
        GManager.instance.walktrg = true;
        if(GetCoin > 0)
        {
            GManager.instance.get_coin += GetCoin;
        }
        if (P != null)
        {
            P.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            P.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
        if(storyText != "" && UI != null && UI.Length != 0)
        {
            if (GManager.instance.isEnglish == 0)
            {
                GManager.instance.storyUI = storyText;
            }
            else if (GManager.instance.isEnglish == 1)
            {
                GManager.instance.storyUI = storyText2;
            }
            Instantiate(UI[0], transform.position, transform.rotation);
        }
        if (nextevent)
        {
            GManager.instance.EventNumber[gmeventID] += defaulteventadd;
            if(addevent.Length != 0)
            {
                for (int i = 0; i < addevent.Length;)
                {
                    if(addEnumber.Length != 0)
                    {
                        GManager.instance.EventNumber[addevent[i]] = addEnumber[i];
                    }
                    else
                    {
                        GManager.instance.EventNumber[addevent[i]] += defaulteventadd;
                    }
                    i++;
                }
            }
        }
        if (sayreturn)
        {
            Invoke("SayTrg", returnTime);
        }
        if (EventNumber == 2 || UIsummon)
        {
            GManager.instance.walktrg = false;
            GManager.instance.setmenu = 1;
            Instantiate(UI[0], transform.position, transform.rotation);
        }
        
        if (EventNumber == 3)
        {
            if (GManager.instance.isEnglish == 0)
            {
                GManager.instance.txtget = GManager.instance.achievementsID[allID].name + "の実績を解除した！";
            }
            else if (GManager.instance.isEnglish == 1)
            {
                GManager.instance.txtget = "I released the " + GManager.instance.achievementsID[allID].name2 + " achievement！";
            }
            GManager.instance.setrg = 1;
            GManager.instance.achievementsID[allID].gettrg = 1;
        }

        if (bgm != null&& !audiostop && llnpc != null && !llnpc.bgmplay)
        {
            GameObject BGM = GameObject.Find("BGM");
            AudioSource bgmA = BGM.GetComponent<AudioSource>();
            bgmA.Stop();
            bgmA.clip = bgm;
            bgmA.Play();
        }
        if (saySc != null)
        {
            saySc.saystop = false;
        }
        if(EventNumber == 5)
        {
            Instantiate(UI[0], transform.position, transform.rotation);
            Invoke(nameof(SceneChange), 1);
        }

        if (EventNumber == 99)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Application.OpenURL("https://twitter.com/hayadebi");
            Application.Quit();
        }
        else if (DestroyOBJtext != "")
        {
            GameObject obj = GameObject.Find(DestroyOBJtext);
            Destroy(obj.gameObject);
        }
    }
    void SayTrg()
    {
        saytrg = false;
    }
    void SaveDate()
    {
        //後で
    }
    void SceneChange()
    {
        GManager.instance.walktrg = true;
        GManager.instance.setmenu = 0;
        SceneManager.LoadScene(target_scene);
    }
}
