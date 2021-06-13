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

    public GameObject enemycontainer;
    public GameObject swordParticle; //必殺技時のパーティクル


    public Text victory_text; //勝利時のテキスト
    public Text playerHPText;

    public int exp;

    TalkScript talkScript;


    public AudioClip playerdamageSE; //PlayerDamageSE
    public AudioClip playerstingSE; //Playerが刺された時のSE
    public AudioClip playertackleSE; //Playerが物理攻撃食らったSE
    public AudioClip playerbossSE; //ボスに食らった時のSE

    public AudioClip playerAttackSE; //Playerが攻撃するSE
    public AudioClip playerSpecialAttackSE; //Playerの必殺技SE
    public AudioClip playerRecoverSE; //Playerの回復SE

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
        battleEnemy = player.gameObject.GetComponent<PlayerScript>().enemy;
        player.GetComponent<Animator>().SetTrigger("Attack");

        audioSource.PlayOneShot(playerAttackSE);　
        Invoke("EnemyDamage", 0.5f);
        //battleEnemy.GetComponent<Animator>().SetTrigger("Damage");
        //Invoke("EnemyAttack", 2.0f);
    }

    public void PlayerSpecialAttck() //Special攻撃時
    {
        swordParticle.SetActive(true); //必殺技時に出現
        
        battleEnemy = player.gameObject.GetComponent<PlayerScript>().enemy;
        player.GetComponent<Animator>().SetTrigger("Special");

        audioSource.PlayOneShot(playerSpecialAttackSE);
        Invoke("EnemySpecialDamage", 0.5f);
    }

    public void PlayerRecoveryButton()
    {
        audioSource.PlayOneShot(playerRecoverSE);
        Invoke("EnemyAttack", 1.5f);
    }
   
    public void EnemyDamage() //Enemyくらう
    {
        battleEnemy.GetComponent<Animator>().SetTrigger("Damage");
        audioSource.PlayOneShot(enemyDamageSE);

        if(enemySlider.GetComponent<Image>().fillAmount <= 0)
        {
            Invoke("EnemyDestroyCoroutine", 0.2f);
        }

        else
        {
            Invoke("EnemyAttack", 1.5f);
        }
    }

    public void EnemySpecialDamage() //必殺技くらう時
    {
        battleEnemy.GetComponent<Animator>().SetTrigger("Damage");
        audioSource.PlayOneShot(enemySpecialDamageSE);

        if (enemySlider.GetComponent<Image>().fillAmount <= 0) //体力がなくなったら死亡
        {
            Invoke("EnemyDestroyCoroutine", 0.2f);
        }

        else
        {
            Invoke("EnemyAttack", 1.5f);
        }
    }

    public void EnemyAttack() //Enemy攻撃
    {
        swordParticle.SetActive(false);

        if(battleEnemy.tag != "BOSS")
        {
            audioSource.PlayOneShot(battleEnemy.GetComponent<EnemyController>().attackSE);
        }

        else
        {
            audioSource.PlayOneShot(battleEnemy.GetComponent<BossController>().attackSE);
        }

        battleEnemy.GetComponent<Animator>().SetTrigger("Attack");
        Invoke("PlayerDamage", 0.7f);
       // player.GetComponent<Animator>().SetTrigger("Damage");

    }

    public void PlayerDamage() //Playerくらう
    {
        if(battleEnemy.gameObject.tag == "BOSS")
        {
            audioSource.PlayOneShot(playerbossSE);
        }


        else if(battleEnemy.GetComponent<EnemyController>().enemynumber == 4 || battleEnemy.GetComponent<EnemyController>().enemynumber == 5 || battleEnemy.GetComponent<EnemyController>().enemynumber == 6) //槍使いとゴブリンか骸骨にくらった時
        {
            audioSource.PlayOneShot(playerstingSE);
        }

        else if(battleEnemy.GetComponent<EnemyController>().enemynumber == 1 || battleEnemy.GetComponent<EnemyController>().enemynumber == 2 || battleEnemy.GetComponent<EnemyController>().enemynumber == 3) //ピンクか箱か甲羅に食らった時
        {
            audioSource.PlayOneShot(playertackleSE);
        }

        else //スライムに食らった時
        {
            audioSource.PlayOneShot(playerdamageSE);
        }

        player.GetComponent<Animator>().SetTrigger("Damage");
        
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

        if(battleEnemy.tag == "BOSS")
        {
            exp = 0;
        }

        else if (battleEnemy.GetComponent<EnemyController>().enemynumber == 0) //スライム
        {
            exp = Random.Range(100, 150);
        }

        else if (battleEnemy.GetComponent<EnemyController>().enemynumber == 1) //箱のモンスター
        {
            exp = Random.Range(250, 300);
        }

        else if (battleEnemy.GetComponent<EnemyController>().enemynumber == 2) //ピンクのモンスター
        {
            exp = Random.Range(400, 450);
        }


        else if (battleEnemy.GetComponent<EnemyController>().enemynumber == 3) //青いスライム
        {
            exp = Random.Range(500, 550);
        }

        else if (battleEnemy.GetComponent<EnemyController>().enemynumber == 4) //スケルトン
        {
            exp = Random.Range(700, 800);
        }

        else if (battleEnemy.GetComponent<EnemyController>().enemynumber == 5) //槍使い
        {
            exp = Random.Range(700, 800);
        }

        else if (battleEnemy.GetComponent<EnemyController>().enemynumber == 6) //ゴブリン
        {
            exp = Random.Range(800, 1000);
        }


        victory_text.text = "おっしゃあ！　たおしたったで!"; //勝利時のテキスト

        yield return new WaitForSeconds(3.0f);

        if(battleEnemy.tag != "BOSS") //ボス以外
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


            if(player.GetComponent<PlayerScript>().currentPlayerHP >= player.GetComponent<PlayerScript>().maxPlayerHP)
            {
                player.GetComponent<PlayerScript>().currentPlayerHP = player.GetComponent<PlayerScript>().maxPlayerHP;
                player.GetComponent<PlayerScript>().playerSliderGauge.fillAmount = 1;

            }

            playerHPText.text = player.GetComponent<PlayerScript>().currentPlayerHP + "/" + player.GetComponent<PlayerScript>().maxPlayerHP;

            victory_text.text = "たいりょく " + recover + "　かいふく　したで！\n" + "けいけんち　" + exp + "　ゲットや！"; //回復&経験値

            player.GetComponent<LevelController>().levelUpWait(); //Playerのレベルアップ関数を呼び出す

            enemycontainer.GetComponent<AudioSource>().Stop();

        }

        else //ボスに勝った場合
        {
            victory_text.text = "ボスを　みごと　げきは　やで！\n" + "これで　オウサカじまの　へいわが　まもられたで！\n" + "ほんま　ありがとうな！";
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
        Text lose_text = talkScript.talkText;
        lose_text.text = "あああ、　やられてしもうた、、";
        player.GetComponent<Animator>().SetTrigger("Death");
        gameObject.GetComponent<TitleController>().GameOverWait();
    }
}
