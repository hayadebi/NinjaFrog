using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ochiba : MonoBehaviour
{
    private AudioSource audiosource;
    public AudioClip se;
    [Header("通常時の設定")]
    public GameObject shake_obj;
    public GameObject shake_effect;
    private GameObject Player;
    private player ps;
    private SpriteRenderer p_childimage;
    public float esc_volume = 50;
    private bool ok_esc = false;
    public TextMesh esc_text;
    private MeshRenderer esctext_obj;

    [Header("敵が潜伏してるかの設定")]
    public enemy on_enemyobj;
    public float check_enemymaxvoice = -1;
    public ColEvent enemy_colevent;
    private float shake_time = 0;
    private bool empty_enemy = false;
    private float in_time = 0;
    public SpriteRenderer visible_sprite;
    
    // Start is called before the first frame update
    void Start()
    {
        audiosource = this.GetComponent<AudioSource>();
        Player = GameObject.Find("Player");
        ps = Player.GetComponent<player>();
        p_childimage = Player.transform.Find("image").gameObject.GetComponent<SpriteRenderer>();
        if (GManager.instance.isEnglish == 0)
        {
            esc_text.text = "退出声量：" + Mathf.Round(esc_volume*100).ToString() + "ゲロ";
        }
        else if (GManager.instance.isEnglish == 1)
        {
            esc_text.text = "Exit volume:" + Mathf.Round(esc_volume * 100).ToString() + "ribbit";
        }
        esctext_obj = esc_text.gameObject.GetComponent<MeshRenderer>();
        esctext_obj.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(on_enemyobj != null && enemy_colevent != null  && !empty_enemy)
        {
            if (enemy_colevent.ColTrigger)
            {
                shake_time = 5;
                empty_enemy = true;
                Instantiate(shake_effect, transform.position, transform.rotation);
                iTween.ShakePosition(shake_obj.gameObject, iTween.Hash("x", 0.5f, "time", 0.3f));
                audiosource.PlayOneShot(se);
                on_enemyobj.gameObject.SetActive(true);
                on_enemyobj.AbsoluteRun();
            }
            else if (shake_time <= 0 && !enemy_colevent.ColTrigger && visible_sprite.isVisible)
            {
                shake_time = 5;
                Instantiate(shake_effect, transform.position, transform.rotation);
                iTween.ShakePosition(shake_obj.gameObject, iTween.Hash("x", 0.5f,"time",0.3f));
                audiosource.PlayOneShot(se);
            }
            else if(visible_sprite.isVisible )
            {
                shake_time -= Time.deltaTime;
            }
        }
        if ((on_enemyobj == null || empty_enemy)&& GManager.instance.empty_player
            && GManager.instance.live_volume >= esc_volume && !ok_esc && in_time <= 0)
        {
            ok_esc = true;
        }
        else if (GManager.instance.live_volume < esc_volume/2 && p_childimage!=null && ok_esc && in_time <= 0)
        {
            ok_esc = false;
            Instantiate(shake_effect, transform.position, transform.rotation);
            iTween.ShakePosition(shake_obj.gameObject, iTween.Hash("x", 0.5f, "time", 0.3f));
            audiosource.PlayOneShot(se);
            GManager.instance.empty_player = false;
            p_childimage.enabled = true;
            esctext_obj.enabled = false;
        }
        if (in_time >= 0f)
            in_time -= Time.deltaTime;
    }
    private void OnTriggerEnter(Collider col)
    {
        if((on_enemyobj == null || empty_enemy)&& col.tag == "player" && p_childimage != null && !GManager.instance.empty_player && !audiosource.isPlaying && !ps.get_missiontarget)
        {
            in_time = 3f;
            if (col.GetComponent<Rigidbody>())
                col.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Instantiate(shake_effect, transform.position, transform.rotation);
            iTween.ShakePosition(shake_obj.gameObject, iTween.Hash("x", 0.5f, "time", 0.3f));
            audiosource.PlayOneShot(se);
            GManager.instance.empty_player = true;
            p_childimage.enabled = false;
            esctext_obj.enabled = true;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if ((on_enemyobj == null || empty_enemy) && col.tag == "player" && p_childimage != null && GManager.instance.empty_player && in_time <= 0)
        {
            Instantiate(shake_effect, transform.position, transform.rotation);
            iTween.ShakePosition(shake_obj.gameObject, iTween.Hash("x", 0.5f, "time", 0.3f));
            audiosource.PlayOneShot(se);
            GManager.instance.empty_player = false;
            p_childimage.enabled = true;
            esctext_obj.enabled = false;
        }
    }
}
