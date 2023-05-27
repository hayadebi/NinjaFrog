using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mission_target : MonoBehaviour
{
    public int cure_num = 1;
    public GameObject[] flys;
    public GameObject[] goal_on;
    private AudioSource audioSource;
    [Header("AudioSource必須")]
    public AudioClip se;
    public GameObject clear_effect;
    public int addtrg_id;
    public int target_trgnum;
    public GameObject tutorial_ui=null;
    public int tutorialcheck_eventid;
    public int tutorialcheck_eventnum;
    public enemy run_onenemy = null;
    private bool use_event = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ;
    }
    private void OnTriggerEnter(Collider col)
    {
        if (!GManager.instance.over && GManager.instance.event_on && col.tag == "parea" && !use_event)
        {
            use_event = true;
            GManager.instance.Pstatus.hp += cure_num;
            if (GManager.instance.Pstatus.hp > GManager.instance.Pstatus.maxHP)
                GManager.instance.Pstatus.hp = GManager.instance.Pstatus.maxHP;
            GManager.instance.setrg = 1;
            audioSource.PlayOneShot(se);
            Instantiate(clear_effect, transform.position, transform.rotation);
            for(int i=0;i<flys.Length;)
            {
                flys[i].SetActive(false);
                i++;
            }
            for (int i = 0; i < goal_on.Length;)
            {
                goal_on[i].SetActive(true);
                i++;
            }
            if (GManager.instance.Triggers[addtrg_id] < target_trgnum)
                GManager.instance.Triggers[addtrg_id] += 1;

            if (tutorial_ui != null && GManager.instance.EventNumber[tutorialcheck_eventid] <= tutorialcheck_eventnum)
            {
                GManager.instance.EventNumber[tutorialcheck_eventid] += 1;
                GManager.instance.walktrg = false;
                GManager.instance.setmenu = 1;
                Invoke(nameof(TutorialSet), 0.5f);
            }
            else if (run_onenemy != null && !run_onenemy.enemy_dstrg)
                run_onenemy.AbsoluteRun();
        }
    }
    void TutorialSet()
    {
        Instantiate(tutorial_ui, transform.position, transform.rotation);
        if (run_onenemy != null && !run_onenemy.enemy_dstrg)
        {
            run_onenemy.run_time = 999f;
            run_onenemy.AbsoluteRun();
        }
    }
}
