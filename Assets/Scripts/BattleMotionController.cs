using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMotionController : MonoBehaviour
{
    public GameObject player; //Playerの変数
    public GameObject battleEnemy; //戦う敵の変数
    public GameObject enemySlider; //敵のHPゲージ
    public GameObject playerSlider; //PlayerのHPゲージ

    public GameObject attackButton; //攻撃ボタン
    public GameObject specialButton; //必殺技ボタン
    public GameObject recoveryButton; //回復ボタン
    public GameObject escapeButton; //にげるボタン

    GameObject swordeffect;
    GameObject specialeffect;

    public GameObject enemycontainer;
    public GameObject swordParticle; //必殺技時のパーティクル
    public GameObject playerDamageParticle; //ダメージ受ける時のパーティクル


    public Text victory_text; //勝利時のテキスト
    public Text playerHPText;

    public int exp;

    TalkScript talkScript;

    public AudioClip playerAttackSE; //Playerが攻撃するSE
    public AudioClip playerSpecialAttackSE; //Playerの必殺技SE
    public AudioClip playerRecoverSE; //Playerの回復SE
    public AudioClip playerEscapeSE; //PlayerがにげるSE
    public AudioClip playerNotEscapeSE; //PlayerがにげられなかったSE

    public AudioClip enemyDamageSE; //エネミーがくらうSE
    public AudioClip enemySpecialDamageSE;

    public AudioClip victorySE; //勝利BGM

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        talkScript = gameObject.GetComponent<TalkScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerAttack() //Playerが攻撃
    {
        GetEnemyData();
        player.GetComponent<Animator>().SetTrigger("Attack");

        audioSource.PlayOneShot(playerAttackSE);　
        Invoke("EnemyDamage", 0.5f);
    }

    public void PlayerSpecialAttck() //Special攻撃時
    {
        swordParticle.SetActive(true); //必殺技時に出現

        GetEnemyData();
        player.GetComponent<Animator>().SetTrigger("Special");

        audioSource.PlayOneShot(playerSpecialAttackSE);
        Invoke("EnemySpecialDamage", 0.5f);
    }

    public void PlayerRecoveryButton()
    {
        audioSource.PlayOneShot(playerRecoverSE);
        Invoke("EnemyAttack", 1.5f);
    }

    public void PlayerEscapeButton() //Escapeボタン押した時
    {
        GetEnemyData();

        if (gameObject.GetComponent<BattleController>().escapeNumber == 1)
        {
            audioSource.PlayOneShot(playerEscapeSE);

            Invoke("EnemyDestroy", 2f);

        }

        else
        {
            audioSource.PlayOneShot(playerNotEscapeSE);
            Invoke("EnemyAttack", 1.5f);
        }
    }
   
    public void EnemyDamage() //Enemyくらう
    {
        EnemyDamageAni();
        audioSource.PlayOneShot(enemyDamageSE);

        battleEnemy.transform.Find("Eff_Laser_1").gameObject.SetActive(true);
        Invoke("NotActiveEffect", 0.1f); //エフェクト消す関数

        EnemyDeath();
    }

    public void EnemySpecialDamage() //必殺技くらう時
    {
        EnemyDamageAni();
        audioSource.PlayOneShot(enemySpecialDamageSE);

        battleEnemy.transform.Find("Eff_Hit_2").gameObject.SetActive(true);
        Invoke("NotActiveEffect", 1f); //エフェクト消す関数

        EnemyDeath();
    }

    public void EnemyAttack() //Enemy攻撃
    {
        swordParticle.SetActive(false);
       
        battleEnemy.GetComponent<Animator>().SetTrigger("Attack");
        Invoke("PlayerDamage", 0.7f);
       // player.GetComponent<Animator>().SetTrigger("Damage");

    }

    public void PlayerDamage() //Playerくらう
    {
        audioSource.PlayOneShot(gameObject.GetComponent<BattleController>()._base.AttackSE);

        playerDamageParticle.SetActive(true);
        player.GetComponent<Animator>().SetTrigger("Damage");
        Invoke("NotActiveEffect", 1f); //エフェクト消す関数

    }

    public void EnemyDestroyCoroutine() 
    {
        StartCoroutine("EnemyDestroyWait");
    }

    private IEnumerator EnemyDestroyWait() //倒れるアニメーションを再生
    {
        battleEnemy.GetComponent<Animator>().SetTrigger("Death");

        int recover = Random.Range(100, 300);　//バトル勝利時の回復
        victory_text = talkScript.talkText;
        exp = Random.Range(battleEnemy.GetComponent<EnemyController>().enemyBase.ExpMin, battleEnemy.GetComponent<EnemyController>(). enemyBase.ExpMax);


        talkScript.talkText.text = talkScript.textWin; //勝利時のテキスト

        yield return new WaitForSeconds(3.0f);

        if(battleEnemy.tag != "BOSS") //ボス以外
        {
            if (player.GetComponent<PlayerScript>().currentPlayerHP + recover >= player.GetComponent<PlayerScript>().maxPlayerHP)
            {
                recover = player.GetComponent<PlayerScript>().maxPlayerHP - player.GetComponent<PlayerScript>().currentPlayerHP; //回復力の差分
                player.GetComponent<PlayerScript>().currentPlayerHP = player.GetComponent<PlayerScript>().maxPlayerHP;
                player.GetComponent<PlayerScript>().playerSliderGauge.fillAmount = 1;
                player.GetComponent<PlayerScript>().playerSliderGauge.color = player.GetComponent<PlayerScript>().color_1;

            }

            else
            {
                player.GetComponent<PlayerScript>().currentPlayerHP = player.GetComponent<PlayerScript>().currentPlayerHP + recover;
                player.GetComponent<PlayerScript>().playerSliderGauge.fillAmount = (float)player.GetComponent<PlayerScript>().currentPlayerHP / (float)player.GetComponent<PlayerScript>().maxPlayerHP; //HPバーのゲージを増やす
                Debug.Log("slider.value : " + playerSlider.GetComponent<Image>().fillAmount);
                
                if (player.GetComponent<PlayerScript>().playerSliderGauge.fillAmount > 0.75f) //回復すると色が赤→黄→緑に変化していく
                {
                    player.GetComponent<PlayerScript>().playerSliderGauge.color = Color.Lerp(player.GetComponent<PlayerScript>().color_2, player.GetComponent<PlayerScript>().color_1, (playerSlider.GetComponent<Image>().fillAmount + 0.25f) * 4f);
                }

                else if (player.GetComponent<PlayerScript>().playerSliderGauge.fillAmount > 0.25f)
                {
                    player.GetComponent<PlayerScript>().playerSliderGauge.color = Color.Lerp(player.GetComponent<PlayerScript>().color_3, player.GetComponent<PlayerScript>().color_2, (playerSlider.GetComponent<Image>().fillAmount + 0.75f) * 4f);
                }
                else
                {
                    player.GetComponent<PlayerScript>().playerSliderGauge.color = Color.Lerp(player.GetComponent<PlayerScript>().color_4, player.GetComponent<PlayerScript>().color_3, playerSlider.GetComponent<Image>().fillAmount * 4f);
                }
            }
          
            playerHPText.text = player.GetComponent<PlayerScript>().currentPlayerHP + "/" + player.GetComponent<PlayerScript>().maxPlayerHP;

            victory_text.text = talkScript.textGet1 + recover + talkScript.textGet2 + "\n" + talkScript.textGet3 + exp + talkScript.textGet4; //回復&経験値

            player.GetComponent<LevelController>().levelUpWait(); //Playerのレベルアップ関数を呼び出す

            enemycontainer.GetComponent<AudioSource>().Stop();

        }

        else //ボスに勝った場合
        {
            talkScript.talkText.text = talkScript.textClear1 + "\n" + talkScript.textClear2 + "\n" + talkScript.textClear3;
            battleEnemy.GetComponent<AudioSource>().Stop();
        }

        audioSource.PlayOneShot(victorySE);
        
        Invoke("EnemyDestroy", 4f);
    }

    
    public void EnemyDestroy() //戦ったEnemyを消滅させる
    {
        Destroy(battleEnemy);
        if(battleEnemy.gameObject.tag == "Enemy")
        {
            gameObject.GetComponent<ModeController>().ChangeMoveMode();
        }
        else if(battleEnemy.gameObject.tag == "BOSS")
        {
            gameObject.GetComponent<ModeController>().GameClear();
        }
    }

    public void PlayerDeath()
    {
        //attackButton.SetActive(false);
        //specialButton.SetActive(false);
        talkScript.talkText.text = talkScript.textLose;
        player.GetComponent<Animator>().SetTrigger("Death");
        gameObject.GetComponent<TitleController>().GameOverWait();
    }

    public void NotActiveEffect()
    {
        battleEnemy.transform.Find("Eff_Laser_1").gameObject.SetActive(false);
        battleEnemy.transform.Find("Eff_Hit_2").gameObject.SetActive(false);
        playerDamageParticle.SetActive(false);

    }
    
    public void GetEnemyData() //接触した敵の情報を変数に格納
    {
        battleEnemy = player.gameObject.GetComponent<PlayerScript>().enemy;
    }

    public void EnemyDamageAni()
    {
        battleEnemy.GetComponent<Animator>().SetTrigger("Damage");
    }

    public void EnemyDeath() //体力がなくなったら死亡
    {
        if (enemySlider.GetComponent<Image>().fillAmount <= 0)
        {
            Invoke("EnemyDestroyCoroutine", 0.2f);
        }

        else
        {
            Invoke("EnemyAttack", 1.5f);
        }
    }
}
