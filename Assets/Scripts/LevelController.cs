using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public int level;　//Playerのレベルの変数
    public int playerExp; //経験値の変数
    public int playermaxEXP;
    public GameObject gameMaster; //

    public int playerAttackMinDamage; //Playerの攻撃力
    public int playerAttackMaxDamage;

    public GameObject levelUpEffect; //レベルアップエフェクト
    public GameObject expGauge; //経験値ゲージ
    public Text levelText; //Playerのレベルの表示

    public AudioClip levelUpSE;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        level = 1; //Playerの初期能力
        playerExp = 0;
        playermaxEXP = 300;
        playerAttackMinDamage =100;
        playerAttackMaxDamage = 150;
        expGauge.GetComponent<Image>().fillAmount = 0;

        audioSource = GetComponent<AudioSource>();

        levelText.text = "レベル１"; 

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void levelUpWait()
    {
        StartCoroutine("levelUp");
    }
    private IEnumerator levelUp()
    {
        playerExp += gameMaster.GetComponent<BattleMotionController>().exp; //経験値を加算
        expGauge.GetComponent<Image>().fillAmount = (float)playerExp / (float)playermaxEXP; //経験値ゲージ増やす

        //yield return new WaitForSeconds(2f);

        if(playerExp >= 300 && level == 1) 
        {
            yield return new WaitForSeconds(1.5f);

            level = 2; //レベルアップ
            levelText.text = "レベル2";

            playerAttackMinDamage = 125;
            playerAttackMaxDamage = 175;

            gameObject.GetComponent<PlayerScript>().maxPlayerHP = 650;
            gameObject.GetComponent<PlayerScript>().currentPlayerHP = gameObject.GetComponent<PlayerScript>().maxPlayerHP;
            gameObject.GetComponent<PlayerScript>().playerHPText.text = gameObject.GetComponent<PlayerScript>().currentPlayerHP + "/" + gameObject.GetComponent<PlayerScript>().maxPlayerHP; //更新

            gameObject.GetComponent<PlayerScript>().playerSliderGauge.fillAmount = 1;
            gameObject.GetComponent<PlayerScript>().playerSliderGauge.color = gameObject.GetComponent<PlayerScript>().color_1;


            playermaxEXP = 500;
            playerExp = playerExp - playermaxEXP;
            expGauge.GetComponent<Image>().fillAmount = (float)playerExp / (float)playermaxEXP;


            levelUpEffect.SetActive(true);
            Invoke("LevelUpEffectNotActive", 1.8f);
            audioSource.PlayOneShot(levelUpSE);

            gameMaster.GetComponent<BattleMotionController>().victory_text.text = "レベル２に　アップしたで!\n" + "もっと　つよく　なろうな！";

        }

        if(playerExp >= 500 && level == 2)
        {
            yield return new WaitForSeconds(1.5f);

            level = 3;//レベルアップ
            levelText.text = "レベル3";

            playerAttackMinDamage = 200;
            playerAttackMaxDamage = 275;

            gameMaster.GetComponent<BattleController>().recover = 800; 
            
            gameObject.GetComponent<PlayerScript>().maxPlayerHP = 800;
            gameObject.GetComponent<PlayerScript>().currentPlayerHP = gameObject.GetComponent<PlayerScript>().maxPlayerHP;
            gameObject.GetComponent<PlayerScript>().playerHPText.text = gameObject.GetComponent<PlayerScript>().currentPlayerHP + "/" + gameObject.GetComponent<PlayerScript>().maxPlayerHP; //更新

            gameObject.GetComponent<PlayerScript>().playerSliderGauge.fillAmount = 1;
            gameObject.GetComponent<PlayerScript>().playerSliderGauge.color = gameObject.GetComponent<PlayerScript>().color_1;

            playermaxEXP = 700;
            playerExp = playerExp - playermaxEXP;
            expGauge.GetComponent<Image>().fillAmount = (float)playerExp / (float)playermaxEXP;

            

            levelUpEffect.SetActive(true);
            Invoke("LevelUpEffectNotActive", 1.8f);
            audioSource.PlayOneShot(levelUpSE);


            gameMaster.GetComponent<BattleMotionController>().victory_text.text = "レベル3に　アップしたで!\n" + "リカバリーを　しゅうとく　したで！";

            
        }

        if(playerExp >= 700 && level == 3)
        {
            yield return new WaitForSeconds(1.5f);

            level = 4;//レベルアップ
            levelText.text = "レベル4";

            playerAttackMinDamage = 250;
            playerAttackMaxDamage = 300;

            gameObject.GetComponent<PlayerScript>().maxPlayerHP = 1000;
            gameObject.GetComponent<PlayerScript>().currentPlayerHP = gameObject.GetComponent<PlayerScript>().maxPlayerHP;
            gameObject.GetComponent<PlayerScript>().playerHPText.text = gameObject.GetComponent<PlayerScript>().currentPlayerHP + "/" + gameObject.GetComponent<PlayerScript>().maxPlayerHP; //更新

            gameObject.GetComponent<PlayerScript>().playerSliderGauge.fillAmount = 1;
            gameObject.GetComponent<PlayerScript>().playerSliderGauge.color = gameObject.GetComponent<PlayerScript>().color_1;

            playermaxEXP = 900;
            playerExp = playerExp - playermaxEXP;
            expGauge.GetComponent<Image>().fillAmount = (float)playerExp / (float)playermaxEXP;

            

            levelUpEffect.SetActive(true);
            Invoke("LevelUpEffectNotActive", 1.8f);
            audioSource.PlayOneShot(levelUpSE);


            gameMaster.GetComponent<BattleMotionController>().victory_text.text = "レベル4に　アップしたで!\n" + "もっと　つよく　なろうな！";

            
        }

        if(playerExp >= 900 && level == 4)
        {
            yield return new WaitForSeconds(1.5f);

            level = 5;//レベルアップ
            levelText.text = "レベル5";

            playerAttackMinDamage = 350;
            playerAttackMaxDamage = 400;

            gameMaster.GetComponent<BattleController>().specialrange = 2;

            gameObject.GetComponent<PlayerScript>().maxPlayerHP = 1200;
            gameObject.GetComponent<PlayerScript>().currentPlayerHP = gameObject.GetComponent<PlayerScript>().maxPlayerHP;
            gameObject.GetComponent<PlayerScript>().playerHPText.text = gameObject.GetComponent<PlayerScript>().currentPlayerHP + "/" + gameObject.GetComponent<PlayerScript>().maxPlayerHP; //更新

            gameObject.GetComponent<PlayerScript>().playerSliderGauge.fillAmount = 1;
            gameObject.GetComponent<PlayerScript>().playerSliderGauge.color = gameObject.GetComponent<PlayerScript>().color_1;

            playermaxEXP = 1000;
            playerExp = playerExp - playermaxEXP;
            expGauge.GetComponent<Image>().fillAmount = (float)playerExp / (float)playermaxEXP;

           

            levelUpEffect.SetActive(true);
            Invoke("LevelUpEffectNotActive", 1.8f);
            audioSource.PlayOneShot(levelUpSE);


            gameMaster.GetComponent<BattleMotionController>().victory_text.text = "レベル5に　アップしたで!\n" + "ひっさつわざを　しゅうとく　したで！";

        }

        if(playerExp >= 1000 && level == 5)
        {
            yield return new WaitForSeconds(1.5f);

            level = 6;//レベルアップ
            levelText.text = "レベル6";

            playerAttackMinDamage = 400;
            playerAttackMaxDamage = 450;

            gameObject.GetComponent<PlayerScript>().maxPlayerHP = 1400;
            gameObject.GetComponent<PlayerScript>().currentPlayerHP = gameObject.GetComponent<PlayerScript>().maxPlayerHP;
            gameObject.GetComponent<PlayerScript>().playerHPText.text = gameObject.GetComponent<PlayerScript>().currentPlayerHP + "/" + gameObject.GetComponent<PlayerScript>().maxPlayerHP; //更新

            gameObject.GetComponent<PlayerScript>().playerSliderGauge.fillAmount = 1;
            gameObject.GetComponent<PlayerScript>().playerSliderGauge.color = gameObject.GetComponent<PlayerScript>().color_1;

            playermaxEXP = 1100;
            playerExp = playerExp - playermaxEXP;
            expGauge.GetComponent<Image>().fillAmount = (float)playerExp / (float)playermaxEXP;


           

            levelUpEffect.SetActive(true);
            Invoke("LevelUpEffectNotActive", 1.8f);
            audioSource.PlayOneShot(levelUpSE);


            gameMaster.GetComponent<BattleMotionController>().victory_text.text = "レベル6に アップしたで!\n" + "もっと  つよく　なろうな！";

            
        }

        if(playerExp >= 1100 && level == 6)
        {
            yield return new WaitForSeconds(1.5f);

            level = 7;//レベルアップ
            levelText.text = "レベル7";

            playerAttackMinDamage = 500;
            playerAttackMaxDamage = 600;

            gameMaster.GetComponent<BattleController>().recover = 1400; //回復力アップ

            gameObject.GetComponent<PlayerScript>().maxPlayerHP = 1700;
            gameObject.GetComponent<PlayerScript>().currentPlayerHP = gameObject.GetComponent<PlayerScript>().maxPlayerHP;
            gameObject.GetComponent<PlayerScript>().playerHPText.text = gameObject.GetComponent<PlayerScript>().currentPlayerHP + "/" + gameObject.GetComponent<PlayerScript>().maxPlayerHP; //更新

            gameObject.GetComponent<PlayerScript>().playerSliderGauge.fillAmount = 1;
            gameObject.GetComponent<PlayerScript>().playerSliderGauge.color = gameObject.GetComponent<PlayerScript>().color_1;

            playermaxEXP = 1200;
            playerExp = playerExp - playermaxEXP;
            expGauge.GetComponent<Image>().fillAmount = (float)playerExp / (float)playermaxEXP;


            

            levelUpEffect.SetActive(true);
            Invoke("LevelUpEffectNotActive", 1.8f);
            audioSource.PlayOneShot(levelUpSE);


            gameMaster.GetComponent<BattleMotionController>().victory_text.text = "レベル7に　アップしたで!\n" + "リカバリーの　いりょくが　つよく　なったで！";

        }

        if(playerExp >= 1200 && level == 7)
        {
            yield return new WaitForSeconds(1.5f);

            level = 8;//レベルアップ
            levelText.text = "レベル8";

            playerAttackMinDamage = 600;
            playerAttackMaxDamage = 700;

            gameObject.GetComponent<PlayerScript>().maxPlayerHP = 2000;
            gameObject.GetComponent<PlayerScript>().currentPlayerHP = gameObject.GetComponent<PlayerScript>().maxPlayerHP;
            gameObject.GetComponent<PlayerScript>().playerHPText.text = gameObject.GetComponent<PlayerScript>().currentPlayerHP + "/" + gameObject.GetComponent<PlayerScript>().maxPlayerHP; //更新

            gameObject.GetComponent<PlayerScript>().playerSliderGauge.fillAmount = 1;
            gameObject.GetComponent<PlayerScript>().playerSliderGauge.color = gameObject.GetComponent<PlayerScript>().color_1;

            playermaxEXP = 1300;
            playerExp = playerExp - playermaxEXP;
            expGauge.GetComponent<Image>().fillAmount = (float)playerExp / (float)playermaxEXP;


            

            levelUpEffect.SetActive(true);
            Invoke("LevelUpEffectNotActive", 1.8f);
            audioSource.PlayOneShot(levelUpSE);


            gameMaster.GetComponent<BattleMotionController>().victory_text.text = "レベル8に　アップしたで!\n" + "もっと　つよく　なろうな！";

        }

        if(playerExp >= 1300 && level == 8)
        {
            yield return new WaitForSeconds(1.5f);

            level = 9;//レベルアップ
            levelText.text = "レベル9";

            playerAttackMinDamage = 700;
            playerAttackMaxDamage = 800;

            gameMaster.GetComponent<BattleController>().specialrange = 3;　//必殺技威力アップ

            gameObject.GetComponent<PlayerScript>().maxPlayerHP = 2200;
            gameObject.GetComponent<PlayerScript>().currentPlayerHP = gameObject.GetComponent<PlayerScript>().maxPlayerHP;
            gameObject.GetComponent<PlayerScript>().playerHPText.text = gameObject.GetComponent<PlayerScript>().currentPlayerHP + "/" + gameObject.GetComponent<PlayerScript>().maxPlayerHP; //更新

            gameObject.GetComponent<PlayerScript>().playerSliderGauge.fillAmount = 1;
            gameObject.GetComponent<PlayerScript>().playerSliderGauge.color = gameObject.GetComponent<PlayerScript>().color_1;

            playermaxEXP = 1500;
            playerExp = playerExp - playermaxEXP;
            expGauge.GetComponent<Image>().fillAmount = (float)playerExp / (float)playermaxEXP;


            

            levelUpEffect.SetActive(true);
            Invoke("LevelUpEffectNotActive", 1.8f);
            audioSource.PlayOneShot(levelUpSE);

            gameMaster.GetComponent<BattleMotionController>().victory_text.text = "レベル9に　アップしたで!\n" + "ひっさつわざが　さらに　つよく　なったで！";

        }

        if(playerExp >= 1500 && level == 9)
        {
            yield return new WaitForSeconds(1.5f);

            level = 10;//レベルアップ
            levelText.text = "レベル10";

            playerAttackMinDamage = 1000;
            playerAttackMaxDamage = 1100;

            gameMaster.GetComponent<BattleController>().recover = 2000; //回復威力アップ
            gameMaster.GetComponent<BattleController>().specialrange = 4; //必殺技威力アップ

            gameObject.GetComponent<PlayerScript>().maxPlayerHP = 2500;
            gameObject.GetComponent<PlayerScript>().currentPlayerHP = gameObject.GetComponent<PlayerScript>().maxPlayerHP;
            gameObject.GetComponent<PlayerScript>().playerHPText.text = gameObject.GetComponent<PlayerScript>().currentPlayerHP + "/" + gameObject.GetComponent<PlayerScript>().maxPlayerHP; //更新

            gameObject.GetComponent<PlayerScript>().playerSliderGauge.fillAmount = 1;
            gameObject.GetComponent<PlayerScript>().playerSliderGauge.color = gameObject.GetComponent<PlayerScript>().color_1;


            expGauge.GetComponent<Image>().fillAmount = 1;　//最大レベルなので満タン



            levelUpEffect.SetActive(true);
            Invoke("LevelUpEffectNotActive", 1.8f);
            audioSource.PlayOneShot(levelUpSE);


            gameMaster.GetComponent<BattleMotionController>().victory_text.text = "レベル10　にアップしたで!\n" + "きみは　いま　さいきょうや！";

          
        }
    }

    public void LevelUpEffectNotActive()
    {
        levelUpEffect.SetActive(false);
    }
}
