using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopui : MonoBehaviour
{
    public int shop_id = 0;
    public GameObject commentui;
    public Text itemname;
    public Image itemimg;
    public Text lvprice;
    public Text commenttext;
    private int pricetemp;
    private float cooltime = 0;
    // Start is called before the first frame update
    void Start()
    {
        ItemView();
    }

    // Update is called once per frame
    void Update()
    {
        if(cooltime >= 0)
        {
            cooltime -= Time.deltaTime;
        }
    }
    public void PointEnterUI()
    {
        commentui.SetActive(true);
    }
    public void PointExitUI()
    {
        commentui.SetActive(false);
    }
    public void ItemView()
    {
        itemimg.sprite = GManager.instance.shopitems[GManager.instance.day_shopitemID[shop_id]].shopitem_image;
        if (GManager.instance.isEnglish == 0)
        {
            itemname.text = GManager.instance.shopitems[GManager.instance.day_shopitemID[shop_id]].shopitem_name[0];
            commenttext.text = GManager.instance.shopitems[GManager.instance.day_shopitemID[shop_id]].shopitem_script[0];
            lvprice.fontSize = 24;
            lvprice.text = "購入Lv：" + GManager.instance.shopitems[GManager.instance.day_shopitemID[shop_id]].shopitem_lv.ToString() + "　値段：" + ((int)(GManager.instance.shopitems[GManager.instance.day_shopitemID[shop_id]].shopitem_startprice + (GManager.instance.shopitems[GManager.instance.day_shopitemID[shop_id]].shopitem_startprice * GManager.instance.shopitems[GManager.instance.day_shopitemID[shop_id]].shopitem_lv+1) / 2)).ToString() + "GF";
        }
        else
        {
            itemname.text = GManager.instance.shopitems[GManager.instance.day_shopitemID[shop_id]].shopitem_name[1];
            commenttext.text = GManager.instance.shopitems[GManager.instance.day_shopitemID[shop_id]].shopitem_script[1];
            lvprice.fontSize = 20;
            lvprice.text = "Purchase Lv：" + GManager.instance.shopitems[GManager.instance.day_shopitemID[shop_id]].shopitem_lv.ToString() + "　Price：" + ((int)(GManager.instance.shopitems[GManager.instance.day_shopitemID[shop_id]].shopitem_startprice + (GManager.instance.shopitems[GManager.instance.day_shopitemID[shop_id]].shopitem_startprice * GManager.instance.shopitems[GManager.instance.day_shopitemID[shop_id]].shopitem_lv+1) / 2)).ToString() + "GF";
        }
        pricetemp = ((int)(GManager.instance.shopitems[GManager.instance.day_shopitemID[shop_id]].shopitem_startprice + (GManager.instance.shopitems[GManager.instance.day_shopitemID[shop_id]].shopitem_startprice * GManager.instance.shopitems[GManager.instance.day_shopitemID[shop_id]].shopitem_lv + 1) / 2));
    }
    public void ClickShop()
    {
        if(pricetemp != 0 && pricetemp <= GManager.instance.get_coin && cooltime <= 0 && (GManager.instance.shopitems[GManager.instance.day_shopitemID[shop_id]].shopitem_lv < GManager.instance.shopitems[GManager.instance.day_shopitemID[shop_id]].shopitem_maxlv || -1 == GManager.instance.shopitems[GManager.instance.day_shopitemID[shop_id]].shopitem_maxlv))
        {
            cooltime = 1f;
            GManager.instance.setrg = 10;
            GManager.instance.get_coin -= pricetemp;
            GManager.instance.shopitems[GManager.instance.day_shopitemID[shop_id]].shopitem_lv += 1;
            ShopEvent(GManager.instance.day_shopitemID[shop_id]);
            ItemView();
        }
        else if(cooltime <= 0)
        {
            cooltime = 0.3f;
            GManager.instance.setrg = 4;
        }
    }
    void ShopEvent(int quick_id = 0)
    {
        ;
    }
}
