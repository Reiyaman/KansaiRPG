using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public GameObject enemyslider; //敵の体力ゲージ

    public Image enemySliderGauge; //スライダーの色の変数
    public Color color_1, color_2, color_3, color_4; // カラーの変数

    public GameObject player; //PlayerScriptから引っ張ってきたCollisionの情報を格納するための変数
    public GameObject battleEnemy; //戦う敵の変数

    public GameObject attackButton;
    public GameObject specialButton;
    public GameObject recoveryButton;
    public GameObject escapeButton;

    public GameObject healEffect; //ヒールエフェクト

    public int special;　//スペシャルが使えるまでのターン数
    public int recovery; //リカバリーが使えるまでのターン数

    public int specialrange; //必殺技の倍率
    public int recover; //回復力
    public int escapeNumber;

    [SerializeField] Text enemyHPText;　//敵のHP
    public Text playerHPText;　//自分のHP
    [SerializeField] Text enemyLevelText;　//敵のレベル
    public GameObject enemyImage;
    public Image image;

    public int playerAttackMinDamage; //Playerの攻撃力
    public int playerAttackMaxDamage;

    public int maxHP; //Playerスクリプトから引っ張ってきた戦う敵の最大HPを格納する変数
    public int currentHP; //Playerスクリプトから引っ張ってきた戦う敵の現在のHPを格納する変数

    public TalkScript talkScript;

    public EnemyBase _base;
    public EnemyStatusController enemyStatus { get; set; }

    //SerializeField] EnemyBase _base;


    // Start is called before the first frame update
    void Start()
    {
        talkScript = gameObject.GetComponent<TalkScript>();
        image = enemyImage.GetComponent<Image>();
        special = 0;
        recovery = 0;

        //enemyHPText.text = maxHP + "/" + maxHP;

        enemySliderGauge = enemyslider.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackButton() //Attackボタンを押した時に呼ぶ関数
    {
        gameObject.GetComponent<ModeController>().wait = false;

        attackButton.SetActive(false); //ボタンを消す
        specialButton.SetActive(false);
        recoveryButton.SetActive(false);
        escapeButton.SetActive(false);

        int damage = Random.Range(player.GetComponent<LevelController>().playerAttackMinDamage, player.GetComponent<LevelController>().playerAttackMaxDamage); //攻撃のダメージを乱数で取得
        Debug.Log("damage : " + damage);

        currentHP = currentHP - damage; //最新のHPを取得
        Debug.Log("After current : " + currentHP);
        
        if (currentHP <= 0) //０以下は０と表示
        {
            currentHP = 0;
        }

        enemyHPText.text = currentHP + "/" + maxHP;

        enemySliderGauge.fillAmount = (float)currentHP / (float)maxHP; //HPバーのゲージを減らす
        Debug.Log("slider.value : " + enemySliderGauge.fillAmount);

        if (enemySliderGauge.fillAmount > 0.75f) //段々と色が緑→黄→赤に変化していく
        {
            enemySliderGauge.color = Color.Lerp(color_2, color_1, (enemySliderGauge.fillAmount - 0.75f) * 4f);
        }

        else if (enemySliderGauge.fillAmount > 0.25f)
        {
            enemySliderGauge.color = Color.Lerp(color_3, color_2, (enemySliderGauge.fillAmount - 0.25f) * 4f);
        }
        else
        {
            enemySliderGauge.color = Color.Lerp(color_4, color_3, enemySliderGauge.fillAmount * 4f);
        }
        
        talkScript.talkText.text = damage + talkScript.textAttack;

        if(currentHP > 0)
        {
            Invoke("DamageButton", 3.0f);
            //gameObject.SendMessage("DestroyEnemyWait");

        }
    }

    public void SpecialButton() //Specialボタンを押した時に呼ぶ関数
    {
        gameObject.GetComponent<ModeController>().wait = false;

        attackButton.SetActive(false); //ボタンを消す
        specialButton.SetActive(false);
        recoveryButton.SetActive(false);
        escapeButton.SetActive(false);

        special = 0; //カウントリセット

        int damage = Random.Range(player.GetComponent<LevelController>().playerAttackMinDamage, player.GetComponent<LevelController>().playerAttackMaxDamage) * specialrange; //攻撃のダメージを乱数で取得
        Debug.Log("damage : " + damage);

        currentHP = currentHP - damage; //最新のHPを取得
        Debug.Log("After current : " + currentHP);
        
        if (currentHP <= 0) //０以下は０と表示
        {
            currentHP = 0;
        }

        enemyHPText.text = currentHP + "/" + maxHP;

        enemySliderGauge.fillAmount = (float)currentHP / (float)maxHP; //HPバーのゲージを減らす
        Debug.Log("slider.value : " + enemySliderGauge.fillAmount);

        if (enemySliderGauge.fillAmount > 0.75f) //段々と色が緑→黄→赤に変化していく
        {
            enemySliderGauge.color = Color.Lerp(color_2, color_1, (enemySliderGauge.fillAmount - 0.75f) * 4f);
        }

        else if (enemySliderGauge.fillAmount > 0.25f)
        {
            enemySliderGauge.color = Color.Lerp(color_3, color_2, (enemySliderGauge.fillAmount - 0.25f) * 4f);
        }
        else
        {
            enemySliderGauge.color = Color.Lerp(color_4, color_3, enemySliderGauge.fillAmount * 4f);
        }

        Text special_text = talkScript.talkText;
        special_text.text =  talkScript.textSpecial + "\n" + damage + talkScript.textAttack;

        if (currentHP > 0)
        {
            Invoke("DamageButton", 3.0f);
            //gameObject.SendMessage("DestroyEnemyWait");

        }
    }

    public void RecoveryButton()
    {
        gameObject.GetComponent<ModeController>().wait = false;

        attackButton.SetActive(false); //ボタンを消す
        specialButton.SetActive(false);
        recoveryButton.SetActive(false);
        escapeButton.SetActive(false);

        recovery = 0;

        

        healEffect.SetActive(true);

        player.GetComponent<PlayerScript>().currentPlayerHP = player.GetComponent<PlayerScript>().currentPlayerHP + recover;

        

        playerHPText.text = player.GetComponent<PlayerScript>().currentPlayerHP + "/" + player.GetComponent<PlayerScript>().maxPlayerHP;

        player.GetComponent<PlayerScript>().playerSliderGauge.fillAmount = (float)player.GetComponent<PlayerScript>().currentPlayerHP / (float)player.GetComponent<PlayerScript>().maxPlayerHP; //HPバーのゲージを増やす

        if (player.GetComponent<PlayerScript>().playerSliderGauge.fillAmount > 0.75f) //回復すると色が赤→黄→緑に変化していく
        {
            player.GetComponent<PlayerScript>().playerSliderGauge.color = Color.Lerp(player.GetComponent<PlayerScript>().color_2, player.GetComponent<PlayerScript>().color_1, (player.GetComponent<PlayerScript>().playerSliderGauge.fillAmount + 0.25f) * 4f);
        }

        else if (player.GetComponent<PlayerScript>().playerSliderGauge.fillAmount > 0.25f)
        {
            player.GetComponent<PlayerScript>().playerSliderGauge.color = Color.Lerp(player.GetComponent<PlayerScript>().color_3, player.GetComponent<PlayerScript>().color_2, (player.GetComponent<PlayerScript>().playerSliderGauge.fillAmount + 0.75f) * 4f);
        }
        else
        {
            player.GetComponent<PlayerScript>().playerSliderGauge.color = Color.Lerp(player.GetComponent<PlayerScript>().color_4, player.GetComponent<PlayerScript>().color_3, player.GetComponent<PlayerScript>().playerSliderGauge.fillAmount * 4f);
        }

        healEffect.SetActive(true);

        if (player.GetComponent<PlayerScript>().currentPlayerHP >= player.GetComponent<PlayerScript>().maxPlayerHP) //回復後のHPがマックス超えたらマックスに制限する
        {
            player.GetComponent<PlayerScript>().currentPlayerHP = player.GetComponent<PlayerScript>().maxPlayerHP;
        }

        playerHPText.text = player.GetComponent<PlayerScript>().currentPlayerHP + "/" + player.GetComponent<PlayerScript>().maxPlayerHP;

        Text recover_text = talkScript.talkText;
        recover_text.text = talkScript.textRecover1 + "\n" + talkScript.textRecover2;

        player.GetComponent<PlayerScript>().currentPlayerHP = player.GetComponent<PlayerScript>().maxPlayerHP;

        Invoke("healEffectNotActive", 1.5f);
        Invoke("DamageButton", 1.5f);
        //gameObject.SendMessage("DestroyEnemyWait");
 
    }


    public void EscapeButton()
    {
        gameObject.GetComponent<ModeController>().wait = false;

        attackButton.SetActive(false); //ボタンを消す
        specialButton.SetActive(false);
        recoveryButton.SetActive(false);
        escapeButton.SetActive(false);

        escapeNumber = Random.Range(1, 3); //逃げる確率2分の１

        if(escapeNumber == 1) //もし逃げれるなら
        {
            talkScript.talkText.text = talkScript.textRun;

        }

        else  //もし無理なら
        {
            talkScript.talkText.text = talkScript.textNotRun;

            Invoke("DamageButton", 1.5f);
        }
       
    }


    public void DamageButton()　//Player受ける
    {
        player.GetComponent<PlayerScript>().PlayerDamage();
       // player.gameObject.SendMessage("PlayerDamage");
    }
    
    public void BattleStart() //バトル相手の情報を代入
    {
        _base = player.gameObject.GetComponent<PlayerScript>().enemyBase;
        //_base = battleEnemy.GetComponent<EnemyController>().enemyBase;
        enemyStatus = new EnemyStatusController(_base);
        maxHP = currentHP = enemyStatus.MaxHP;
        enemyHPText.text = maxHP + "/" + maxHP;
        image.sprite = _base.Sprite;
        enemyLevelText.text = enemyStatus.Level;
        enemyLevelText.gameObject.SetActive(true);


    }

    public void healEffectNotActive()
    {
        healEffect.SetActive(false);
    }

}
