using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    Rigidbody rb; //Rigidbody変数の宣言 
    float moveSpeed = 9; //スピードの変数の宣言
    float dashSpeed = 20;
    float rotateSpeed = 90; //回転スピードの宣言

    public int jumpCount = 1; //ジャンプできる回数
    int defaultJumpCount; //設定したジャンプできる回数を保存する変数
   
    
    public int eHP; //接触したEnemyの最大HP
    public int cHP; //接触したEnemyの現在のHP
    public int enemyAttackMaxDamage; //接触したエネミーの攻撃力
    public int enemyAttackMinDamage; //

    public int maxPlayerHP; //Playerの最大HP
    public int currentPlayerHP; //Playerの現在のHP

    public Slider playerSlider; //PlayerのHPゲージ変数
    public Image playerSliderGauge; //スライダーの色の変数
    public Color color_1, color_2, color_3, color_4; // カラーの変数

    public GameObject enemyimage; //エネミーの画像
    public GameObject gameMaster; //GameMasterオブジェクトの変数
    public GameObject enemy; //戦うEnemyの変数
    public GameObject playerIllusion; //バトル時のPlayerの位置

    public GameObject enemycontainer;

    public AudioClip jumpSE; //ジャンプSE
    public AudioClip landingSE; //着地SE
    //public GameObject attackButton; //攻撃ボタン
    //public GameObject specialButton; //必殺ボタン

    TalkScript talkScript;
    

    //public GameObject[] enemy; //エネミーの配列

    public Image image;
    //Sprite[] enemySprite = new Sprite[6]; //Spriteを入れるための配列

    Animator animator; //アニメーションの変数
    public static AnimatorStateInfo currentState; //現在のアニメーションの状態の変数

   // private void Awake()
   // {
       // for(int i = 0; i < enemy.Length; i++)
       // {
       //     enemySprite[i] = Resources.Load<Sprite>(enemy[i].name); //Resourcesフォルダに入れたSpriteを取得する
       // }
    //}
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();　//Rigidbodyの取得
        animator = GetComponent<Animator>();　//Animatorの取得
        image = enemyimage.GetComponent<Image>(); //EnemyImageのImageコンポーネント取得
        currentPlayerHP = maxPlayerHP; //代入
        talkScript = gameMaster.GetComponent<TalkScript>();
        defaultJumpCount = jumpCount;
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

            if (Input.GetKey(KeyCode.D)) //D
            {
                animator.SetInteger("Dash", 1);
                rb.velocity = transform.forward * dashSpeed + new Vector3(0, rb.velocity.y, 0);
            }

            if (Input.GetKey(KeyCode.Space) && jumpCount > 0 ) //Spaceでジャンプ
            {
                //rb.velocity = new Vector3(0, 0, 0);
                rb.AddForce(new Vector3(0, 11, 0), ForceMode.Impulse);

                jumpCount--;

                gameObject.GetComponent<AudioSource>().PlayOneShot(jumpSE);
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
            

            gameMaster.GetComponent<ModeController>().mode = true;
            rb.velocity = new Vector3(0, 0, 0);

            currentPlayerHP = maxPlayerHP; //代入

            eHP = collision.gameObject.GetComponent<EnemyController>().enemyHP; //戦うEnemyの最大HPを取得
            cHP = collision.gameObject.GetComponent<EnemyController>().currentHP; //戦うEnemyの現在のHPを取得
            image.sprite = collision.gameObject.GetComponent<EnemyController>().enemyImage; //戦うEnemyのSprits
            enemy = collision.gameObject; //戦うEnemyのゲームオブジェクトを代入
            enemyAttackMaxDamage = collision.gameObject.GetComponent<EnemyController>().enemyattackmaxDamage;
            enemyAttackMinDamage = collision.gameObject.GetComponent<EnemyController>().enemyattackminDamage;

            gameObject.transform.position = collision.transform.Find("Playerillusion").gameObject.transform.position;

            Vector3 enemyPos = collision.transform.position; //変数を作成して、当たったEnemyの座標を格納
            enemyPos.y = transform.position.y; //自分自身のY座標を格納
            transform.LookAt(enemyPos); //PlayerをEnemyPosの座標方向に向かせる

            gameObject.GetComponent<PlayerScript>().ChangeBattleModeWait();
            //Invoke("ChangeBattleModeWait", 0.5f);
            Invoke("battlestart", 0.5f);

            enemycontainer.GetComponent<AudioSource>().Play();

            //Rigidbody enemyBody = other.gameObject.GetComponent<Rigidbody>(); //EnemyのRigidbodyを取得
            //Vector3 attackForce = (other.transform.position - this.transform.position) * 5; //Enemyに与える力を設定
            //enemyBody.AddForce(attackForce, ForceMode.Impulse); //Enemyに衝撃を与える
        }

        else if(collision.gameObject.tag == "BOSS") //ボスに接触したら
        {
            gameMaster.GetComponent<ModeController>().mode = true;
            rb.velocity = new Vector3(0, 0, 0);

            currentPlayerHP = maxPlayerHP; //代入

            eHP = collision.gameObject.GetComponent<BossController>().enemyHP;
            cHP = collision.gameObject.GetComponent<BossController>().currentHP;
            image.sprite = collision.gameObject.GetComponent<BossController>().enemyImage; //戦うEnemyのSprits
            enemyAttackMaxDamage = collision.gameObject.GetComponent<BossController>().enemyAttackMaxDamage;
            enemyAttackMinDamage = collision.gameObject.GetComponent<BossController>().enemyAttackMinDamage;
            enemy = collision.gameObject; //戦うEnemyのゲームオブジェクトを代入

            gameObject.transform.position = collision.transform.Find("Playerillusion").gameObject.transform.position;

            Vector3 enemyPos = collision.transform.position; //変数を作成して、当たったEnemyの座標を格納
            enemyPos.y = transform.position.y; //自分自身のY座標を格納
            transform.LookAt(enemyPos); //PlayerをEnemyPosの座標方向に向かせる

            gameObject.GetComponent<PlayerScript>().ChangeBattleModeWait();
            //Invoke("ChangeBattleModeWait", 3.0f);
            Invoke("battlestart", 0.5f);

            collision.gameObject.GetComponent<AudioSource>().Play();
        }

        else if(collision.gameObject.tag == "Ground")
        {
            jumpCount = defaultJumpCount;
            gameObject.GetComponent<AudioSource>().PlayOneShot(landingSE);
        }

    }

    public void PlayerDamage() //Playerがくらう
    {
        int damage = Random.Range(enemyAttackMinDamage, enemyAttackMaxDamage); //攻撃のダメージを乱数で取得

        Text damage_text = talkScript.talkText;
        damage_text.text = damage + "のダメージをくらってもうたわ！";
        Debug.Log("damage : " + damage);


        currentPlayerHP = currentPlayerHP - damage; //最新のHPを取得
        Debug.Log("After current : " + currentPlayerHP);

        playerSlider.value = (float)currentPlayerHP / (float)maxPlayerHP; //HPバーのゲージを減らす
        Debug.Log("slider.value : " + playerSlider.value);

        if (playerSlider.value > 0.75f) //段々と色が緑→黄→赤に変化していく
        {
            playerSliderGauge.color = Color.Lerp(color_2, color_1, (playerSlider.value - 0.75f) * 4f);
        }

        else if (playerSlider.value > 0.25f)
        {
            playerSliderGauge.color = Color.Lerp(color_3, color_2, (playerSlider.value - 0.25f) * 4f);
        }
        else
        {
            playerSliderGauge.color = Color.Lerp(color_4, color_3, playerSlider.value * 4f);
        }


        if(playerSlider.value <= 0)
        {
            gameMaster.GetComponent<BattleMotionController>().PlayerDeath();
           // gameMaster.SendMessage("PlayerDeath");

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
       // gameMaster.SendMessage("BattleStart"); //GameMasterに関数を送る
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
        talkScript.Next();
    }

}
