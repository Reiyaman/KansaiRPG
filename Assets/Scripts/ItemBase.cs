using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemBase : ScriptableObject
{
  //名前、 効果、価格
    [SerializeField] string itemName;
    [SerializeField] string effect;
    [SerializeField] int itemPrice;


    //他のファイルから値を取得できる
    public string ItemName { get => itemName; }
    public string Effect { get => effect; }
    public int ItemPrice { get => itemPrice; }


}
