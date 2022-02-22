using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusController
{

    [SerializeField] EnemyBase _base;
    public EnemyBase Base { get; set; } //ベースとなるデータ
    public int MaxHP { get; set; }
    public string Level { get; set; }
    public string EnemyName { get; set; }
    public int AttackMinDamage { get; set; }
    public int AttackMaxDamage { get; set; }
    public int ExpMin { get; set; }
    public int ExpMax { get; set; }
    public Sprite Sprite { get; set; }
    public AudioClip AttackSE { get; set; }


    public EnemyStatusController(EnemyBase eBase) //初期設定
    {
        Base = eBase;
        Level = eBase.Level;
        EnemyName = eBase.EnemyName;
        MaxHP = eBase.MaxHP;
        AttackMinDamage = eBase.AttackMinDamage;
        AttackMaxDamage = eBase.AttackMaxDamage;
        ExpMin = eBase.ExpMin;
        ExpMax = eBase.ExpMax;
        AttackSE = eBase.AttackSE;
        Sprite = eBase.Sprite;
    }

}
