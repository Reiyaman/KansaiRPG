using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    Rigidbody  rb;//Rigidbody変数の宣言
    float moveSpeed = 14; //スピードの変数の宣言
    float walkSpeed = 3;
    public float stopDistance; //Enemyが停止するPlayerとの距離を格納する変数
    public float moveDistance; //EnemyがPlayerに向かって移動を開始する距離を格納する変数
    bool action = false; //Playerに接触したかしていないか

    Vector3 enemyMoveRange;

    public int enemyHP ; //敵の最大HP
    public int currentHP; //現在の敵のHP
    public Sprite enemyImage; //敵のSprite
    
    Transform player; //PlayerのTransformコンポーネントを格納する変数
    Animator animator;//アニメーションの変数
    public static AnimatorStateInfo currentState; //現在のアニメーションの状態の変数
    
    NavMeshAgent agent; //NavMeshAgentの変数

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("RPGHeroHP").transform;　//プレイヤーのオブジェクトを探して格納
        rb = GetComponent<Rigidbody>(); //RigidBodyを取得
        animator = GetComponent<Animator>(); //Animatorを取得
        agent = GetComponent<NavMeshAgent>(); //NavMeshAgentを取得
        enemyMoveRange = transform.position; //Enemyの初期位置を取得
        currentHP = enemyHP; //代入
        //enemyImage = GetComponent<Image>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        currentState = animator.GetCurrentAnimatorStateInfo(0);
        animator.SetInteger("Walk", 0);
        animator.SetInteger("Run", 0);
        animator.SetBool("Battle", false);

        if(action == false) //Playerにまだ接触していないので動ける
        {
            float distance = Vector3.Distance(transform.position, player.position); //変数を作成してEnemyとPlayerの距離を格納
            if (distance < moveDistance && distance > stopDistance) //距離判定
            {
                Vector3 playerPos = player.position; //変数を作成して、Playerの座標を格納
                playerPos.y = transform.position.y; //自分自身のY座標を格納
                transform.LookAt(playerPos); //EnemyをPlayerPosの座標方向に向かせる
                animator.SetInteger("Run", 1);
                transform.position = transform.position + transform.forward * moveSpeed * Time.deltaTime; //Enemyを前方向に向かわせる
            }

            else
            {
                // InvokeRepeating("MoveEnemy", 1, 5);
            }
        }
    }

    private void OnTriggerEnter(Collider other) //接触した時の処理
    {
        if (other.gameObject.tag == "Player") //プレイヤーに接触した場合
        {
            Rigidbody playerBody = other.gameObject.GetComponent<Rigidbody>(); //PlayerのRigidbodyを取得
            if (other.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Dash"))
            {
                Vector3 attackForce = (other.transform.position - this.transform.position) * 7; //Playerに与える力を設定
                attackForce.y = transform.position.y; //Y座標だけは動かさない
                playerBody.AddForce(attackForce, ForceMode.Impulse); //Playerに衝撃を与える
            }
            else if (other.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
                Vector3 attackForce = (other.transform.position - this.transform.position) * 3f; //Playerに与える力を設定
                attackForce.y = transform.position.y; //Y座標だけは動かさない
                playerBody.AddForce(attackForce, ForceMode.Impulse); //Playerに衝撃を与える
            }
            else
            {
                Vector3 attackForce = (other.transform.position - this.transform.position) * 2; //Playerに与える力を設定
                attackForce.y = transform.position.y; //Y座標だけは動かさない
                playerBody.AddForce(attackForce, ForceMode.Impulse); //Playerに衝撃を与える
            }

            //animator.SetInteger("Walk", 0);
            //animator.SetInteger("Run", 0);
            animator.SetBool("Battle", true); //バトルスタート
            action = true; //Playerに接触したから静止
        }
    }

    void MoveEnemy()
    {
        float enemyMoveRangex = 6 * Mathf.Sin(Random.Range(0f, 360f));
        float enemyMoveRangez = 6 * Mathf.Sin(Random.Range(0f, 360f));
        transform.position = transform.position + new Vector3(enemyMoveRangex, 1.0f, enemyMoveRangez) * walkSpeed * Time.deltaTime - enemyMoveRange;
    }

}
