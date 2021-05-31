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
        Invoke("TalkStart", 4f);
    }

    public void TalkStart()
    {
        talkText.text = "きっしょいモンスターが現れよったで！";

    }

    public void Next()
    {
        talkText.text = "まだまだ負けてないでえ！";
    }
}
