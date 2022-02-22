using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkScript : MonoBehaviour
{

    //TalkboxのText取得して変更する
    public GameObject talkbox;
    public Text talkText;

    public string talkStart;
    public string talkNext;
    public string textDamage;
    public string textAttack;
    public string textSpecial;
    public string textRecover1;
    public string textRecover2;
    public string textRun;
    public string textNotRun;
    public string textWin;
    public string textLose;
    public string textGet1;
    public string textGet2;
    public string textGet3;
    public string textGet4;
    public string textClear1;
    public string textClear2;
    public string textClear3;
   
    public void TalkWait()
    {
        Invoke("TalkStart", 0.5f);
    }

    public void TalkStart()
    {
        talkText.text = talkStart;

    }

    public void Next()
    {
        talkText.text = talkNext;
    }

    
}
