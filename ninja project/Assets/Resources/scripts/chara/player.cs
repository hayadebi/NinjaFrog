using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class player : MonoBehaviour
{
    public GameObject body;
    public ColEvent groundCol;
    [Header("停止気にするな")] public bool stoptrg = false;
    public float _overhight = 9999;
    private bool highttrg = false;
    public float gravity = 12;
    public GameObject colobj;
    private float jumptime = 0;
    public int jumpmode = 0;
    public float dashtime;
    public float dashspeed;
    public string Anumbername = "Anumber";
    private float damagetrg = 0;
    [System.Serializable]
    public struct ALLSE
    {
        public AudioClip walkse;
        public AudioClip damagese;
        public AudioClip dsse;
        public AudioClip eventse;
    }
    public ALLSE all_se;
    
    AudioSource audioSource;
    public Animator anim;
    Rigidbody rb;
    public Vector3 mousepos;
    
    private bool restarttrg = false;
    private GameObject nearObj;
    private float searchTime = 0;

    private GameObject bgmobj = null;
    private AudioSource bgmaudio = null;
    private Audiovolume bgmvolume = null;

    private bool rap_trg = false;
    private int tmp_fieldcount = 0;
    private Vector3 target_cpos;
    private float ySpeed = 0;
    private float zSpeed = 0;
    private float xSpeed = 0;
    private Animator blur_anim;
    public float knockback_up = 4;
    private SpriteRenderer chara_sprite;
    private Color damage_color = new Color(1.0f, 0.25f, 0.25f,1.0f);
    private Color normal_color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    private bool event_area = false;
    private bool bero_trg = false;
    private float remove_speed = 1;
    public bool get_missiontarget = false;
    GameObject serchTag(GameObject nowObj, string tagName)
    {
        float tmpDis = 0;           //距離用一時変数
        float nearDis = 0;          //最も近いオブジェクトの距離
        //string nearObjName = "";    //オブジェクト名称
        GameObject targetObj = null; //オブジェクト

        //タグ指定されたオブジェクトを配列で取得する
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            //自身と取得したオブジェクトの距離を取得
            tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
            //一時変数に距離を格納
            if (nearDis == 0 || nearDis > tmpDis)
            {
                nearDis = tmpDis;
                //nearObjName = obs.name;
                targetObj = obs;
            }

        }
        //最も近かったオブジェクトを返す
        //return GameObject.Find(nearObjName);
        return targetObj;
    }

    void Start()
    {
        GManager.instance.rock_num = GManager.instance.shopitems[1].shopitem_lv;
        GManager.instance.Triggers[GManager.instance.all_mission[GManager.instance.select_mission].targettrg_id] = 0;
        //開始時リセット
        if (GManager.instance.mode == 0)
        {
            GManager.instance.Pstatus.maxHP = 5;
            GManager.instance.global_grain = 50+(GManager.instance.add_grain *-1);
        }
        else if (GManager.instance.mode == 1)
        {
            GManager.instance.Pstatus.maxHP = 3;
            GManager.instance.global_grain = 45 + (GManager.instance.add_grain * -1);
        }
        else if (GManager.instance.mode == 2)
        {
            GManager.instance.Pstatus.maxHP = 1;
            GManager.instance.global_grain = 40 + (GManager.instance.add_grain * -1);
        }
        GManager.instance.Pstatus.maxHP += GManager.instance.shopitems[0].shopitem_lv;
        GManager.instance.Pstatus.hp = GManager.instance.Pstatus.maxHP;
        GManager.instance.over = false;
        GManager.instance.freenums[0] = 0;
        GManager.instance.parent_runtrg = 0;
        GManager.instance.child_runtrg = 0;
        GManager.instance.freenums[1] = 0;
        GManager.instance.run_number = 0;
        GManager.instance.empty_player = false;
        GManager.instance.event_on = false;

        //取得
        bgmobj = GameObject.Find("BGM");
        if (bgmobj != null)
        {
            bgmaudio = bgmobj.GetComponent<AudioSource>();
            bgmvolume = bgmobj.GetComponent<Audiovolume>();
        }
        audioSource = this.GetComponent<AudioSource>();
        rb = this.GetComponent<Rigidbody>();
        rb.useGravity = false;
        target_cpos = Camera.main.gameObject.transform.position- this.transform.position ;
        blur_anim = Camera.main.gameObject.GetComponent<Animator>();
        anim.SetInteger(Anumbername, 0);
        chara_sprite = anim.gameObject.GetComponent<SpriteRenderer>();
        GManager.instance.start_enemy = null;

    }
    
    void FixedUpdate()
    {
        if (GManager.instance.Pstatus.hp <= 0 && bgmobj != null && !bgmvolume.enabled && bgmaudio.volume > 0)
        {
            bgmaudio.volume -= (Time.deltaTime / 8);
        }
        if(_overhight != 9999 && transform.position.y < -_overhight )
        {
            var temp_vec = transform.position;
            temp_vec.y = _overhight / 2;
            transform.position = temp_vec;

        }
        if (!GManager.instance.over)
        {
            if (GManager.instance.walktrg && GManager.instance.setmenu == 0)
            {
                if (this.transform.position + target_cpos != Camera.main.gameObject.transform.position)
                {
                    Camera.main.gameObject.transform.position = this.transform.position + target_cpos;
                }
                //ダッシュモード
                if (!GManager.instance.event_on && GManager.instance.Pstatus.loadtime <= 0 && !GManager.instance.dashtrg && GManager.instance.voice_volume >= 0.9f && !bero_trg)
                {
                    GManager.instance.Pstatus.loadtime = 0.3f;
                    GManager.instance.Pstatus.maxload = 0.3f;
                    blur_anim.SetInteger(Anumbername, 1);
                    GManager.instance.dashtrg = true;
                    anim.SetInteger(Anumbername, 1);
                }
                else if (GManager.instance.dashtrg)
                {
                    dashtime += Time.deltaTime;
                    if (dashtime > 0.2f)
                    {
                        dashtime = 0;
                        blur_anim.SetInteger(Anumbername, 0);
                        GManager.instance.dashtrg = false;
                    }
                }
                if (damagetrg <= 0 && !GManager.instance.dashtrg && GManager.instance.Pstatus.loadtime <= 0 && !GManager.instance.event_on && GManager.instance.live_volume >= 0.6f && bero_trg)
                {
                    GManager.instance.Pstatus.loadtime = 0.15f;
                    GManager.instance.Pstatus.maxload = 0.15f;
                    GManager.instance.event_on = true;
                    GManager.instance.setrg = 2;
                    anim.SetInteger(Anumbername, 4);
                }
                else if(GManager.instance.event_on && GManager.instance.Pstatus.loadtime <= 0 && GManager.instance.live_volume <= 0.3f)
                {
                    GManager.instance.event_on = false;
                    anim.SetInteger(Anumbername, 0);
                }
                //スキル、技、ダッシュ使用制限タイム
                if (GManager.instance.Pstatus.loadtime > 0)
                {
                    GManager.instance.Pstatus.loadtime -= Time.deltaTime;
                }
                //-------------
            }
            else if(!GManager.instance.walktrg && rb.velocity != Vector3.zero)
            {
                rb.velocity = Vector3.zero;
            }
            if (!stoptrg)
            {
                if (damagetrg >= 0)
                {
                    damagetrg -= Time.deltaTime;
                }
                else if (anim.GetInteger(Anumbername) == 2)
                {
                    anim.SetInteger(Anumbername, 0);
                }
                //色
                if (chara_sprite.color != normal_color && damagetrg <= 0)
                {
                    chara_sprite.color = normal_color;
                }
                //視点移動
                if (GManager.instance.setmenu == 0)
                {
                    var distance = Vector3.Distance(this.transform.position, Camera.main.transform.position);
                    var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
                    mousepos = Camera.main.ScreenToWorldPoint(mousePosition);
                    mousepos.y = this.transform.position.y;
                    var tmp_vec = mousepos - body.transform.position;
                    if (tmp_vec.x >= 0)
                    {
                        Vector3 tmp_scale = anim.gameObject.transform.localScale;
                        Vector3 tmp_position = anim.gameObject.transform.localPosition;
                        if (damagetrg <= 0)
                        {
                            tmp_scale.x = Mathf.Abs(tmp_scale.x);
                            if (GManager.instance.event_on)
                                tmp_position.x = 0.5f;
                        }
                        else
                        {
                            tmp_scale.x = -Mathf.Abs(tmp_scale.x);
                            if (GManager.instance.event_on)
                                tmp_position.x = -0.5f;
                        }
                        anim.gameObject.transform.localPosition = tmp_position;
                        anim.gameObject.transform.localScale = tmp_scale;
                    }
                    else if (tmp_vec.x < 0)
                    {
                        Vector3 tmp_scale = anim.gameObject.transform.localScale;
                        Vector3 tmp_position = anim.gameObject.transform.localPosition;
                        if (damagetrg <= 0)
                        {
                            tmp_scale.x = -Mathf.Abs(tmp_scale.x);
                            if (GManager.instance.event_on)
                                tmp_position.x = -0.5f;
                        }
                        else
                        {
                            tmp_scale.x = Mathf.Abs(tmp_scale.x);
                            if (GManager.instance.event_on)
                                tmp_position.x = 0.5f;
                        }
                        anim.gameObject.transform.localPosition = tmp_position;
                        anim.gameObject.transform.localScale = tmp_scale;
                    }
                    var rotation = Quaternion.LookRotation(tmp_vec);
                    rotation.x = 0;
                    groundCol.gameObject.transform.rotation = rotation;
                }
                //----
                if (GManager.instance.walktrg)
                {
                    //----移動----
                    if (GManager.instance.voice_volume >= 0.015f && groundCol.ColTrigger && jumptime <= 0.0f && !audioSource.isPlaying && damagetrg <= 0 && !GManager.instance.event_on && (GManager.instance.voice_volume < 0.5f || GManager.instance.voice_volume >= 0.9f))
                    {
                        jumptime = 0.45f;
                        var upp = transform.position;
                        transform.position = upp;
                        tmp_fieldcount += 1;
                        //--------------------------
                        if (tmp_fieldcount >= 999)
                        {
                            tmp_fieldcount = 0;
                        }
                        anim.SetInteger(Anumbername, 1);
                        audioSource.PlayOneShot(all_se.walkse);
                    }
                    ySpeed = gravity;
                    if (damagetrg <= 0)
                    {
                        zSpeed = 0;
                        xSpeed = 0;
                    }
                    var tempVc = new Vector3(0, 0, 0);
                    if (GManager.instance.voice_volume >= 0.015f && damagetrg <= 0 && !GManager.instance.event_on)
                    {
                        zSpeed = GManager.instance.voice_volume + 2;
                    }
                    else if (!(jumptime <= 0.05f && GManager.instance.live_volume > 0) && !GManager.instance.event_on )
                    {
                        anim.SetInteger(Anumbername, 0);
                        zSpeed = 0;
                    }
                    if(jumptime >= 0.0f)
                    {
                        jumptime -= Time.deltaTime;
                    }

                    tempVc = new Vector3(xSpeed, 0, zSpeed);
                    if (tempVc.magnitude > 1) tempVc = tempVc.normalized;
                    dashspeed = 1;
                    if (GManager.instance.dashtrg && damagetrg <= 0)
                    {
                        dashspeed = 4;
                    }
                    else if (damagetrg > 0)
                    {
                        dashspeed = knockback_up;
                    }

                    var vec = (groundCol.gameObject.transform.forward * tempVc.z + groundCol.gameObject.transform.right * tempVc.x);
                    if (damagetrg > 0)
                    {
                        vec = tempVc;
                    }

                    var movevec = vec * (GManager.instance.Pstatus.speed * dashspeed /remove_speed) + (body.transform.up * ySpeed);
                    rb.velocity = movevec;
                }
            }
        }
        else if ((!GManager.instance.walktrg || GManager.instance.setmenu > 0 || stoptrg || GManager.instance.over) && (rb.velocity != Vector3.zero || (GManager.instance.over && this.gameObject.tag != "untag")))
        {
            audioSource.Stop();
            rb.velocity = Vector3.zero;
            if (GManager.instance.over)
            {
                GManager.instance.Triggers[GManager.instance.all_mission[GManager.instance.select_mission].targettrg_id] = 0;
                rb.constraints = RigidbodyConstraints.FreezeAll;
                this.gameObject.tag = "untag";
                anim.SetInteger(Anumbername, 3);
                audioSource.PlayOneShot(all_se.dsse);
                GManager.instance.walktrg = false;
                GManager.instance.setmenu = 1;
                Instantiate(GManager.instance.all_ui[0], transform.position, transform.rotation);
            }
            else
            {
                anim.SetInteger(Anumbername, 0);
            }
        }
        if (damagetrg <= 0 && colobj!= null && !GManager.instance.over && colobj.GetComponent<enemy>() && !colobj.GetComponent<enemy>().enemy_dstrg && !GManager.instance.empty_player )
        {
            ColEvents(colobj);
            colobj = null;
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (!GManager.instance.over && col.tag == "bero" && !bero_trg)
            bero_trg = true;
        if(!GManager.instance.over && col.tag == "event" && !event_area )
            event_area = true;
        if (col.tag == "spider")
            remove_speed = 4f;
    }

    private void OnTriggerStay(Collider col)
    {
        if (!GManager.instance.over && col.tag == "bero" && !bero_trg)
            bero_trg = true;
        if (!GManager.instance.over && col.tag == "event" && !event_area)
            event_area = true;
        if (!GManager.instance.over && damagetrg <= 0 && (col.tag == "enemy" || col.tag == "e_bullet")&&col.GetComponent<enemy>() && !col.GetComponent<enemy>().enemy_dstrg  && colobj ==  null && GManager.instance.setmenu <= 0 && GManager.instance.walktrg && !GManager.instance.empty_player)
        {
            col.GetComponent<enemy>().colobj = this.gameObject;
            ColEvents(col.gameObject);
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (!GManager.instance.over && col.tag == "bero")
            bero_trg = false;
        if (!GManager.instance.over && col.tag == "event" && event_area)
            event_area = false;
        if (col.tag == "spider")
            remove_speed = 1f;
    }
    private void ColEvents(GameObject coltarget)
    {
        if (!GManager.instance.empty_player)
        {
            damagetrg = 0.3f;
            if (!GManager.instance.dashtrg)
            {
                audioSource.PlayOneShot(all_se.damagese);
                anim.SetInteger(Anumbername, 2);
                chara_sprite.color = damage_color;
                if (GManager.instance.rock_num <= 0)
                {
                    if (coltarget.GetComponent<enemy>())
                        GManager.instance.Pstatus.hp -= coltarget.GetComponent<enemy>().enemy_at;
                    else
                        GManager.instance.Pstatus.hp -= 1;
                }
                else if(GManager.instance.rock_num > 0)
                {
                    GManager.instance.rock_num -= 1;
                    GManager.instance.setrg = 11;
                    Instantiate(GManager.instance.effectobj[0], transform.position, transform.rotation);
                }
                if (GManager.instance.Pstatus.hp <= 0 && bgmobj != null && bgmaudio != null)
                {
                    GManager.instance.over = true;
                    bgmaudio.Stop();
                    bgmaudio.clip = GManager.instance.managerSE[1];
                    bgmaudio.Play();
                }
            }
            rb.velocity = Vector3.zero;
            Vector3 distination = (transform.position - coltarget.transform.position).normalized;
            zSpeed = distination.z;
            xSpeed = distination.x;
        }
    }
}