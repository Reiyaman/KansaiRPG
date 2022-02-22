using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public bool exist;

    Rigidbody  rb; //Rigidbody変数の宣言
    float moveSpeed = 14; //スピードの変数の宣言
    float walkSpeed = 3;
    float moveArea = 6;
    public float stopDistance; //Enemyが停止するPlayerとの距離を格納する変数
    public float moveDistance; //EnemyがPlayerに向かって移動を開始する距離を格納する変数

    private Vector3 startPosition; //初期位置
    private bool arrived; //目的地に到着したか
    private Vector3 direcition; //移動方向
    private Vector3 destination; //目的値
    

    bool action = false; //Playerに接触したかしていないか

    Vector3 enemyMoveRange;

    public int enemynumber; //エネミー番号

    Transform player; //PlayerのTransformコンポーネントを格納する変数
    Animator animator;//アニメーションの変数
    public static AnimatorStateInfo currentState; //現在のアニメーションの状態の変数
    
    NavMeshAgent agent; //NavMeshAgentの変数

    GameObject enemyContainer; //フィールド上のEnemyを格納するコンテナオブジェクト

    public EnemyBase enemyBase;



    // Start is called before the first frame update
    void Start()
    {
        exist = true;

        player = GameObject.Find("RPGHeroHP").transform;　//プレイヤーのオブジェクトを探して格納
        rb = GetComponent<Rigidbody>(); //RigidBodyを取得
        animator = GetComponent<Animator>(); //Animatorを取得
        agent = GetComponent<NavMeshAgent>(); //NavMeshAgentを取得
        enemyMoveRange = transform.position; //Enemyの初期位置を取得

        enemyContainer = GameObject.Find("EnemyContainer"); //Hierarchy上にないのでFindで探す
        this.gameObject.transform.parent = enemyContainer.transform; //Enemyをコンテナに格納する

        animator.SetInteger("Walk", 0);
        animator.SetInteger("Run", 0);
        animator.SetBool("Battle", false);

        var randDestination = Random.insideUnitCircle * 15; //目的地ランダム設定
        startPosition = transform.position; //初期位置の保存
        destination = startPosition + new Vector3(randDestination.x, 0, randDestination.y); //目的地の設定
        arrived = false; //目的地についてないから偽
        
    }

    public void Update()
    {
        if (exist == true)
        {
            Time.timeScale = 1; //動かす
        }

        else
        {
            Time.timeScale = 0; //停止させる
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        currentState = animator.GetCurrentAnimatorStateInfo(0);
       
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

            else //射程距離外なら
            {
                if (!arrived) //まだ目的地についていないなら
                {
                    direcition = (destination - transform.position).normalized; //正規化
                    transform.LookAt(new Vector3(destination.x, transform.position.y, destination.z)); //目的地に顔向ける
                    transform.position += direcition * walkSpeed * Time.deltaTime; //目的地に向かって進む
                    animator.SetInteger("Run", 0);
                    animator.SetInteger("Walk", 1);

                    if(Vector3.Distance(transform.position, destination) < 0.5f) //目的地についたら
                    {
                        arrived = true;
                        animator.SetInteger("Walk", 0);
                        Invoke("destinationOption", 3f);
                    }
                }
                
            }
        }

    }



    private void OnCollisionEnter(Collision collision) //接触した時の処理
    {
        if (collision.gameObject.tag == "Player") //プレイヤーに接触した場合
        {
            action = true; //Playerに接触したから静止
            this.gameObject.transform.parent = null; //Enemyをコンテナから外す
            rb.velocity = new Vector3(0, 0, 0);
            animator.SetBool("Battle", true); //バトルスタート
            
            action = true; //Playerに接触したから静止
        }
    }

    void MoveEnemy()
    {
        float enemyMoveRangex = moveArea * Mathf.Sin(Random.Range(0f, 360f));
        float enemyMoveRangez = moveArea * Mathf.Sin(Random.Range(0f, 360f));
        transform.position = transform.position + new Vector3(enemyMoveRangex, 1.0f, enemyMoveRangez) * walkSpeed * Time.deltaTime - enemyMoveRange;
    }

    public void destinationOption() //目的地更新
    {
        var randDestination = Random.insideUnitCircle * 15; //目的地ランダム設定
        startPosition = transform.position; //現時点の位置の保存
        destination = startPosition + new Vector3(randDestination.x, 0, randDestination.y); //目的地の設定
        arrived = false; //目的地についてないから偽

    }


}
