using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController
{
    [SerializeField] ItemBase ibase;
    public ItemBase Base { get; set; } //ベースとなるデータ
    public string ItemName { get; set; }
    public string Effect { get; set; }
    public int ItemPrice { get; set; }


    public ItemController(ItemBase _iBase) //初期設定
    {
        Base = _iBase;
        ItemName = _iBase.ItemName;
        Effect = _iBase.Effect;
        ItemPrice = _iBase.ItemPrice;
    }
}
