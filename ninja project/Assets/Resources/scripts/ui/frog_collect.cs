using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class frog_collect : MonoBehaviour
{
    private List<int> view_allfrog = new List<int>();
    private int select_page = 0;
    private float cooltime = 0;
    [Header("表示用")]
    public Text frog_name;
    public Text frog_script;
    public Text frog_state;
    public Image frog_image;
    [Header("効果音等の演出用")]
    public AudioSource audioSource;
    [Header("0=on,1=no")] public AudioClip[] se;

    // Start is called before the first frame update
    void Start()
    {
        view_allfrog = new List<int>();
        for (int i = 0; i < GManager.instance.all_frog.Length;)
        {
            if (GManager.instance.all_frog[i].check_trg > 0 )
                view_allfrog.Add(i);
            i++;
        }
        if (view_allfrog.Count > 0)
        {
            select_page = 0;
            PageView(0);
        }
    }
    private void Update()
    {
        if (cooltime >= 0f)
            cooltime -= Time.deltaTime;
    }
    public void NextPage()
    {
        if (select_page < view_allfrog.Count-1 && cooltime <= 0)
        {
            cooltime = 0.3f;
            audioSource.PlayOneShot(se[0]);
            select_page += 1;
            PageView(select_page);
        }
        else if (cooltime <= 0)
        {
            cooltime = 0.3f;
            audioSource.PlayOneShot(se[1]);
        }
    }
    public void ReturnPgae()
    {
        if (select_page > 0 && cooltime <= 0)
        {
            cooltime = 0.3f;
            audioSource.PlayOneShot(se[0]);
            select_page -= 1;
            PageView(select_page);
        }
        else if (cooltime <= 0)
        {
            cooltime = 0.3f;
            audioSource.PlayOneShot(se[1]);
        }
    }
    private void PageView(int page_frogID = 0)
    {
        string mode_text = "HARD　×" + GManager.instance.all_frog[view_allfrog[page_frogID]].hardhp + "　　NORMAL　×" + GManager.instance.all_frog[view_allfrog[page_frogID]].normalhp + "　　EASY　×" + GManager.instance.all_frog[view_allfrog[page_frogID]].easyhp+"\n";
        if (GManager.instance.isEnglish == 0)
        {
            frog_name.text = GManager.instance.all_frog[view_allfrog[page_frogID]].jp_name;
            frog_script.text = GManager.instance.all_frog[view_allfrog[page_frogID]].jp_script;
        }
        else if (GManager.instance.isEnglish != 0)
        {
            frog_name.text = GManager.instance.all_frog[view_allfrog[page_frogID]].en_name;
            frog_script.text = GManager.instance.all_frog[view_allfrog[page_frogID]].en_script;
        }
        frog_state.text =  mode_text+ "　×" + GManager.instance.all_frog[view_allfrog[page_frogID]].at + "    　×" + GManager.instance.all_frog[view_allfrog[page_frogID]].speed + " 　 ：" + GManager.instance.all_frog[view_allfrog[page_frogID]].voice + "　　：" + GManager.instance.all_frog[view_allfrog[page_frogID]].runtime + "sec";

        //get_itemimage.sprite = null;
        frog_image.sprite = GManager.instance.all_frog[view_allfrog[page_frogID]].frog_image;
    }
}
