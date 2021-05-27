using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    Rigidbody  rb;//Rigidbody変数の宣言
    float moveSpeed = 14; //スピードの変数の宣言
    float walkSpeed = 3;
    public float stopDistance; //Enemyが停止するPlayerとの距離を格納する変数
    public float moveDistance; //EnemyがPlayerに向かって移動を開始する距離を格納する変数

    Vector3 enemyMoveRange;

    Transform player; //PlayerのTransformコンポーネントを格納する変数
    Animator animator;//アニメーションの変数

    NavMeshAgent agent; //

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("RPGHeroHP").transform;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        enemyMoveRange = transform.position;
    }

    // Update is called once per frame
    void Update()
    { 

        float distance = Vector3.Distance(transform.position, player.position); //変数を作成してEnemyとPlayerの距離を格納
        if(distance < moveDistance && distance > stopDistance) //距離判定
        {
            Vector3 playerPos = player.position; //変数を作成して、Playerの座標を格納
            playerPos.y = transform.position.y; //自分自身のY座標を格納
            transform.LookAt(playerPos); //EnemyをPlayerPosの座標方向に向かせる
            transform.position = transform.position + transform.forward * moveSpeed * Time.deltaTime; //Enemyを前方向に向かわせる
        }

        else
        {
           // InvokeRepeating("MoveEnemy", 1, 5);
        }
    }

    void MoveEnemy()
    {
        float enemyMoveRangex = 6 * Mathf.Sin(Random.Range(0f, 360f));
        float enemyMoveRangez = 6 * Mathf.Sin(Random.Range(0f, 360f));
        transform.position = transform.position + new Vector3(enemyMoveRangex, 1.0f, enemyMoveRangez) * walkSpeed * Time.deltaTime - enemyMoveRange;
    }
}
