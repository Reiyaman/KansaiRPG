using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class ModeController : MonoBehaviour
{
    public GameObject enemyimage; //エネミーの画像
    public GameObject enemySlider; //エネミーのHPゲージ
    public GameObject attackButton; //攻撃ボタン
    public GameObject specialButton; //必殺ボタン
    public GameObject recoveryButton; //回復ボタン
    public GameObject talkBox; //トークボックス
    public GameObject HPText; //PlayerのHPテキスト
    public GameObject HPSlider; //PlayerのHPゲージ
    public GameObject GameClearText; //Gameclearテキスト

    public GameObject moveModeCamera; //移動中のカメラの変数
    public GameObject battleModeCamera; //バトル中のカメラの変数
    public GameObject gameClearCamera; //ゲームクリアのカメラの変数

    public GameObject enemyContainer;

    public bool mode; // /移動中かバトル中かの変数
    public int x; //スポナーの数
    public EnemyGenarator[] refObj; //エネミースポナースクリプトの変数

    //public GameObject Enemy;
    //public GameObject 

    //public List<GameObject>  StopEnemyObject = new List<GameObject>(); //停止させる配列

    public GameObject Player;

    Image images;

    // Start is called before the first frame update
    void Start()
    {
        EnemyGenarator enemySpawners = refObj[x].gameObject.GetComponent<EnemyGenarator>(); //EnemySpawner達のスクリプトを取得
        mode = false; //移動中はFalse

        moveModeCamera.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 100; //最初は移動カメラ
        battleModeCamera.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 1; 

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBattleMode()
    {
        mode = true; //バトルモードに遷移

        int size = refObj.Length;
        foreach(EnemyGenarator item in refObj)
        {
          if(item != null)
          {
             item.DisappearSpawn(); //フィールド上のEnemySpawnerを消す
           }

        }

        
        gameObject.GetComponent<TalkScript>().TalkWait();

        enemyContainer.GetComponent<EnemyStopController>().DisappearEnemy(); //フィールド上の全てのEnemyを消す

        Invoke("BattleMode", 0.5f);

    }

    public void ChangeMoveMode()
    {
        mode = false; //移動モードに遷移
        moveModeCamera.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 100; //移動カメラに切り替え
        battleModeCamera.gameObject.GetComponent<CinemachineVirtualCamera>().Priority = 1; 

        enemyimage.SetActive(false); //移動モードは非表示
        attackButton.SetActive(false); //移動モードは非表示
        enemySlider.SetActive(false); //移動モードは非表示
        specialButton.SetActive(false); //最初は非表示
        recoveryButton.SetActive(false);//最初は非表示
        talkBox.SetActive(false); //移動モードは非表示

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

        enemySlider.GetComponent<Slider>().value = 1;
        gameObject.GetComponent<BattleController>().enemySliderGauge.color = gameObject.GetComponent<BattleController>().color_1;

        enemyimage.SetActive(true); //画像を表示
        enemySlider.SetActive(true); //敵のHPゲージを表示
        talkBox.SetActive(true); //トークボックスを表示
        attackButton.SetActive(true); //攻撃ボタンの表示

        if (gameObject.GetComponent<BattleController>().special >= 3 && Player.GetComponent<LevelController>().level >= 6) //３ターン経過＆レベル５になったら
        {
            gameObject.GetComponent<BattleController>().specialButton.SetActive(true); //Specialボタン表示

        }

        if (gameObject.GetComponent<BattleController>().special >= 3 && Player.GetComponent<LevelController>().level >= 4) //2ターン経過＆レベル3になったら
        {
            gameObject.GetComponent<BattleController>().recoveryButton.SetActive(true); //Specialボタン表示

        }
    }

    public void GameClear()
    {
        mode = true;
        HPText.SetActive(false); //PlayerのHP文字を非表示
        HPSlider.SetActive(false); //PlayerのHPゲージを非表示
        GameClearText.SetActive(true); //Gameclearのテキストを表示
        enemyimage.SetActive(false); //非表示
        attackButton.SetActive(false); //非表示
        enemySlider.SetActive(false); //非表示
        specialButton.SetActive(false); //非表示
        recoveryButton.SetActive(false);//非表示
        talkBox.SetActive(false); //非表示

        gameObject.GetComponent<ResultController>().ResultCoroutine();
        
        
    }

}
