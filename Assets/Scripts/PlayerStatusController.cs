using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusController
{
    [SerializeField] PlayerBase pbase;
    public PlayerBase Base { get; set; } //ベースとなるデータ
    public string Level { get; set; }
    public int MaxHP { get; set; }
    public int AttackMinDamage { get; set; }
    public int AttackMaxDamage { get; set; }
    public int MaxExp { get; set; }
    public string LevelUpText1 { get; set; }
    public string LevelUpText2 { get; set; }


    public PlayerStatusController(PlayerBase pBase) //初期設定
    {
        Base = pBase;
        Level = pBase.Level;
        MaxHP = pBase.MaxHP;
        AttackMinDamage = pBase.AttackMinDamage;
        AttackMaxDamage = pBase.AttackMaxDamage;
        MaxExp = pBase.MaxExp;
        LevelUpText1 = pBase.LevelUpText1;
        LevelUpText2 = pBase.LevelUpText2;
    }
}
