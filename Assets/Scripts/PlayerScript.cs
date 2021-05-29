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

    public int eHP; //接触したEnemyの最大HP
    public int cHP; //接触したEnemyの現在のHP
    public int maxPlayerHP; //Playerの最大HP
    public int currentPlayerHP; //Playerの現在のHP

    public Slider playerSlider; //PlayerのHPゲージ変数
    public Image playerSliderGauge; //スライダーの色の変数
    public Color color_1, color_2, color_3, color_4; // カラーの変数

    public GameObject enemyimage; //エネミーの画像
    //public GameObject[] enemy; //エネミーの配列

    Image image;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))　//左矢印を押した場合
        {
            transform.Rotate(new Vector3(0, -1, 0) * rotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))　//右矢印を押した場合
        {
            transform.Rotate(new Vector3(0, 1, 0) * rotateSpeed * Time.deltaTime);
        }

      


    }

    private void FixedUpdate()
    {
        currentState = animator.GetCurrentAnimatorStateInfo(0);
        animator.SetInteger("Walk", 0);
        animator.SetInteger("Dash", 0);

        if (Input.GetKey(KeyCode.UpArrow))　//上矢印を押した場合
        {
            animator.SetInteger("Walk", 1);
            rb.velocity = transform.forward * moveSpeed + new Vector3(0, rb.velocity.y, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow))　//下矢印を押した場合
        {
            rb.velocity = -transform.forward * moveSpeed + new Vector3(0, rb.velocity.y, 0);
        }

        else if (Input.GetKey(KeyCode.Space)) //Space
        {
            animator.SetInteger("Dash", 1);
            rb.velocity = transform.forward * dashSpeed + new Vector3(0, rb.velocity.y, 0);
        }

        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
    
    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag == "Enemy")//Enemyに接触した場合
        {
            eHP = collision.gameObject.GetComponent<EnemyController>().enemyHP; //戦うEnemyの最大HPを取得
            cHP = collision.gameObject.GetComponent<EnemyController>().currentHP; //戦うEnemyの現在のHPを取得
            image.sprite = collision.gameObject.GetComponent<EnemyController>().enemyImage; //戦うEnemyのSprite
        }
    }

    public void DamagePlayer()
    {
        int damage = Random.Range(100, 200); //攻撃のダメージを乱数で取得
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
    }
}
