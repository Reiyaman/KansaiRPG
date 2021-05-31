using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMotionController : MonoBehaviour
{
    public GameObject player; //Playerの変数
    public GameObject battleEnemy; //戦う敵の変数
    public Slider enemySlider;

    // Start is called before the first frame update
    void Start()
    {
        
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
        Invoke("EnemyAttack", 1.5f);
        if(enemySlider.value <= 0)
        {
            gameObject.SendMessage("DestroyEnemyWait");
        }
    }

    public void EnemyAttack()
    {
        if(enemySlider.value > 0)
        {
            battleEnemy.GetComponent<Animator>().SetTrigger("Attack");
            Invoke("PlayerDamage", 0.5f);
        }
       // player.GetComponent<Animator>().SetTrigger("Damage");

    }

    public void PlayerDamage()
    {
        player.GetComponent<Animator>().SetTrigger("Damage");
    }

    public void DestroyEnemyWait() //倒れるアニメーションを再生
    {
        battleEnemy.GetComponent<Animator>().SetTrigger("Death");
        Invoke("DestroyEnemy", 5f);
    }
    
    public void DestroyEnemy() //戦ったEnemyを消滅させる
    {
        Destroy(battleEnemy);
        gameObject.SendMessage("ChangeMoveMode");
    }
}
