using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenarator : MonoBehaviour
{
    public GameObject[] enemies; //Enemyの配列を宣言
    public GameObject player; //Playerオブジェクトの変数
    public float interval = 5; //何秒に一回敵を発生させるか
    float timer = 7; //タイマー
    float moveDistance = 40; //EnemyがPlayerに向かって移動を開始する距離を格納する変数

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("GenerateEnemy", 10, 10);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime; //タイマーを減らす
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (timer < 0 && distance < moveDistance)　//タイマーが切れてPlayerが一定距離内に入ったら
        {
            Spawn();
            timer = interval;
        }
    }

    void GenerateEnemy()
    {
        int enemyNumber = Random.Range(0, enemies.Length); //どの敵が出現するかランダムで選ぶ

        //Enemyを出現させる場所をランダムで指定
        float enemyAppearx = player.transform.position.x + Random.Range(-10, 10);
        float enemyAppeary = 1.0f;
        float enemyAppearz = player.transform.forward.z * Random.Range(0.5f, 2);
        Vector3 enemyPosition = new Vector3(enemyAppearx, enemyAppeary, enemyAppearz) - player.transform.position;

        Instantiate(enemies[enemyNumber], enemyPosition, Quaternion.identity); //選んだエネミーを、決められた位置にオリジナルの向きで出現させる

    }

    void Spawn()
    {
        int enemyNumber = Random.Range(0, enemies.Length); //どの敵が出現するかランダムで選ぶ
        Instantiate(enemies[enemyNumber], transform.position, transform.rotation); //敵を生成する
    }
}