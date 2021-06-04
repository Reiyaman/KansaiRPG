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
            Invoke("EnemyDestroyWait", 0.5f);
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

    public void EnemyDestroyWait() //倒れるアニメーションを再生
    {
        Text victory_text = talkScript.talkText;
        victory_text.text = "おっしゃあ！　倒したったで！";
        battleEnemy.GetComponent<Animator>().SetTrigger("Death");
        Invoke("EnemyDestroy", 6f);
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
