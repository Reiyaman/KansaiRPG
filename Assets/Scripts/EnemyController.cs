using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody  rb;//Rigidbody変数の宣言
    float moveSpeed = 3; //スピードの変数の宣言

    Animator animator;//アニメーションの変数

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
