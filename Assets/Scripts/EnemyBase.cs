using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵のデータ：外部からは変更しない
[CreateAssetMenu]
public class EnemyBase : ScriptableObject
{
    //名前、ステータス、画像
    
    [SerializeField] string enemyName;
    [SerializeField] string level;
    [SerializeField] int maxHP;
    [SerializeField] int attackMinDamage;
    [SerializeField] int attackMaxDamage;
    [SerializeField] int expMin;
    [SerializeField] int expMax;
    [SerializeField] AudioClip attackSE;
    [SerializeField] Sprite sprite;

    //他のファイルから値を取得できる
    public string EnemyName { get => enemyName; }
    public string Level { get => level; }
    public int AttackMinDamage {get => attackMinDamage; }
    public int MaxHP { get => maxHP; }
    public int AttackMaxDamage { get => attackMaxDamage; }
    public int ExpMin { get => expMin; }
    public int ExpMax { get => expMax; }
    public Sprite Sprite { get => sprite; }
    public AudioClip AttackSE { get => attackSE; }
}
