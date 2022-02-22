using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵のデータ：外部からは変更しない
[CreateAssetMenu]
public class PlayerBase : ScriptableObject
{
    //ステータス、画像
    [SerializeField] string level;
    [SerializeField] int maxHP;
    [SerializeField] int attackMinDamage;
    [SerializeField] int attackMaxDamage;
    [SerializeField] int maxExp;
    [SerializeField] string levelUpText1;
    [SerializeField] string levelUpText2;

    //他のファイルから値を取得できる
    public int MaxHP { get => maxHP; }
    public int AttackMinDamage { get => attackMinDamage; }
    public int AttackMaxDamage { get => attackMaxDamage; }
    public int MaxExp { get => maxExp; }
    public string Level { get => level;  }
    public string LevelUpText1 { get => levelUpText1; }
    public string LevelUpText2 { get => levelUpText2; }
}
