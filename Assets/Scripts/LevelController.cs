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

    public AudioClip levelUpSE;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        level = 1; //Playerの初期能力
        playerExp = 0;
        playermaxEXP = 300;
        playerAttackMinDamage = 200;
        playerAttackMaxDamage = 300;
        expGauge.GetComponent<Image>().fillAmount = 0;

        audioSource = GetComponent<AudioSource>();
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

        yield return new WaitForSeconds(2f);

        if(playerExp >= 300 && level == 1) 
        {
            level = 2; //レベルアップ
            playerAttackMinDamage = 150;
            playerAttackMaxDamage = 300;

            gameObject.GetComponent<PlayerScript>().maxPlayerHP = 500; 
            gameObject.GetComponent<PlayerScript>().playerHPText.text = gameObject.GetComponent<PlayerScript>().currentPlayerHP + "/" + gameObject.GetComponent<PlayerScript>().maxPlayerHP; //更新

            playermaxEXP = 500;
            playerExp = playerExp - playermaxEXP;
            expGauge.GetComponent<Image>().fillAmount = (float)playerExp / (float)playermaxEXP;

            yield return new WaitForSeconds(1.5f);

            levelUpEffect.SetActive(true);
            Invoke("LevelUpEffectNotActive", 1.8f);
            audioSource.PlayOneShot(levelUpSE);

            gameMaster.GetComponent<BattleMotionController>().victory_text.text = "レベル２にアップしたで!\n" + "もっと強くなろうな！";

        }

        if(playerExp >= 500 && level == 2)
        {
            level = 3;//レベルアップ
            playerAttackMinDamage = 300;
            playerAttackMaxDamage = 450;

            gameObject.GetComponent<PlayerScript>().maxPlayerHP = 800;
            gameObject.GetComponent<PlayerScript>().playerHPText.text = gameObject.GetComponent<PlayerScript>().currentPlayerHP + "/" + gameObject.GetComponent<PlayerScript>().maxPlayerHP; //更新

            playermaxEXP = 1000;
            playerExp = playerExp - playermaxEXP;
            expGauge.GetComponent<Image>().fillAmount = (float)playerExp / (float)playermaxEXP;

            yield return new WaitForSeconds(1.5f);

            levelUpEffect.SetActive(true);
            Invoke("LevelUpEffectNotActive", 1.8f);
            audioSource.PlayOneShot(levelUpSE);


            gameMaster.GetComponent<BattleMotionController>().victory_text.text = "レベル3にアップしたで!\n" + "リカバリーを習得したで！";

            
        }

        if(playerExp >= 1000 && level == 3)
        {
            level = 4;//レベルアップ
            playerAttackMinDamage = 450;
            playerAttackMaxDamage = 600;

            gameObject.GetComponent<PlayerScript>().maxPlayerHP = 1000;
            gameObject.GetComponent<PlayerScript>().playerHPText.text = gameObject.GetComponent<PlayerScript>().currentPlayerHP + "/" + gameObject.GetComponent<PlayerScript>().maxPlayerHP; //更新

            playermaxEXP = 2500;
            playerExp = playerExp - playermaxEXP;
            expGauge.GetComponent<Image>().fillAmount = (float)playerExp / (float)playermaxEXP;

            yield return new WaitForSeconds(1.5f);

            levelUpEffect.SetActive(true);
            Invoke("LevelUpEffectNotActive", 1.8f);
            audioSource.PlayOneShot(levelUpSE);


            gameMaster.GetComponent<BattleMotionController>().victory_text.text = "レベル4にアップしたで!\n" + "もっと強くなろうな！";

            
        }

        if(playerExp >= 2500 && level == 4)
        {
            level = 5;//レベルアップ
            playerAttackMinDamage = 800;
            playerAttackMaxDamage = 900;

            gameObject.GetComponent<PlayerScript>().maxPlayerHP = 1300;
            gameObject.GetComponent<PlayerScript>().playerHPText.text = gameObject.GetComponent<PlayerScript>().currentPlayerHP + "/" + gameObject.GetComponent<PlayerScript>().maxPlayerHP; //更新

            playermaxEXP = 5000;
            playerExp = playerExp - playermaxEXP;
            expGauge.GetComponent<Image>().fillAmount = (float)playerExp / (float)playermaxEXP;

            yield return new WaitForSeconds(1.5f);

            levelUpEffect.SetActive(true);
            Invoke("LevelUpEffectNotActive", 1.8f);
            audioSource.PlayOneShot(levelUpSE);


            gameMaster.GetComponent<BattleMotionController>().victory_text.text = "レベル5にアップしたで!\n" + "必殺技を習得したで！";

        }

        if(playerExp >= 5000 && level == 5)
        {
            level = 6;//レベルアップ
            playerAttackMinDamage = 1000;
            playerAttackMaxDamage = 1200;

            gameObject.GetComponent<PlayerScript>().maxPlayerHP = 1600;
            gameObject.GetComponent<PlayerScript>().playerHPText.text = gameObject.GetComponent<PlayerScript>().currentPlayerHP + "/" + gameObject.GetComponent<PlayerScript>().maxPlayerHP; //更新

            playermaxEXP = 10000;
            playerExp = playerExp - playermaxEXP;
            expGauge.GetComponent<Image>().fillAmount = (float)playerExp / (float)playermaxEXP;


            yield return new WaitForSeconds(1.5f);

            levelUpEffect.SetActive(true);
            Invoke("LevelUpEffectNotActive", 1.8f);
            audioSource.PlayOneShot(levelUpSE);


            gameMaster.GetComponent<BattleMotionController>().victory_text.text = "レベル6にアップしたで!\n" + "もっと強くなろうな！";

            
        }

        if(playerExp >= 10000 && level == 6)
        {
            level = 7;//レベルアップ
            playerAttackMinDamage = 1200;
            playerAttackMaxDamage = 1400;

            gameObject.GetComponent<PlayerScript>().maxPlayerHP = 2000;
            gameObject.GetComponent<PlayerScript>().playerHPText.text = gameObject.GetComponent<PlayerScript>().currentPlayerHP + "/" + gameObject.GetComponent<PlayerScript>().maxPlayerHP; //更新

            playermaxEXP = 20000;
            playerExp = playerExp - playermaxEXP;
            expGauge.GetComponent<Image>().fillAmount = (float)playerExp / (float)playermaxEXP;


            yield return new WaitForSeconds(1.5f);

            levelUpEffect.SetActive(true);
            Invoke("LevelUpEffectNotActive", 1.8f);
            audioSource.PlayOneShot(levelUpSE);


            gameMaster.GetComponent<BattleMotionController>().victory_text.text = "レベル7にアップしたで!\n" + "もっと強くなろうな！";

        }

        if(playerExp >= 20000 && level == 7)
        {
            level = 8;//レベルアップ
            playerAttackMinDamage = 1400;
            playerAttackMaxDamage = 1600;

            gameObject.GetComponent<PlayerScript>().maxPlayerHP = 2500;
            gameObject.GetComponent<PlayerScript>().playerHPText.text = gameObject.GetComponent<PlayerScript>().currentPlayerHP + "/" + gameObject.GetComponent<PlayerScript>().maxPlayerHP; //更新

            playermaxEXP = 35000;
            playerExp = playerExp - playermaxEXP;
            expGauge.GetComponent<Image>().fillAmount = (float)playerExp / (float)playermaxEXP;


            yield return new WaitForSeconds(1.5f);

            levelUpEffect.SetActive(true);
            Invoke("LevelUpEffectNotActive", 1.8f);
            audioSource.PlayOneShot(levelUpSE);


            gameMaster.GetComponent<BattleMotionController>().victory_text.text = "レベル8にアップしたで!\n" + "もっと強くなろうな！";

        }

        if(playerExp >= 35000 && level == 8)
        {
            level = 9;//レベルアップ
            playerAttackMinDamage = 1600;
            playerAttackMaxDamage = 1700;

            gameObject.GetComponent<PlayerScript>().maxPlayerHP = 3000;
            gameObject.GetComponent<PlayerScript>().playerHPText.text = gameObject.GetComponent<PlayerScript>().currentPlayerHP + "/" + gameObject.GetComponent<PlayerScript>().maxPlayerHP; //更新

            playermaxEXP = 50000;
            playerExp = playerExp - playermaxEXP;
            expGauge.GetComponent<Image>().fillAmount = (float)playerExp / (float)playermaxEXP;


            yield return new WaitForSeconds(1.5f);

            levelUpEffect.SetActive(true);
            Invoke("LevelUpEffectNotActive", 1.8f);
            audioSource.PlayOneShot(levelUpSE);

            gameMaster.GetComponent<BattleMotionController>().victory_text.text = "レベル9にアップしたで!\n" + "もっと強くなろうな！";

        }

        if(playerExp >= 50000 && level == 9)
        {
            level = 10;//レベルアップ
            playerAttackMinDamage = 1800;
            playerAttackMaxDamage = 2000;

            gameObject.GetComponent<PlayerScript>().maxPlayerHP = 3400;
            gameObject.GetComponent<PlayerScript>().playerHPText.text = gameObject.GetComponent<PlayerScript>().currentPlayerHP + "/" + gameObject.GetComponent<PlayerScript>().maxPlayerHP; //更新


            expGauge.GetComponent<Image>().fillAmount = 1;　//最大レベルなので満タン


            yield return new WaitForSeconds(1.5f);

            levelUpEffect.SetActive(true);
            Invoke("LevelUpEffectNotActive", 1.8f);
            audioSource.PlayOneShot(levelUpSE);


            gameMaster.GetComponent<BattleMotionController>().victory_text.text = "レベル10にアップしたで!\n" + "君は今最強や！";

          
        }
    }

    public void LevelUpEffectNotActive()
    {
        levelUpEffect.SetActive(false);
    }
}
