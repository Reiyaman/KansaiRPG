using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    Rigidbody rb; //Rigidbody変数の宣言 
    float moveSpeed = 10; //スピードの変数の宣言
    float dashSpeed = 24;
    float rotateSpeed = 90; //回転スピードの宣言
   
    
    public int eHP; //接触したEnemyの最大HP
    public int cHP; //接触したEnemyの現在のHP

    public int maxPlayerHP; //Playerの最大HP
    public int currentPlayerHP; //Playerの現在のHP

    public GameObject playerSlider; //PlayerのHPゲージ変数
    public Image playerSliderGauge; //スライダーの色の変数
    public Color color_1, color_2, color_3, color_4; // カラーの変数
    public Text playerHPText; 

    //public GameObject enemyimage; //エネミーの画像
    public GameObject gameMaster; //GameMasterオブジェクトの変数
    public GameObject enemy; //戦うEnemyの変数
    public GameObject playerIllusion; //バトル時のPlayerの位置

    public GameObject enemycontainer;


    TalkScript talkScript;
    public EnemyBase enemyBase;
    public PlayerBase playerBase;
    int level = 1; //初期レベル

    Animator animator; //アニメーションの変数
    public static AnimatorStateInfo currentState; //現在のアニメーションの状態の変数


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();　//Rigidbodyの取得
        animator = GetComponent<Animator>();　//Animatorの取得
        //image = enemyimage.GetComponent<Image>(); //EnemyImageのImageコンポーネント取得
        currentPlayerHP = maxPlayerHP; //代入
        talkScript = gameMaster.GetComponent<TalkScript>();
        //defaultJumpCount = jumpCount;

        playerSliderGauge = playerSlider.GetComponent<Image>();

        playerHPText.text = maxPlayerHP +  "/" + maxPlayerHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        currentState = animator.GetCurrentAnimatorStateInfo(0);

        if (gameMaster.GetComponent<ModeController>().mode == false) //移動中の時
        {
            animator.SetInteger("Walk", 0);
            animator.SetInteger("Dash", 0);

            if (Input.GetKey(KeyCode.UpArrow)) //上矢印を押した場合
            { 
                animator.SetInteger("Walk", 1);
                rb.velocity = transform.forward * moveSpeed + new Vector3(0, rb.velocity.y, 0);

            }
            else if (Input.GetKey(KeyCode.DownArrow)) //下矢印を押した場合
            {
                rb.velocity = -transform.forward * moveSpeed + new Vector3(0, rb.velocity.y, 0);
            }

            else
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
            if (Input.GetKey(KeyCode.LeftArrow)) //左矢印を押した場合
            {
                transform.Rotate(new Vector3(0, -1, 0) * rotateSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.RightArrow)) //右矢印を押した場合
            {
                transform.Rotate(new Vector3(0, 1, 0) * rotateSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.Space)) // Space
            {
                animator.SetInteger("Dash", 1);
                rb.velocity = transform.forward * dashSpeed + new Vector3(0, rb.velocity.y, 0);
            }

            if(gameObject.transform.position.y <= -25.0f) //もしも落下したらGameOver
            {
                gameMaster.GetComponent<TitleController>().GameOver();
            }
            


        }

        else
        {
            
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Enemy")//Enemyに接触した場合
        {
            enemy = collision.gameObject;
            ContactEnemy(enemy);
            enemyBase = enemy.GetComponent<EnemyController>().enemyBase; //戦うEnemyのゲームオブジェクトを代入
        }

        else if(collision.gameObject.tag == "BOSS") //ボスに接触したら
        {
            enemy = collision.gameObject;
            ContactEnemy(enemy);
            enemyBase = enemy.GetComponent<BossController>().enemyBase; //戦うEnemyのゲームオブジェクトを代入
        }

        /*else if(collision.gameObject.tag == "Shop")
        {
            gameMaster.GetComponent<ModeController>().mode = true;
            collision.gameObject.GetComponent<ShopManager>().Hello();

        }*/


    }

    public void PlayerDamage() //Playerがくらう
    {
        int damage = Random.Range(enemyBase.AttackMinDamage, enemyBase.AttackMaxDamage); //攻撃のダメージを乱数で取得

        talkScript.talkText.text = damage + talkScript.textDamage;
        Debug.Log("damage : " + damage);

        currentPlayerHP = currentPlayerHP - damage; //最新のHPを取得
        Debug.Log("After current : " + currentPlayerHP);

        if(currentPlayerHP <= 0) //０以下は０と表示
        {
            currentPlayerHP = 0;
        }

        playerHPText.text = currentPlayerHP + "/" + maxPlayerHP;

        playerSliderGauge.fillAmount = (float)currentPlayerHP / (float)maxPlayerHP; //HPバーのゲージを減らす
        Debug.Log("slider.value : " + playerSlider.GetComponent<Image>().fillAmount);

        if (playerSliderGauge.fillAmount > 0.75f) //段々と色が緑→黄→赤に変化していく
        {
            playerSliderGauge.color = Color.Lerp(color_2, color_1, (playerSlider.GetComponent<Image>().fillAmount - 0.75f) * 4f);
        }

        else if (playerSliderGauge.fillAmount > 0.25f)
        {
            playerSliderGauge.color = Color.Lerp(color_3, color_2, (playerSlider.GetComponent<Image>().fillAmount - 0.25f) * 4f);
        }
        else
        {
            playerSliderGauge.color = Color.Lerp(color_4, color_3, playerSlider.GetComponent<Image>().fillAmount * 4f);
        }


        if(playerSliderGauge.fillAmount <= 0)
        {
            gameMaster.GetComponent<BattleMotionController>().PlayerDeath();

        }
        else
        {
            Invoke("NextWait", 2f);
            Invoke("ButtonTrue", 2f);
            gameMaster.GetComponent<BattleController>().special++;
            gameMaster.GetComponent<BattleController>().recovery++;
        }
    }

     public void ChangeBattleModeWait()
    {
        gameMaster.GetComponent<ModeController>().ChangeBattleMode();
        //gameMaster.SendMessage("ChangeBattleMode"); //バトルモードに移動
        animator.SetInteger("Walk", 0); //Playerの静止
        animator.SetInteger("Dash", 0);
    }

    public void battlestart() 
    {
        gameMaster.GetComponent<BattleController>().BattleStart();
    }

    public void ButtonTrue()
    {
        gameMaster.GetComponent<BattleController>().attackButton.SetActive(true);

        if(gameMaster.GetComponent<BattleController>().special >= 4 && gameObject.GetComponent<LevelController>().level >= 5) //３ターン経過＆レベル５になったら
        {
            gameMaster.GetComponent<BattleController>().specialButton.SetActive(true); //Specialボタン表示
     
        }

        if(gameMaster.GetComponent<BattleController>().recovery >= 3 && gameObject.GetComponent<LevelController>().level >= 3)//2ターン経過＆レベル3になったら
        {
            gameMaster.GetComponent<BattleController>().recoveryButton.SetActive(true);//Recoveryボタン表示
        }



    }

    public void NextWait()
    {
        gameMaster.GetComponent<ModeController>().wait = true;
        talkScript.Next();
    }

    public void ContactEnemy(GameObject enemy) //敵と接触した時の処理
    {
        
        gameMaster.GetComponent<ModeController>().mode = true;
        rb.velocity = new Vector3(0, 0, 0);

        gameObject.transform.position = enemy.transform.Find("Playerillusion").gameObject.transform.position; //指定の位置にPlayer移動

        Vector3 enemyPos = enemy.transform.position; //変数を作成して、当たったEnemyの座標を格納
        enemyPos.y = transform.position.y; //自分自身のY座標を格納
        transform.LookAt(enemyPos); //PlayerをEnemyPosの座標方向に向かせる

        gameObject.GetComponent<PlayerScript>().ChangeBattleModeWait();
        Invoke("battlestart", 0.5f);

        enemycontainer.GetComponent<AudioSource>().Play();
    }
} 
