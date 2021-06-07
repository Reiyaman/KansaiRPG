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
    public GameObject specialButton;
    public GameObject recoveryButton;

    public int special;
    public int recovery;

    public int playerAttackMinDamage; //Playerの攻撃力
    public int playerAttackMaxDamage;

    public int maxHP; //Playerスクリプトから引っ張ってきた戦う敵の最大HPを格納する変数
    public int currentHP; //Playerスクリプトから引っ張ってきた戦う敵の現在のHPを格納する変数

    TalkScript talkScript;



    // Start is called before the first frame update
    void Start()
    {
        talkScript = gameObject.GetComponent<TalkScript>();
        special = 0;
        recovery = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackButton() //Attackボタンを押した時に呼ぶ関数
    {
        attackButton.SetActive(false); //ボタンを消す
        specialButton.SetActive(false);
        recoveryButton.SetActive(false);

        int damage = Random.Range(player.GetComponent<LevelController>().playerAttackMinDamage, player.GetComponent<LevelController>().playerAttackMaxDamage); //攻撃のダメージを乱数で取得
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

    public void SpecialButton() //Specialボタンを押した時に呼ぶ関数
    {
        attackButton.SetActive(false);
        specialButton.SetActive(false); //ボタンを消す
        recoveryButton.SetActive(false);

        special = 0; //カウントリセット

        int damage = Random.Range(player.GetComponent<LevelController>().playerAttackMinDamage, player.GetComponent<LevelController>().playerAttackMaxDamage) * 3; //攻撃のダメージを乱数で取得
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

        Text special_text = talkScript.talkText;
        special_text.text = "くらえ！わいのスペシャル攻撃や！\n" + damage + "のダメージを与えたったわい！";

        if (currentHP > 0)
        {
            Invoke("DamageButton", 3.0f);
            //gameObject.SendMessage("DestroyEnemyWait");

        }
    }

    public void RecoveryButton()
    {
        attackButton.SetActive(false);
        specialButton.SetActive(false); //ボタンを消す
        recoveryButton.SetActive(false);

        recovery = 0;

        int recover = 1500;

        player.GetComponent<PlayerScript>().currentPlayerHP = player.GetComponent<PlayerScript>().currentPlayerHP + recover;
        player.GetComponent<PlayerScript>().playerSlider.value = (float)player.GetComponent<PlayerScript>().currentPlayerHP / (float)player.GetComponent<PlayerScript>().maxPlayerHP; //HPバーのゲージを増やす

        if (player.GetComponent<PlayerScript>().playerSlider.value > 0.75f) //回復すると色が赤→黄→緑に変化していく
        {
            player.GetComponent<PlayerScript>().playerSliderGauge.color = Color.Lerp(player.GetComponent<PlayerScript>().color_2, player.GetComponent<PlayerScript>().color_1, (player.GetComponent<PlayerScript>().playerSlider.value + 0.25f) * 4f);
        }

        else if (player.GetComponent<PlayerScript>().playerSlider.value > 0.25f)
        {
            player.GetComponent<PlayerScript>().playerSliderGauge.color = Color.Lerp(player.GetComponent<PlayerScript>().color_3, player.GetComponent<PlayerScript>().color_2, (player.GetComponent<PlayerScript>().playerSlider.value + 0.75f) * 4f);
        }
        else
        {
            player.GetComponent<PlayerScript>().playerSliderGauge.color = Color.Lerp(player.GetComponent<PlayerScript>().color_4, player.GetComponent<PlayerScript>().color_3, player.GetComponent<PlayerScript>().playerSlider.value * 4f);
        }

        Text recover_text = talkScript.talkText;
        recover_text.text = "おおおパワーがみなぎってきたで！\n" + "めっちゃ回復したで！";

        player.GetComponent<PlayerScript>().currentPlayerHP = player.GetComponent<PlayerScript>().maxPlayerHP;

        if (currentHP > 0)
            {
                Invoke("DamageButton", 1.5f);
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
