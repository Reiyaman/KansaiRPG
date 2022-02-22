using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class ModeController : MonoBehaviour
{
    public GameObject enemyimage; //エネミーの画像
    public GameObject enemySliderobject; //エネミーのHPゲージ
    public GameObject enemySlider;

    public GameObject attackButton; //攻撃ボタン
    public GameObject specialButton; //必殺ボタン
    public GameObject recoveryButton; //回復ボタン
    public GameObject escapeButton; //にげるボタン

    public GameObject talkBox; //トークボックス
    //public GameObject HPText; //PlayerのHPテキスト
    public GameObject HPSlider; //PlayerのHPゲージ
    public GameObject GameClearText; //Gameclearテキスト
    public GameObject enemyLevelText; //Enemyの強さテキスト
    public GameObject playerLevelText; //Playerのレベルテキスト

    public GameObject moveModeCamera; //移動中のカメラの変数
    public GameObject battleModeCamera; //バトル中のカメラの変数
    public GameObject gameClearCamera; //ゲームクリアのカメラの変数

    public GameObject sayDialog;
    public Text talkText;
    public bool wait;


    public GameObject enemyContainer;

    public bool mode; // /移動中かバトル中かの変数
    public int x; //スポナーの数
    public EnemyGenarator[] refObj; //エネミースポナースクリプトの変数

    //public AudioClip[] bgm; //移動とバトルのBGM配列
    public AudioClip gameclearSE;


    AudioSource moveBGM;

    public GameObject Player;

    Image images;

    // Start is called before the first frame update
    void Start()
    { 

        moveBGM = GetComponent<AudioSource>();

        //wait = false;

        sayDialog.SetActive(false);
        EnemyGenarator enemySpawners = refObj[x].gameObject.GetComponent<EnemyGenarator>(); //EnemySpawner達のスクリプトを取得
        mode = false; //移動中はFalse

        moveModeCamera.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 100; //最初は移動カメラ
        battleModeCamera.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 1; 

    }

    // Update is called once per frame
    void Update()
    {
        if (sayDialog.activeSelf == true) //会話中は消す
        {
            
            attackButton.SetActive(false);
            specialButton.SetActive(false);
            recoveryButton.SetActive(false);
            escapeButton.SetActive(false);
            talkBox.SetActive(false);
            
            wait = true;
        }

        else if (wait == true && sayDialog.activeSelf == false)
        {

            talkBox.SetActive(true);
            enemyimage.SetActive(true); //少し間を開けて表示
            enemySliderobject.SetActive(true);
            enemyLevelText.SetActive(true);
 
            attackButton.SetActive(true);

            if (gameObject.GetComponent<BattleController>().special >= 4 && Player.GetComponent<LevelController>().level >= 5) //３ターン経過＆レベル５になったら
            {
                gameObject.GetComponent<BattleController>().specialButton.SetActive(true); //Specialボタン表示
            }

                

            if (gameObject.GetComponent<BattleController>().recovery >= 3 && Player.GetComponent<LevelController>().level >= 3) //2ターン経過＆レベル3になったら
            {
                gameObject.GetComponent<BattleController>().recoveryButton.SetActive(true); //Recoverボタン表示
            }


            if(gameObject.GetComponent<BattleController>().player.gameObject.GetComponent<PlayerScript>().enemy.tag != "BOSS") //道中の敵にだけ表示
            {
                escapeButton.SetActive(true);
            }

            

        }
    }


    public void ChangeBattleMode()
    {
        moveBGM.Stop();
        //enemyContainer.GetComponent<AudioSource>().Play();
        mode = true; //バトルモードに遷移

        int size = refObj.Length;
        foreach(EnemyGenarator item in refObj)
        {
          if(item != null)
          {
             item.DisappearSpawn(); //フィールド上のEnemySpawnerを消す
           }

        }

        enemySlider.GetComponent<Image>().fillAmount = 1;
        gameObject.GetComponent<BattleController>().enemySliderGauge.color = gameObject.GetComponent<BattleController>().color_1;


        gameObject.GetComponent<TalkScript>().TalkWait();

        enemyContainer.GetComponent<EnemyStopController>().DisappearEnemyWait(); //フィールド上の全てのEnemyを消す

        Invoke("BattleMode", 0.5f);

    }

    public void ChangeMoveMode()
    {
        //audioSource.Stop();
        //audioSource.clip = bgm[0];
        //audioSource.UnPause();
        

        enemyContainer.GetComponent<AudioSource>().Stop();
        moveBGM.Play();

        mode = false; //移動モードに遷移
        moveModeCamera.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 100; //移動カメラに切り替え
        battleModeCamera.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 1;

        MoveHide();

        images = enemyimage.GetComponent<Image>(); //EnemyImageのImageコンポーネント取得

        foreach (EnemyGenarator item in refObj)
        {
            if (item != null)
            {
               item.AppearSpawn(); //フィールド上のEnemySpawnerを出現させる
           }
        }

        enemyContainer.GetComponent<EnemyStopController>().AppearEnemy();　//フィールド上の全てのEnemyを出現させる
    }

    public void BattleMode()
    {

        moveModeCamera.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 1;
        battleModeCamera.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 100; //バトルカメラに切り替え

        enemyimage.SetActive(true); //画像を表示
        enemySliderobject.SetActive(true); //敵のHPゲージを表示

    }

    public void GameClear()
    {
        mode = true;
        MoveHide();
        HPSlider.SetActive(false); //PlayerのHPゲージを非表示
        GameClearText.SetActive(true); //Gameclearのテキストを表示
        playerLevelText.SetActive(false);

        moveBGM.PlayOneShot(gameclearSE);

        gameObject.GetComponent<ResultController>().ResultCoroutine();
        
        
    }

    public void MoveHide() //移動モード中隠すもの
    {
        attackButton.SetActive(false);
        specialButton.SetActive(false);
        recoveryButton.SetActive(false);
        escapeButton.SetActive(false);
        talkBox.SetActive(false);
        enemyimage.SetActive(false); //非表示
        enemySliderobject.SetActive(false);
        enemyLevelText.SetActive(false);//移動モードは非表示
        
  
    }

}
