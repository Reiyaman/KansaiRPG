using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    Rigidbody rb; //Rigidbody変数の宣言

    public int enemyHP; //ボスの最大HP
    public int currentHP; //現在のボスのHP
    public Sprite enemyImage; //ボスのSprite
    public int enemyAttackMaxDamage; //接触したエネミーの攻撃力
    public int enemyAttackMinDamage; //

    Animator animator;//アニメーションの変数
    public static AnimatorStateInfo currentState; //現在のアニメーションの状態の変数

    //Transform player; //PlayerのTransformコンポーネントを格納する変数

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //RigidBodyを取得
        animator = GetComponent<Animator>(); //Animatorを取得
        currentHP = enemyHP; //代入
      //  player = GameObject.Find("RPGHeroHP").transform;　//プレイヤーのオブジェクトを探して格納

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            animator.SetBool("Battle", true); //バトルスタート

            Vector3 playerPos = other.transform.position; //変数を作成して、Playerの座標を格納
            playerPos.y = transform.position.y; //自分自身のY座標を格納
            transform.LookAt(playerPos); //EnemyをPlayerPosの座標方向に向かせる

            Rigidbody playerBody = other.gameObject.GetComponent<Rigidbody>(); //PlayerのRigidbodyを取得
            //if (other.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Dash"))
            //{
             //   Vector3 attackForce = (other.transform.position - this.transform.position) * 7; //Playerに与える力を設定
             //   attackForce.y = transform.position.y; //Y座標だけは動かさない
              //  playerBody.AddForce(attackForce, ForceMode.Impulse); //Playerに衝撃を与える
            //}
            //else if (other.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            //{
               // Vector3 attackForce = (other.transform.position - this.transform.position) * 3f; //Playerに与える力を設定
               // attackForce.y = transform.position.y; //Y座標だけは動かさない
               // playerBody.AddForce(attackForce, ForceMode.Impulse); //Playerに衝撃を与える
           // }
           // else
           // {
             //   Vector3 attackForce = (other.transform.position - this.transform.position) * 2; //Playerに与える力を設定
               // attackForce.y = transform.position.y; //Y座標だけは動かさない
               /// playerBody.AddForce(attackForce, ForceMode.Impulse); //Playerに衝撃を与える
           // }
        }
    }
}
