using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using UnityEngine.Windows.Speech;
using System.Linq;
public class WordManager : MonoBehaviour
{
    public static WordManager instance = null;
    private bool isPlay = false;
    // Use this for initialization
    public bool voice_trg = false;
    [SerializeField]
    private string[] words;
    DictationRecognizer dictationRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    //public bool wordevent = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        try
        {
            if (GManager.instance.ui_voice == 1)
            {
                for (int i = 0; i < words.Length;)
                {
                    //反応するキーワードを辞書に登録
                    keywords.Add(words[i], () => {; });
                    i++;
                }
            }
            isPlay = true;
        }
        catch (System.Exception)
        {
            if (GManager.instance.ui_voice == 1)
            {
                GManager.instance.ui_voice = 0;
                PlayerPrefs.SetInt("ui_voice", GManager.instance.ui_voice);
            }
            isPlay = false;
        }
    }
    //ワード認識時
    private void DictationRecognizer_DictationResult(string text, ConfidenceLevel confidence)
    {
        //System.Action keywordAction;//　keywordActionという処理を行う
        //認識されたキーワードが辞書に含まれている場合に、アクションを呼び出す。
        //if (keywords.TryGetValue(text, out keywordAction) && GManager.instance.get_word != text)
        //{
        //    GManager.instance.get_word = text;
        //    text = "";
        //    //wordevent
        //        SetWordEvent();
        //}
        ;
    }
    //入力中
    private void DictationRecognizer_DictationHypothesis(string text)
    {
        System.Action keywordAction;//　keywordActionという処理を行う
        //認識されたキーワードが辞書に含まれている場合に、アクションを呼び出す。
        if (keywords.TryGetValue(text, out keywordAction) && GManager.instance.get_word != text)
        {
            GManager.instance.get_word = text;
            text = "";
            //worevent
                SetWordEvent();

        }
    }
    //認識終了時
    private void DictationRecognizer_DictationComplete(DictationCompletionCause cause)
    {
        voice_trg = false;
    }
    //エラー時
    private void DictationRecognizer_DictationError(string error, int hresult)
    {
        ;
    }
    // Update is called once per frame
    void Update()
    {
        try
        {
            if (isPlay && GManager.instance.live_volume >= 0.04f && !voice_trg && GManager.instance.ui_voice == 1 && GManager.instance.walktrg)
            {
                //ディクテーションを開始
                dictationRecognizer = new DictationRecognizer();
                dictationRecognizer.Start();
                voice_trg = true;
            }
            else if (voice_trg && GManager.instance.walktrg)
            {
                dictationRecognizer.DictationResult += DictationRecognizer_DictationResult;//DictationRecognizer_DictationResult処理を行う
                dictationRecognizer.DictationHypothesis += DictationRecognizer_DictationHypothesis;//DictationRecognizer_DictationHypothesis処理を行う
                dictationRecognizer.DictationComplete += DictationRecognizer_DictationComplete;//DictationRecognizer_DictationComplete処理を行う
                dictationRecognizer.DictationError += DictationRecognizer_DictationError;//DictationRecognizer_DictationError処理を行う
            }
        }
        catch (System.Exception)
        {
            if (GManager.instance.ui_voice == 1)
            {
                GManager.instance.ui_voice = 0;
                PlayerPrefs.SetInt("ui_voice", GManager.instance.ui_voice);
            }
        }
    }

    void SetWordEvent()
    {
        //設定
        if ((GManager.instance.get_word == words[6] || GManager.instance.get_word == words[7] || GManager.instance.get_word == words[8] || GManager.instance.get_word == words[9] || GManager.instance.get_word == words[10] || GManager.instance.get_word == words[11] || GManager.instance.get_word == words[12]) && GManager.instance.setmenu <= 0)
        {
            GManager.instance.get_word = "";
            //wordevent = false;
            GManager.instance.setrg = 3;
            Instantiate(GManager.instance.all_ui[1], transform.position, transform.rotation);
            GManager.instance.setmenu = 1;
            GManager.instance.walktrg = false;
        }
        else if ((GManager.instance.get_word == words[13] || GManager.instance.get_word == words[14] || GManager.instance.get_word == words[15] || GManager.instance.get_word == words[16]) && GManager.instance.setmenu <= 0)
        {
            GManager.instance.get_word = "";
            //wordevent = false;
            GManager.instance.setrg = 3;
            Instantiate(GManager.instance.all_ui[3], transform.position, transform.rotation);
            GManager.instance.setmenu = 1;
            GManager.instance.walktrg = false;
        }
    }
}