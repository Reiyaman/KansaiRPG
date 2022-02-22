using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    Rigidbody rb; //Rigidbody変数の宣言

    Animator animator;//アニメーションの変数
    public static AnimatorStateInfo currentState; //現在のアニメーションの状態の変数
    public EnemyBase enemyBase;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //RigidBodyを取得
        animator = GetComponent<Animator>(); //Animatorを取得
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            rb.velocity = new Vector3(0, 0, 0);

            animator.SetBool("Battle", true); //バトルスタート

            Vector3 playerPos = other.transform.position; //変数を作成して、Playerの座標を格納
            playerPos.y = transform.position.y; //自分自身のY座標を格納
            transform.LookAt(playerPos); //EnemyをPlayerPosの座標方向に向かせる

        }
    }
}
