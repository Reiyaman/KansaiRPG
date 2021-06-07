using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMotionController : MonoBehaviour
{
    public GameObject player; //Playerの変数
    public GameObject battleEnemy; //戦う敵の変数
    public Slider enemySlider; //敵のHPゲージ
    public Slider playerSlider; //PlayerのHPゲージ
    public GameObject attackButton;
    public GameObject specialButton;

    public Text victory_text; //勝利時のテキスト

    public int exp;

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

    public void PlayerAttack()
    {
        battleEnemy = player.gameObject.GetComponent<PlayerScript>().enemy;
        player.GetComponent<Animator>().SetTrigger("Attack");
        Invoke("EnemyDamage", 0.5f);
        //battleEnemy.GetComponent<Animator>().SetTrigger("Damage");
        //Invoke("EnemyAttack", 2.0f);
    }

    public void EnemyDamage()
    {
        battleEnemy.GetComponent<Animator>().SetTrigger("Damage");

        if(enemySlider.value <= 0)
        {
            Invoke("EnemyDestroyCoroutine", 0.2f);
        }

        else
        {
            Invoke("EnemyAttack", 1.5f);
        }
    }

    public void EnemyAttack()
    {
        if(enemySlider.value > 0)
        {
            battleEnemy.GetComponent<Animator>().SetTrigger("Attack");
            Invoke("PlayerDamage", 0.7f);
        }
       // player.GetComponent<Animator>().SetTrigger("Damage");

    }

    public void PlayerDamage()
    {
        player.GetComponent<Animator>().SetTrigger("Damage");
        
    }

    public void EnemyDestroyCoroutine()
    {
        StartCoroutine("EnemyDestroyWait");
    }

    private IEnumerator EnemyDestroyWait() //倒れるアニメーションを再生
    {
        battleEnemy.GetComponent<Animator>().SetTrigger("Death");

        int recover = Random.Range(100, 400);　//バトル勝利時の回復
        victory_text = talkScript.talkText;

        if(battleEnemy.name == "BeholderPBR") //ピンクのモンスター
        {
             exp = Random.Range(400, 500);
        }

        else if(battleEnemy.name == "ChestMonsterPBR") //箱のモンスター
        {
             exp = Random.Range(250, 300);
        }

        else if(battleEnemy.name == "GoblinHunterMain") //ゴブリン
        {
             exp = Random.Range(800, 1000);
        }

        else if(battleEnemy.name == "SlimePBR") //スライム
        {
             exp = Random.Range(100, 150);
        }

        else if(battleEnemy.name == "TurtleShell") //青いスライム
        {
             exp = Random.Range(500, 550);
        }
        
        victory_text.text = "おっしゃあ！　倒したったで!"; //勝利時のテキスト

        yield return new WaitForSeconds(3.0f);

        if(battleEnemy.name != "wizard") //ボス以外
        {
            player.GetComponent<PlayerScript>().currentPlayerHP = player.GetComponent<PlayerScript>().currentPlayerHP + recover;
            player.GetComponent<PlayerScript>().playerSlider.value = (float)player.GetComponent<PlayerScript>().currentPlayerHP / (float)player.GetComponent<PlayerScript>().maxPlayerHP; //HPバーのゲージを増やす
            Debug.Log("slider.value : " + playerSlider.value);

            if (player.GetComponent<PlayerScript>().playerSlider.value > 0.75f) //回復すると色が赤→黄→緑に変化していく
            {
                player.GetComponent<PlayerScript>().playerSliderGauge.color = Color.Lerp(player.GetComponent<PlayerScript>().color_2, player.GetComponent<PlayerScript>().color_1, (playerSlider.value + 0.25f) * 4f);
            }

            else if (player.GetComponent<PlayerScript>().playerSlider.value > 0.25f)
            {
                player.GetComponent<PlayerScript>().playerSliderGauge.color = Color.Lerp(player.GetComponent<PlayerScript>().color_3, player.GetComponent<PlayerScript>().color_2, (playerSlider.value + 0.75f) * 4f);
            }
            else
            {
                player.GetComponent<PlayerScript>().playerSliderGauge.color = Color.Lerp(player.GetComponent<PlayerScript>().color_4, player.GetComponent<PlayerScript>().color_3, playerSlider.value * 4f);
            }
            victory_text.text = "体力" + recover + "回復したで！\n" + "経験値" + exp + "ゲットや！"; //回復&経験値

            player.GetComponent<LevelController>().levelUpWait(); //Playerのレベルアップ関数を呼び出す

        }

        else //ボスに勝った場合
        {
            victory_text.text = "ボスを見事撃破やで！\n" + "これでオウサカ島の平和が守られたで！\n" + "ほんまありがとうな！";
        }

        
        
        
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
        lose_text.text = "あああ、やられてしもうた、、";
        player.GetComponent<Animator>().SetTrigger("Death");
        gameObject.GetComponent<TitleController>().GameOverWait();
    }
}
