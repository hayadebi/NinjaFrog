using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class rollsay : MonoBehaviour
{
    [System.Serializable]
    public struct SayEvent
    {
        public Text text_mesh;
        public Animator text_anim;
        [Multiline]
        public string[] say_textjp;
        [Multiline]
        public string[] say_texten;
        public float[] start_lag;
        public float[] view_time;
    }
    [Header("会話内容※単体蛙可")]
    public SayEvent[] say_event;
    private bool view_trg = false;
    private int say_viewid = 0;
    private int view_mode = 0;
    private float check_time = 0;
    public AudioSource audioSource = null;
    public AudioClip se = null;
    public enemy es = null;
    private int end_say = 0;
    // Start is called before the first frame update
    void Start()
    {
        ViewEnd();
    }

    // Update is called once per frame
    void Update()
    {
        if (es.enemy_mode == 3 && end_say < 3)
            end_say = 2;
        if (view_trg && GManager.instance.parent_runtrg < 1 && GManager.instance.child_runtrg < 1 && GManager.instance.walktrg && view_mode == 0 && !es.enemy_dstrg && es.auto_moveXtime == 0 && es.auto_moveZtime== 0 && end_say < 3)
        {
            check_time += Time.deltaTime;
            if (check_time >= say_event[0].start_lag[say_viewid])
            {
                check_time = 0;
                view_mode = 1;
            }
        }
        if (view_trg && GManager.instance.parent_runtrg < 1 && GManager.instance.child_runtrg < 1 && GManager.instance.walktrg && view_mode == 1 && !es.enemy_dstrg && es.auto_moveXtime == 0 && es.auto_moveZtime == 0 && end_say < 3)
        {
            if (say_event[0].text_anim.GetInteger("Anumber") != 0)
            {
                say_event[0].text_anim.SetInteger("Anumber", 0);
                ViewStart(1, 0);
            }
            check_time += Time.deltaTime;
            if(check_time >= say_event[0].view_time[say_viewid])
            {
                check_time = 0;
                view_mode = 0;
            }
        }
        else if(say_event[0].text_anim.GetInteger("Anumber") != 1)
        {
            view_trg = false;
            ViewEnd();
        }
    }
    void ViewEnd()
    {
        for(int i = 0; i < say_event.Length;)
        {
            say_event[i].text_anim.SetInteger("Anumber", 1);
            i++;
        }
        if (say_viewid >= say_event[0].say_textjp.Length)
        {
            end_say += 1;
        }

    }
    void ViewStart(int add_id = 0, int target_id = 0)
    {
        if (audioSource != null && se != null)
            audioSource.PlayOneShot(se);
        if(es != null)
        {
            if (!es.body_mirror)
            {
                Vector3 tmp_scale = say_event[target_id].text_mesh.gameObject.transform.localScale;
                tmp_scale.x = Mathf.Abs(tmp_scale.x);
                say_event[target_id].text_mesh.gameObject.transform.localScale = tmp_scale;
            }
            else if(es.body_mirror)
            {
                Vector3 tmp_scale = say_event[target_id].text_mesh.gameObject.transform.localScale;
                tmp_scale.x = -Mathf.Abs(tmp_scale.x);
                say_event[target_id].text_mesh.gameObject.transform.localScale = tmp_scale;
            }
        }
        if (GManager.instance.isEnglish == 0)
        {
            say_event[target_id].text_mesh.fontSize = 38;
            say_event[target_id].text_mesh.text = say_event[target_id].say_textjp[say_viewid];
        }
        else
        {
            say_event[target_id].text_mesh.fontSize = 34;
            say_event[target_id].text_mesh.text = say_event[target_id].say_texten[say_viewid];
        }
        say_viewid += add_id;
        if (say_viewid >= say_event[target_id].say_textjp.Length)
        {
            say_viewid = 0;
        }
            
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "player")
            view_trg = true;
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.tag == "player" && !view_trg)
            view_trg = true;
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "player")
        {
            view_trg = false;
            say_viewid = 0;
            ViewEnd();
        }
    }
}
