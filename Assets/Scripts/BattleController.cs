using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public Slider enemyslider; //敵の体力ゲージ

    public Image enemySliderGauge; //スライダーの色の変数
    public Color color_1, color_2, color_3, color_4; // カラーの変数

    public GameObject player; //PlayerScriptから引っ張ってきたCollisionの情報を格納するための変数

    int i = 1; 
    int maxHP; //Playerスクリプトから引っ張ってきた戦う敵の最大HPを格納する変数
    int currentHP; //Playerスクリプトから引っ張ってきた戦う敵の現在のHPを格納する変数

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackButton() //Attackボタンを押した時に呼ぶ関数
    {
        if(i == 1)　//最初の一回だけ呼び出したい
        {
            maxHP = player.gameObject.GetComponent<PlayerScript>().eHP; 
            currentHP = player.gameObject.GetComponent<PlayerScript>().cHP; 
        }
        int damage = Random.Range(100, 200); //攻撃のダメージを乱数で取得
        Debug.Log("damage : " + damage);

        currentHP = currentHP - damage; //最新のHPを取得
        Debug.Log("After current : " + currentHP);

        enemyslider.value = (float)currentHP / (float)maxHP; //HPバーのゲージを減らす
        Debug.Log("slider.value : " + enemyslider.value);

        if (enemyslider.value > 0.75f) //段々と色が緑→黄→赤に変化していく
        {
            enemySliderGauge.color = Color.Lerp(color_2, color_1, (enemyslider.value - 0.75f) * 4f);
        }

        else if (enemyslider.value > 0.25f)
        {
            enemySliderGauge.color = Color.Lerp(color_3, color_2, (enemyslider.value - 0.25f) * 4f);
        }
        else
        {
            enemySliderGauge.color = Color.Lerp(color_4, color_3, enemyslider.value * 4f);
        }

        i++;

        Invoke("DamageButton", 2f);
    }

    public void DamageButton()
    {
        player.gameObject.SendMessage("DamagePlayer");
    }
}
