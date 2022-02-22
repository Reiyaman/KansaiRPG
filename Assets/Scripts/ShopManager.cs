using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject shopBox; //ショップ用のテキストボックス
    public Text shopText; //ショップ用のテキスト
    public ItemBase[] itemBases;
    public string textHello;
    public ItemController itemController { get; set; }

    //public void Hello()
    //{
    //    shopBox.SetActive(true);
    //}
   
    
}
