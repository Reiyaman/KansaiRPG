using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public int level;　//Playerのレベルの変数
    public int maxPlayerHP; //Playerの最大HP
    public int currentPlayerHP; //Playerの現在のHP
    public int playerExp; //経験値の変数
    public int playermaxEXP; //レベルアップに必要な経験値
    public GameObject gameMaster; //ゲームを管理するゲームオブジェクト

    public int playerAttackMinDamage; //Playerの攻撃力
    public int playerAttackMaxDamage;

    public GameObject levelUpEffect; //レベルアップエフェクト
    public GameObject expGauge; //経験値ゲージ
    public Text levelText; //Playerのレベルの表示

    public AudioClip levelUpSE;

    AudioSource audioSource;

    public PlayerBase[] levelBase; //レベルごとのテーブル
    //public PlayerBase playerBase;
    public PlayerStatusController playerStatus { get; set; }
    public PlayerStatusController levelUpStatus { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        level = 1; //Playerの初期能力
        playerStatus = new PlayerStatusController(levelBase[level - 1]);
        maxPlayerHP = playerStatus.MaxHP;
        playerExp = 0;
        playermaxEXP = playerStatus.MaxExp;
        playerAttackMinDamage = playerStatus.AttackMinDamage;
        playerAttackMaxDamage = playerStatus.AttackMaxDamage;
        levelText.text = playerStatus.Level;
        expGauge.GetComponent<Image>().fillAmount = 0;
        audioSource = GetComponent<AudioSource>();

    }

    public void levelUpWait()
    {
        StartCoroutine("levelUp");
    }


    private IEnumerator levelUp()
    {
        playerExp += gameMaster.GetComponent<BattleMotionController>().exp; //経験値を加算
        expGauge.GetComponent<Image>().fillAmount = (float)playerExp / (float)playermaxEXP; //経験値ゲージ増やす

        if(playerExp >=  playermaxEXP && levelText.text == playerStatus.Level)
        {
            yield return new WaitForSeconds(1.5f);

            LevelUpStatus();
        }

    }

    public void LevelUpEffectNotActive()
    {
        levelUpEffect.SetActive(false);
    }

    public void LevelUpStatus()
    {
        level++; 
        playerStatus = new PlayerStatusController(levelBase[level - 1]);
        maxPlayerHP = currentPlayerHP = playerStatus.MaxHP;
        playermaxEXP = playerStatus.MaxExp;
        playerAttackMinDamage = playerStatus.AttackMinDamage;
        playerAttackMaxDamage = playerStatus.AttackMaxDamage;
        levelText.text = playerStatus.Level;

        gameObject.GetComponent<PlayerScript>().playerHPText.text = currentPlayerHP + "/" + maxPlayerHP; //更新
        gameObject.GetComponent<PlayerScript>().playerSliderGauge.fillAmount = 1;
        gameObject.GetComponent<PlayerScript>().playerSliderGauge.color = gameObject.GetComponent<PlayerScript>().color_1;

        playerExp = playerExp - playermaxEXP;
        expGauge.GetComponent<Image>().fillAmount = (float)playerExp / (float)playermaxEXP;


        levelUpEffect.SetActive(true);
        Invoke("LevelUpEffectNotActive", 1.8f);
        audioSource.PlayOneShot(levelUpSE);

        gameMaster.GetComponent<BattleMotionController>().victory_text.text = playerStatus.LevelUpText1 + "\n" + playerStatus.LevelUpText2;
    }
}
