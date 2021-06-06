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
            Invoke("EnemyDestroyCoroutine", 0.5f);
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
        int recover = Random.Range(200, 500);
        Text victory_text = talkScript.talkText;
       
        player.GetComponent<PlayerScript>().currentPlayerHP = player.GetComponent<PlayerScript>().currentPlayerHP + recover;
        player.GetComponent<PlayerScript>().playerSlider.value = (float)player.GetComponent<PlayerScript>().currentPlayerHP / (float)player.GetComponent<PlayerScript>().maxPlayerHP; //HPバーのゲージを減らす
        Debug.Log("slider.value : " + playerSlider.value);

        if (player.GetComponent<PlayerScript>().playerSlider.value > 0.75f) //段々と色が緑→黄→赤に変化していく
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

        if(battleEnemy.name == "BeholderPBR")
        {
             exp = Random.Range(400, 500);
        }

        else if(battleEnemy.name == "ChestMonsterPBR")
        {
             exp = Random.Range(250, 300);
        }

        else if(battleEnemy.name == "GoblinHunterMain")
        {
             exp = Random.Range(800, 1000);
        }

        else if(battleEnemy.name == "SlimePBR")
        {
             exp = Random.Range(100, 150);
        }

        else if(battleEnemy.name == "TurtleShell")
        {
             exp = Random.Range(500, 550);
        }


        victory_text.text = "おっしゃあ！　倒したったで!";

        yield return new WaitForSeconds(2.0f);

        victory_text.text = "体力" + recover + "回復したで！\n" + "経験値" + exp + "ゲットや！";

        battleEnemy.GetComponent<Animator>().SetTrigger("Death");
        
        Invoke("EnemyDestroy", 2f);
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
