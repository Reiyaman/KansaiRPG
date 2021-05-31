using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeController : MonoBehaviour
{
    public GameObject enemyimage; //エネミーの画像
    public GameObject enemySlider; //エネミーのHPゲージ
    public GameObject attackButton; //攻撃ボタン
    public GameObject specialButton; //必殺ボタン
    public GameObject talkBox; //トークボックス

    public bool mode; // /移動中かバトル中かの変数
    public GameObject enemySpawner; //エネミースポナーオブジェクトの変数
    public GameObject Enemy;
    //public GameObject 

    //public List<GameObject>  StopEnemyObject = new List<GameObject>(); //停止させる配列
    
    //public GameObject Player;

    Image images;

    // Start is called before the first frame update
    void Start()
    {
        mode = false; //移動中はFalse
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBattleMode()
    {
        mode = true; //バトルモードに遷移
        enemySpawner.SendMessage("DisappearSpawn"); //EnemySpawnerを消す
        Enemy.SendMessage("DisappearEnemy");
        gameObject.SendMessage("TalkWait");
       // Enemy = Player.gameObject.GetComponent<PlayerScript>().enemy;
       // StopEnemyObject.Remove(Enemy);
        Invoke("BattleMode", 4f);

    }

    public void ChangeMoveMode()
    {
        mode = false; //移動モードに遷移
        enemyimage.SetActive(false); //移動モードは非表示
        attackButton.SetActive(false); //移動モードは非表示
        enemySlider.SetActive(false); //移動モードは非表示
        specialButton.SetActive(false); //移動モードは非表示
        talkBox.SetActive(false); //移動モードは非表示

        images = enemyimage.GetComponent<Image>(); //EnemyImageのImageコンポーネント取得
        enemySpawner.SendMessage("AppearSpawn");　//EnemySpawnerを出現させる
        Enemy.SendMessage("AppearEnemy");
    }

    public void BattleMode()
    {
        enemyimage.SetActive(true); //画像を表示
        enemySlider.SetActive(true); //敵のHPゲージを表示
        talkBox.SetActive(true); //トークボックスを表示
        attackButton.SetActive(true); //攻撃ボタンの表示
    }
}
