using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody rb; //Rigidbody変数の宣言 
    float moveSpeed = 3; //スピードの変数の宣言
    float dashSpeed = 7;
    float rotateSpeed = 90; //回転スピードの宣言

    Animator animator; //アニメーションの変数
    public static AnimatorStateInfo currentState; //現在のアニメーションの状態の変数

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();　//Rigidbodyの取得
        animator = GetComponent<Animator>();
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

        else if (Input.GetKey(KeyCode.Space))
        {
            animator.SetInteger("Dash", 1);
            rb.velocity = transform.forward * dashSpeed + new Vector3(0, rb.velocity.y, 0);
        }

        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
}
