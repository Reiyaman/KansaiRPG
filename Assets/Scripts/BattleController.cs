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
    public GameObject battleEnemy; //戦う敵の変数

    public GameObject attackButton;

    public int maxHP; //Playerスクリプトから引っ張ってきた戦う敵の最大HPを格納する変数
    public int currentHP; //Playerスクリプトから引っ張ってきた戦う敵の現在のHPを格納する変数

    TalkScript talkScript;



    // Start is called before the first frame update
    void Start()
    {
        talkScript = gameObject.GetComponent<TalkScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackButton() //Attackボタンを押した時に呼ぶ関数
    {
        attackButton.SetActive(false); //ボタンを消す

        int damage = Random.Range(10000, 20000); //攻撃のダメージを乱数で取得
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

        Text attack_text = talkScript.talkText;
        attack_text.text = damage + "のダメージを与えたったわい！";

        if(currentHP > 0)
        {
            Invoke("DamageButton", 3.0f);
            //gameObject.SendMessage("DestroyEnemyWait");

        }
    }

    public void DamageButton()
    {
        player.GetComponent<PlayerScript>().PlayerDamage();
       // player.gameObject.SendMessage("PlayerDamage");
    }

    public void BattleStart()
    {
        
        maxHP = player.gameObject.GetComponent<PlayerScript>().eHP;
        currentHP = player.gameObject.GetComponent<PlayerScript>().cHP;
       // gameObject.SendMessage("ChangeBattleMode");
    }
}
