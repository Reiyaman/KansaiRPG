using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStopController : MonoBehaviour
{

   

   // public GameObject enemyContainer; //フィールド上のEnemyを格納するコンテナオブジェクト
    //public GameObject enemies;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void DisappearEnemyWait()
    {
        Invoke("DisappearEnemy", 0.1f);
    }

    public void DisappearEnemy() //止めて消す
    {
        foreach(Transform child in transform)
        {
            child.gameObject.GetComponent<EnemyController>().exist = false;
            child.gameObject.SetActive(false);
        }
        
       // gameObject.GetComponent<Mesh>().enabled = false;
    }

    public void AppearEnemy() //動かして出現させる
    {
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<EnemyController>().exist = true;
            child.gameObject.SetActive(true);
        }
            
    }
}
