using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStopController : MonoBehaviour
{

    bool exist; //出現しているか消えているか

    // Start is called before the first frame update
    void Start()
    {
        exist = true;
    }

    // Update is called once per frame
    void Update()
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

    public void DisappearEnemy() //止めて消す
    {
        exist = false;
       // gameObject.GetComponent<Mesh>().enabled = false;
    }

    public void AppearEnemy() //動かして出現させる
    {
       exist = true;
      gameObject.SetActive(true);
    }
}
