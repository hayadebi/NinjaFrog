using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mission_princess : MonoBehaviour
{
    public GameObject clear_effect;
    public enemy run_onenemy = null;
    private bool use_event = false;

    public player ps;
    public Transform princess_pos;
    public GameObject goalobj;
    public Renderer[] sprites;
    public BoxCollider bcol;
    public SphereCollider spcol;
    // Start is called before the first frame update
    void Start()
    {
        ;
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
            GManager.instance.setrg = 7;
            ps.get_missiontarget = true;
            goalobj.SetActive(true);
            bcol.enabled = false;
            spcol.enabled = false;
            this.gameObject.tag = "untag";
            GManager.instance.Triggers[GManager.instance.all_mission[GManager.instance.select_mission].targettrg_id] += 1;
            for(int i=0; i < sprites.Length;)
            {
                sprites[i].enabled = false;
                i++;
            }
            this.transform.position = princess_pos.position;
            this.transform.parent = princess_pos;
            Instantiate(clear_effect, transform.position, transform.rotation);
            if (run_onenemy != null && !run_onenemy.enemy_dstrg)
            {
                run_onenemy.run_time = 999f;
                run_onenemy.AbsoluteRun();
            }
        }
    }
}
