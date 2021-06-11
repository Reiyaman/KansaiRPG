using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class ResultController : MonoBehaviour
{
    public GameObject moveModeCamera; //移動中のカメラの変数
    public GameObject battleModeCamera; //バトル中のカメラの変数
    public GameObject gameClearCamera; //ゲームクリアのカメラの変数

    public GameObject gameClearPanel; //ゲームクリアのパネル
    public Text clearTime; //クリアタイム
    public Text clearMessage; //クリアタイムに対するメッセージ
    public GameObject gameClearText; //クリアテキスト
    public GameObject nextButton; //ネクストボタン

    float gameTimeSecond; //クリアするまでにかかる時間を計測する変数
    float gameTimeMinute;

   

    

    // Start is called before the first frame update
    void Start()
    {
        gameTimeSecond = 0;
        gameTimeMinute = 0;
    }

    // Update is called once per frame
    void Update()
    {
        gameTimeSecond += Time.deltaTime;

        if(gameTimeSecond > 60f)
        {
            gameTimeMinute++;
            gameTimeSecond = 0;
        }
    }

    public void ResultCoroutine()
    {
        Debug.Log("sss");
        StartCoroutine("Result");
    }

    private IEnumerator Result()
    {
        Debug.Log("aaa");

        yield return new WaitForSeconds(2.0f);

        gameClearText.SetActive(false); //ゲームクリアテキストを非表示

        moveModeCamera.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 1; //ゲームクリアカメラに切り替え
        battleModeCamera.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 1;
        gameClearCamera.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 100;

        yield return new WaitForSeconds(2.5f); //2.5秒待つ

        gameClearPanel.SetActive(true); //Resultのパネル表示

        yield return new WaitForSeconds(1.0f); //1秒待つ

        clearTime.text = "Clear Time : "; //クリアタイムは、、を表示
        clearTime.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.0f); //1秒待つ

        clearTime.text = "Clear Time   " + gameTimeMinute.ToString() + ":" + gameTimeSecond.ToString("f0"); //クリアタイムの表示
         
        yield return new WaitForSeconds(1.0f); //1秒待つ

        if(gameTimeMinute <= 5) //最速クリアなら
        {
            clearMessage.text = "Wonderful!!";
            clearMessage.gameObject.SetActive(true);
        }

        else if(gameTimeMinute <= 7) //そこそこいいタイムなら
        {
            clearMessage.text = "Congraturation!";
            clearMessage.gameObject.SetActive(true);
        }

        else //それ以外
        {
            clearMessage.text = "GOOD";
            clearMessage.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(2.0f);

        nextButton.SetActive(true);
       
    }

}
