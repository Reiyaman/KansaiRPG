using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkScript : MonoBehaviour
{
    public GameObject talkbox;
    public Text talkText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TalkWait()
    {
        Invoke("TalkStart", 0.5f);
    }

    public void TalkStart()
    {
        talkText.text = "きっしょい　モンスターが　あらわれよったで！";

    }

    public void Next()
    {
        talkText.text = "まだまだ　まけて ないでえ！";
    }
}
