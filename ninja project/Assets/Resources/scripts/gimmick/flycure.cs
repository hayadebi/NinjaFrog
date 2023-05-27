using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flycure : MonoBehaviour
{
    public Transform rightup_max;
    public Transform leftdown_min;
    private SpriteRenderer mysprite;
    public Vector3 nextvec = Vector3.zero;
    public float next_maxtime = 1f;
    private float _time = 1f;
    public float move_speed = 15;
    private float check_minpos = 0.15f;
    private float Xspeed = 0;
    private float Yspeed = 0;
    private float Zspeed = 0;
    public float gravity = -8;
    private Rigidbody rb;
    public int cure_num = 1;
    // Start is called before the first frame update
    void Start()
    {
        _time = next_maxtime;
        mysprite = this.GetComponent<SpriteRenderer>();
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mysprite.isVisible)
        {
            _time += Time.deltaTime;
            if (next_maxtime <= _time)
                SetRandomPos();
            Yspeed = gravity;
            Zspeed = 0;
            Xspeed = 0;
            if(nextvec != Vector3.zero)
            {
                Xspeed = nextvec.x - transform.position.x;
                Zspeed = nextvec.z - transform.position.z;
                if (Xspeed >= 0 && mysprite.flipX)
                {
                    mysprite.flipX = false;
                }
                else if (Xspeed < 0 && !mysprite.flipX)
                {
                    mysprite.flipX = true;
                }
            }
            var tempVc = new Vector3(Xspeed, 0, Zspeed);
            if (tempVc.magnitude > 1) tempVc = tempVc.normalized;
            var movevec = tempVc * move_speed + this.transform.up * Yspeed;
            rb.velocity = movevec;
            if (Mathf.Abs(nextvec.x - transform.position.x) <= check_minpos && Mathf.Abs(nextvec.z - transform.position.z) <= check_minpos)
                SetRandomPos();
        }
        else if(rb.velocity != Vector3.zero)
        {
            rb.velocity = Vector3.zero;
        }
    }
    void SetRandomPos()
    {
        _time = 0;
        nextvec = this.transform.position;
        nextvec.x = Random.Range(leftdown_min.position.x, rightup_max.position.x);
        nextvec.z = Random.Range(leftdown_min.position.z, rightup_max.position.z);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(!GManager.instance.over && GManager.instance.event_on && col.tag == "parea")
        {
            GManager.instance.Pstatus.hp += cure_num;
            if (GManager.instance.Pstatus.hp > GManager.instance.Pstatus.maxHP)
                GManager.instance.Pstatus.hp = GManager.instance.Pstatus.maxHP;
            GManager.instance.setrg = 1;
            Destroy(gameObject);
        }
    }
}
