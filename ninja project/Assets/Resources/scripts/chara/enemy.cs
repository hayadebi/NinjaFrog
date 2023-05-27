using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class enemy : MonoBehaviour
{
    public GameObject body;
    public ColEvent groundCol;
    [Header("停止気にするな")] public bool stoptrg = false;
    public float overhight = 9999;
    private bool highttrg = false;
    public float gravity = -8;
    public GameObject colobj;
    public int jumpmode = 0;
    public string Anumbername = "Anumber";
    private float damagetrg = 0;
    [System.Serializable]
    public struct ALLSE
    {
        public AudioClip walkse;
        public AudioClip damagese;
        public AudioClip dsse;
        public AudioClip i_se;
    }
    public ALLSE all_se;
    AudioSource audioSource;
    public Animator anim;
    Rigidbody rb;

    private Vector3 target_cpos;
    private float ySpeed = 0;
    private float zSpeed = 0;
    private float xSpeed = 0;

    private player ps;
    public int enemy_mode = 0;
    public int enemy_hp = 1;
    public int enemy_at = 1;
    public int enemy_id = 1;
    public bool enemy_dstrg = false;
    public float enemy_speed = 0.4f;
    public float auto_moveXtime = 0;
    public float auto_moveZtime = 0;
    private float x_movetime = 0;
    private float z_movetime = 0;
    private float x_movevec = 1;
    private float z_movevec = 1;
    public ColEvent enemy_qarea;
    public ColEvent enemy_ifarea;
    public ColEvent enemy_minarea;
    public int enemy_limitvoice = 50;
    public float run_time = 30;
    public Image ui_image;
    public Text ui_text;
    public Sprite[] mode_image;
    public float[] mode_r;
    public float[] mode_g;
    public float[] mode_b;
    public float[] mode_a;
    public int old_mode;
    //private bool start_run = false;
    private float enemy_animtime = 0;
    private SpriteRenderer this_sprite;
    public float knockback_up = 8;
    private SpriteRenderer chara_sprite;
    private Color damage_color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
    private Color normal_color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    private float dashspeed = 1;
    private child_setactive check_run;
    public AudioClip run_setBGM;
    private AudioClip normal_oldBGM;
    public AudioSource get_BGM;
    public CrossFade get_Cross;
    private int selectstagemission = 0;
    public bool body_mirror = false;
    public bool auto_rot = true;
    private float remove_speed = 1;
    public int drop_coinnum=3;
    public int selectmission_addtrg = -1;//+=値を入力
    public enemy event_enemy = null;
    public bool addevent = false;
    public bool bosstrg = false;
    void Start()
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
        if (GManager.instance.mode == 0)
        {
            enemy_limitvoice += 5;
            if(enemy_hp > 1)
                enemy_hp -= 1;
        }
        else if (GManager.instance.mode == 2)
        {
            enemy_hp += 1;
            enemy_limitvoice -= 5;
        }
        get_BGM = GameObject.Find("BGM").GetComponent<AudioSource>();
        get_Cross = GameObject.Find("BGM").GetComponent<CrossFade>();
        normal_oldBGM = get_BGM.clip;
        check_run = GameObject.Find("check_run").GetComponent <child_setactive>();
        this_sprite = anim.gameObject.GetComponent<SpriteRenderer>();
        audioSource = this.GetComponent<AudioSource>();
        rb = this.GetComponent<Rigidbody>();
        rb.useGravity = false;
        target_cpos = Camera.main.gameObject.transform.position - this.transform.position;
        anim.SetInteger(Anumbername, 0);
        ps = GameObject.Find("Player").GetComponent<player>();
        if(GManager.instance.isEnglish == 0)
        {
            ui_text.text = "!" + enemy_limitvoice.ToString() + "ゲロ";
        }
        else if (GManager.instance.isEnglish == 1)
        {
            ui_text.text = "!" + enemy_limitvoice.ToString() + "ribbit";
        }

        ui_image.sprite = mode_image[enemy_mode];
        ui_image.color = new Color(mode_r[enemy_mode], mode_g[enemy_mode], mode_b[enemy_mode], mode_a[enemy_mode]);
        old_mode = enemy_mode;
        chara_sprite = anim.gameObject.GetComponent<SpriteRenderer>();
    }
    void image_change()
    {
        ui_image.sprite = mode_image[enemy_mode];
        ui_image.color = new Color(mode_r[enemy_mode], mode_g[enemy_mode], mode_b[enemy_mode], mode_a[enemy_mode]);
        old_mode = enemy_mode;
    }
    public void AbsoluteRun()
    {
        enemy_mode = 3;
        GManager.instance.empty_player = false;
        GManager.instance.run_number += 1;

        //if(all_se.i_se != null)
        //    audioSource.PlayOneShot(all_se.i_se);
        if (check_run != null)
            check_run.set_mode = true;
        if ((run_setBGM != null && get_BGM.clip == normal_oldBGM) || (bosstrg && get_BGM.clip != run_setBGM))
        {
            StartCoroutine(nameof(CountStop));
        }
        if(GManager.instance.start_enemy == null )
            GManager.instance.start_enemy = this.gameObject;
        if (GManager.instance.parent_runtrg == 0)
            GManager.instance.parent_runtrg = 1;
        if (GManager.instance.child_runtrg == 0)
        {
            GManager.instance.child_runtrg = 1;
            GManager.instance.freenums[1] = 1;
        }
        GManager.instance.freenums[0] = run_time;
        image_change();

    }
    IEnumerator CountStop()
    {
        get_Cross.AudioOut();
        yield return new WaitForSeconds(0.3f);
        get_BGM.Stop();
        get_BGM.clip = run_setBGM;
        try
        {
            get_BGM.time = GManager.instance.runbgm_starttime;
            get_BGM.Play();
        }
        catch (System.Exception e)
        {
            GManager.instance.runbgm_starttime = 0;
            get_BGM.time = GManager.instance.runbgm_starttime;
            get_BGM.Play();
        }
        
        get_Cross.AudioIn();
    }
    void FixedUpdate()
    {
        //追いかけっこの元凶なら
        if (!GManager.instance.over && GManager.instance.start_enemy == this.gameObject)
        {
            if (GManager.instance.child_runtrg > 0)
            {
                GManager.instance.freenums[1] -= Time.deltaTime;
                if (GManager.instance.freenums[1] <= 0)
                {
                    GManager.instance.freenums[1] = 0;
                    GManager.instance.child_runtrg = 0;
                    
                }
            }
            GManager.instance.freenums[0] -= Time.deltaTime;
            if(GManager.instance.Triggers[GManager.instance.all_mission[selectstagemission].targettrg_id] < GManager.instance.all_mission[selectstagemission].targettrg_num&& GManager.instance.freenums[0] <= 0&&(GManager.instance.run_number <= 0||GManager.instance.empty_player||!this_sprite.isVisible))//||(GManager.instance.empty_player&& (!enemy_ifarea.ColTrigger || GManager.instance.empty_player || !this_sprite.isVisible)))
            {
                if (normal_oldBGM != null)
                {
                    GManager.instance.runbgm_starttime = get_BGM.time;
                    get_BGM.Stop();
                    get_BGM.clip = normal_oldBGM;
                    get_BGM.Play();
                }
                if (check_run != null)
                    check_run.set_mode = false;
                GManager.instance.freenums[0] = 0;
                GManager.instance.parent_runtrg = 0;
                GManager.instance.child_runtrg = 0;
                GManager.instance.freenums[1] = 0;
                enemy_mode = 0;
                GManager.instance.run_number = 0;
                GManager.instance.start_enemy = null;
                anim.SetInteger(Anumbername, 0);
                audioSource.Stop();
                rb.velocity = Vector3.zero;
            }
        }
        else if (!GManager.instance.over && GManager.instance.child_runtrg == 0 && enemy_mode == 3)
        {
            enemy_mode = 0;
            anim.SetInteger(Anumbername, 0);
            audioSource.Stop();
            rb.velocity = Vector3.zero;
            if (GManager.instance.run_number > 0)
                GManager.instance.run_number -= 1;
        }
        //モード管理
        if (!GManager.instance.over && !stoptrg && !enemy_dstrg)
        {
            if (GManager.instance.walktrg)
            {
                if(GManager.instance.child_runtrg > 0 && this_sprite.isVisible && enemy_mode != 3)
                {
                    enemy_mode = 3;
                    GManager.instance.run_number += 1;
                }
                else if (!enemy_qarea.ColTrigger && !enemy_ifarea.ColTrigger && enemy_mode != 3)
                {
                    enemy_mode = 0;
                }
                else if (enemy_qarea.ColTrigger && !enemy_ifarea.ColTrigger && enemy_mode != 3)
                {
                    enemy_mode = 1;
                }
                else if (((enemy_qarea.ColTrigger && enemy_ifarea.ColTrigger && GManager.instance.live_volume < 0.9f && Mathf.Round(GManager.instance.live_volume * 100) >= enemy_limitvoice && !GManager.instance.empty_player )|| (enemy_minarea.ColTrigger && !GManager.instance.empty_player) ||(enemy_ifarea.ColTrigger && (GManager.instance.parent_runtrg==1 || GManager.instance.child_runtrg ==1)&&!GManager.instance.empty_player&& Mathf.Round(GManager.instance.live_volume * 100) >= enemy_limitvoice/2))&& enemy_mode != 3)
                {
                    enemy_mode = 3;
                    GManager.instance.run_number += 1;
                }
                else if (enemy_qarea.ColTrigger && enemy_ifarea.ColTrigger && enemy_mode != 3)
                {
                    enemy_mode = 2;
                }
                //判定
                if (enemy_mode != old_mode)
                {
                    old_mode = enemy_mode;
                    if (enemy_mode == 3)
                    {
                        audioSource.PlayOneShot(all_se.i_se);
                        if ((run_setBGM != null && get_BGM.clip == normal_oldBGM) || (bosstrg && get_BGM.clip != run_setBGM))
                        {
                            StartCoroutine(nameof(CountStop));
                        }
                        if (check_run != null)
                            check_run.set_mode = true;
                        if (GManager.instance.start_enemy == null)
                            GManager.instance.start_enemy = this.gameObject;
                        if (GManager.instance.parent_runtrg == 0)
                            GManager.instance.parent_runtrg = 1;
                        if (GManager.instance.child_runtrg == 0)
                        {
                            GManager.instance.child_runtrg = 1;
                            GManager.instance.freenums[1] = 1;
                        }
                        GManager.instance.freenums[0] = run_time;
                    }
                    image_change();
                }
            }
            else if(!GManager.instance.walktrg && rb.velocity != Vector3.zero)
            {
                rb.velocity = Vector3.zero;
            }
            
            //ダメージ関連の一部
            if (damagetrg > 0)
            {
                damagetrg -= Time.deltaTime;
            }
            else if (anim.GetInteger(Anumbername) == 2)
            {
                anim.SetInteger(Anumbername, 0);
            }
            //色
            if (chara_sprite.color != normal_color && damagetrg <= 1)
            {
                chara_sprite.color = normal_color;
                zSpeed = 0;
                xSpeed = 0;
            }
            //視点移動
            if (GManager.instance.setmenu == 0)
            {
                if (enemy_mode == 3)
                {
                    var tmp_vec = ps.gameObject.transform.position - body.transform.position;
                    if (tmp_vec.x >= 0)
                    {
                        Vector3 tmp_scale = anim.gameObject.transform.localScale;
                        if (damagetrg <= 1)
                        {
                            tmp_scale.x = Mathf.Abs(tmp_scale.x);
                        }
                        else
                        {
                            tmp_scale.x = -Mathf.Abs(tmp_scale.x);
                        }
                        anim.gameObject.transform.localScale = tmp_scale;
                    }
                    else if (tmp_vec.x < 0)
                    {
                        Vector3 tmp_scale = anim.gameObject.transform.localScale;
                        if (damagetrg <= 1)
                        {
                            tmp_scale.x = -Mathf.Abs(tmp_scale.x);
                        }
                        else
                        {
                            tmp_scale.x = Mathf.Abs(tmp_scale.x);
                        }
                        anim.gameObject.transform.localScale = tmp_scale;
                    }
                    var rotation = Quaternion.LookRotation(tmp_vec);
                    rotation.x = 0;
                    groundCol.gameObject.transform.rotation = rotation;
                }
                else if((auto_moveXtime != 0|| auto_moveZtime != 0) && damagetrg <= 1 && auto_rot )
                {
                    var tmp_vec = ps.gameObject.transform.position - body.transform.position;
                    if (auto_moveXtime != 0 && x_movevec >= 1)
                    {
                        Vector3 tmp_scale = anim.gameObject.transform.localScale;
                        tmp_scale.x = Mathf.Abs(tmp_scale.x);
                        anim.gameObject.transform.localScale = tmp_scale;
                    }
                    else if (auto_moveXtime != 0 && x_movevec <= -1)
                    {
                        Vector3 tmp_scale = anim.gameObject.transform.localScale;
                        tmp_scale.x = -Mathf.Abs(tmp_scale.x);
                        anim.gameObject.transform.localScale = tmp_scale;
                    }
                    else if (auto_moveZtime != 0 && z_movevec <= -1)
                    {
                        Vector3 tmp_scale = anim.gameObject.transform.localScale;
                        tmp_scale.x = -Mathf.Abs(tmp_scale.x);
                        anim.gameObject.transform.localScale = tmp_scale;
                    }
                    else if (auto_moveZtime != 0 && z_movevec >= 1)
                    {
                        Vector3 tmp_scale = anim.gameObject.transform.localScale;
                        tmp_scale.x = Mathf.Abs(tmp_scale.x);
                        anim.gameObject.transform.localScale = tmp_scale;
                    }
                    var rotation = Quaternion.LookRotation(tmp_vec);
                    rotation.x = 0;
                    groundCol.gameObject.transform.rotation = rotation;
                }
                else if(auto_rot )
                {
                    var tmp_vec = ps.gameObject.transform.position - body.transform.position;
                    if (tmp_vec.x >= 0)
                    {
                        body_mirror = true;
                        Vector3 tmp_scale = anim.gameObject.transform.localScale;
                        tmp_scale.x = -Mathf.Abs(tmp_scale.x);
                        anim.gameObject.transform.localScale = tmp_scale;
                    }
                    else if (tmp_vec.x < 0)
                    {
                        body_mirror = false;
                        Vector3 tmp_scale = anim.gameObject.transform.localScale;
                        tmp_scale.x = Mathf.Abs(tmp_scale.x);
                        anim.gameObject.transform.localScale = tmp_scale;
                    }
                    var rotation = Quaternion.LookRotation(tmp_vec);
                    rotation.x = 0;
                    groundCol.gameObject.transform.rotation = rotation;
                }
            }
            //----
            if (GManager.instance.walktrg )
            {
                //----移動----
                if (damagetrg <= 0 && ((enemy_mode == 3 && !enemy_minarea.name2Trigger && !GManager.instance.empty_player) ||(enemy_mode != 3 && (auto_moveXtime != 0 || auto_moveZtime != 0))) && groundCol.ColTrigger && jumpmode == 0 && enemy_animtime <= 0 )
                {
                    enemy_animtime = 0.45f;
                    var upp = transform.position;
                    transform.position = upp;
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
                if ((!(jumpmode != 0 && audioSource.isPlaying) &&( enemy_animtime <= 0.05 && enemy_animtime > 0))||(enemy_mode == 3 && GManager.instance.empty_player))
                {
                    anim.SetInteger(Anumbername, 0);
                    zSpeed = 0;
                }
                if (enemy_mode == 3 && damagetrg <= 0 && !enemy_minarea.name2Trigger && !GManager.instance.empty_player)
                    zSpeed = GManager.instance.voice_volume + 2;

                x_movetime += Time.deltaTime;
                z_movetime += Time.deltaTime;
                if(x_movetime >= auto_moveXtime )
                {
                    x_movetime = 0;
                    x_movevec *= -1;
                }
                if (z_movetime >= auto_moveZtime)
                {
                    z_movetime = 0;
                    z_movevec *= -1;
                }

                if (enemy_mode != 3 && damagetrg <= 0 && auto_moveZtime != 0)
                    zSpeed = (GManager.instance.voice_volume+1 )*z_movevec ;
                if (enemy_mode != 3 && damagetrg <= 0 && auto_moveXtime != 0)
                    xSpeed = (GManager.instance.voice_volume+1 )*x_movevec;

                if (enemy_animtime > 0)
                    enemy_animtime -= Time.deltaTime;
                tempVc = new Vector3(xSpeed, 0, zSpeed);
                if (tempVc.magnitude > 1) tempVc = tempVc.normalized;
                dashspeed = 1;
                if (damagetrg > 1f)
                    dashspeed = knockback_up;

                var vec = (groundCol.gameObject.transform.forward * tempVc.z + groundCol.gameObject.transform.right * tempVc.x);
                if (damagetrg > 1 || (damagetrg <= 1 && enemy_mode != 3 && (auto_moveXtime != 0 || auto_moveZtime != 0)))
                    vec = tempVc;
                var movevec = vec * ((GManager.instance.Pstatus.speed / (2 - enemy_speed)) * dashspeed/remove_speed) + (body.transform.up * ySpeed);
                rb.velocity = movevec;
            }
        }
        //死亡時
        else if ((!GManager.instance.walktrg || GManager.instance.setmenu > 0 || stoptrg || enemy_dstrg ||GManager.instance.empty_player)&& (rb.velocity != Vector3.zero || (enemy_dstrg && this.gameObject.tag != "untag")))
        {
            audioSource.Stop();
            rb.velocity = Vector3.zero;
            if(enemy_dstrg)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
                this.gameObject.tag = "untag";
                enemy_qarea.gameObject.tag = "untag";
                anim.SetInteger(Anumbername, 3);
                ui_image.sprite = mode_image[mode_image.Length -1];
                audioSource.PlayOneShot(all_se.dsse);
                if (GManager.instance.run_number > 0)
                    GManager.instance.run_number -= 1;
            }
            else
            {
                anim.SetInteger(Anumbername, 0);
            }
        }
        //当たり判定の補強
        if (damagetrg <= 0 && colobj != null && !enemy_dstrg && !GManager.instance.empty_player)
        {
            ColEvents(colobj);
            colobj = null;
            if ((enemy_mode != 3 && !enemy_dstrg) || (enemy_mode != 3 && enemy_dstrg && enemy_qarea.name2Trigger))
            {
                enemy_mode = 3;
                GManager.instance.run_number += 1;
                if (GManager.instance.start_enemy == null)
                    GManager.instance.start_enemy = this.gameObject;
                if (GManager.instance.parent_runtrg == 0)
                {
                    if ((run_setBGM != null && get_BGM.clip == normal_oldBGM) || (bosstrg && get_BGM.clip != run_setBGM))
                    {
                        StartCoroutine(nameof(CountStop));
                    }
                    audioSource.PlayOneShot(all_se.i_se);
                    GManager.instance.parent_runtrg = 1;
                    GManager.instance.child_runtrg = 1;
                    GManager.instance.freenums[1] = 1;
                }
                GManager.instance.freenums[0] = run_time;
                image_change();
            }
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "spider")
        {
            remove_speed = 2;
        }
    }
    private void OnTriggerStay(Collider col)
    {
        if (!GManager.instance.over && damagetrg <= 0 && (col.tag == "player" || col.tag == "p_bullet")  && colobj == null &&GManager.instance.setmenu <= 0 && GManager.instance.walktrg && !GManager.instance.empty_player)
        {
            col.GetComponent<player>().colobj = this.gameObject;
            ColEvents(col.gameObject);
            if ((enemy_mode != 3 && !enemy_dstrg) || (enemy_mode != 3 && enemy_dstrg && enemy_qarea.name2Trigger))
            {
                enemy_mode = 3;
                GManager.instance.run_number += 1;
                if (GManager.instance.start_enemy == null)
                    GManager.instance.start_enemy = this.gameObject;
                if (GManager.instance.parent_runtrg == 0)
                {
                    if ((run_setBGM != null && get_BGM.clip == normal_oldBGM) || (bosstrg &&get_BGM.clip != run_setBGM))
                    {
                        StartCoroutine(nameof(CountStop));
                    }
                    audioSource.PlayOneShot(all_se.i_se);
                    GManager.instance.parent_runtrg = 1;
                    GManager.instance.child_runtrg = 1;
                    GManager.instance.freenums[1] = 1;
                }
                GManager.instance.freenums[0] = run_time;
                image_change();
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "spider")
        {
            remove_speed = 1;
        }
    }
    public void Explosion(float tarea = 1f ,float tforce = 1f,float tup = 1f)
    {
        // 範囲内のRigidbodyにAddExplosionForce
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, tarea);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            var rb = hitColliders[i].GetComponent<Rigidbody>();
            if (rb)
            {
                rb.AddExplosionForce(20, transform.position , tforce, tup, ForceMode.Impulse);
            }
        }
    }
    private void ColEvents(GameObject coltarget)
    {
        if (!GManager.instance.empty_player)
        {
            damagetrg = 1.3f;
            if (GManager.instance.all_frog[enemy_id].check_trg < 1)
                GManager.instance.all_frog[enemy_id].check_trg = 1;
            if (GManager.instance.dashtrg)
            {
                audioSource.PlayOneShot(all_se.damagese);
                anim.SetInteger(Anumbername, 2);
                chara_sprite.color = damage_color;
                enemy_hp -= 1;
                if (enemy_hp <= 0)
                {
                    enemy_dstrg = true;
                    if (selectmission_addtrg != -1 && event_enemy != null && event_enemy.addevent == false)
                    {
                        addevent = true;
                        GManager.instance.Triggers[GManager.instance.all_mission[GManager.instance.select_mission].targettrg_id] += selectmission_addtrg;
                        GManager.instance.setrg = 7;
                    }
                    GManager.instance.setrg = 5;
                    Vector3 coinP = this.transform.position;
                    for (int i = 0; i < drop_coinnum;)
                    {
                        float randomp = 0;
                        randomp = UnityEngine.Random.Range(-drop_coinnum / 3, (drop_coinnum / 3 + 0.1f));
                        coinP.x += randomp;
                        randomp = UnityEngine.Random.Range(0.1f, (drop_coinnum / 3 + 0.1f));
                        coinP.y += randomp;
                        randomp = UnityEngine.Random.Range(-drop_coinnum / 3, (drop_coinnum / 3 + 0.1f));
                        coinP.z += randomp;
                        Instantiate(GManager.instance.coinobj, coinP, transform.rotation);
                        i += 1;
                    }
                    Explosion(3f, 0.5f, 0.15f);
                }
            }
            rb.velocity = Vector3.zero;
            Vector3 distination = (transform.position - coltarget.transform.position).normalized;
            zSpeed = distination.z;
            xSpeed = distination.x;
        }
    }

}