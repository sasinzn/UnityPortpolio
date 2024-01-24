using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance;

    public enum BuffType
    {
        HP,
        EXP,
        ATK,
        SPD,
        DEF,
        Delay
    }

    [SerializeField]
    private uint level;
    [SerializeField]
    private uint exp;
    [SerializeField]
    private uint maxExp;
    [SerializeField]
    public uint score;

    public bool touch = true;

    private uint atk;
    private uint maxHp;
    private uint currentHp;

    private float speed;
    private float fireInterval;

    private float expBuff;
    private float atkBuff;
    private float hpBuff;
    private float speedBuff;
    private float defenceBuff;
    private float delayBuff;

    private uint expBuffLevel;
    private uint atkBuffLevel;
    private uint hpBuffLevel;
    private uint speedBuffLevel;
    private uint defenceBuffLevel;
    private uint delayBuffLevel;

    private uint selectCharacterNum = 0;

    private void Awake()
    {
        instance = this;

        level = 1;
        exp = 0;
        maxExp = 100 + (level * 50);
        score = 0;

        atk = 1;
        maxHp = 100;
        currentHp = (uint)(maxHp * (0.1f * level));
        speed = 10;
        fireInterval = 0.2f;

        expBuff = 1.0f;
        atkBuff = 1.0f;
        hpBuff = 1.0f;
        speedBuff = 1.0f;
        defenceBuff = 1.0f;

        expBuffLevel = 0;
        atkBuffLevel = 0;
        hpBuffLevel = 0;
        speedBuffLevel = 0;
        defenceBuffLevel = 0;
        touch = true;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        
    }

    public float GetExpPercent()
    {
        return (float)exp / (float)maxExp;
    }

    public uint GetLevel()
    {
        return level;
    }

    public uint GetScore()
    {
        return score;
    }

    public void SetScore(uint  score)
    {
        this.score += score;
    }

    public void GetExp(uint exp)
    {
        this.exp += (uint)(exp * expBuff);
        maxExp = 100 + (level * 50);

        if (this.exp >= maxExp)
            LevelUP();
    }

    public void GetHitDamage(uint  damage)
    {
        if (damage * defenceBuff < 1)
            this.currentHp -= 1;
        else
            this.currentHp -= (uint)(damage * defenceBuff);

        if (this.currentHp <= 0)
        {
            NetworkManager.instance.SendReaderBoard((int)GetScore());
            GameManager.instance.GameEnd();
        }
           
    }

    public uint GetAttackDamage()
    {
        return (uint)(this.atk * atkBuff);
    }

    public void LevelUP()
    {
        float hpPer = currentHp / (maxHp * (0.1f * level));
        this.level += 1;
        this.exp = 0;
        this.currentHp = (uint)((maxHp * (0.1f * level)) * hpPer);

        touch = false;
        UIManager.instance.UpgradePanelOnOff();
       
    }

    public void LevelUpWithItem()
    {
        this.level += 1;
        touch = false;
        UIManager.instance.UpgradePanelOnOff();
    }

    public void SelectChar(uint num)
    {
        this.selectCharacterNum = num;
    }

    public void BuffLevelUp(BuffType type)
    {
        switch (type)
        {
            case BuffType.EXP:
                this.expBuffLevel += 1;
                this.expBuff = expBuff + (0.1f * this.expBuffLevel);
                break;
            case BuffType.HP:
                this.hpBuffLevel += 1;
                this.hpBuff = hpBuff + (0.1f * this.hpBuffLevel);
                UpdateHP();
                break;
            case BuffType.ATK:
                this.atkBuffLevel += 1;
                this.atkBuff = atkBuff + (0.1f * this.atkBuffLevel);
                break;
            case BuffType.SPD:
                this.speedBuffLevel += 1;
                this.speedBuff = speedBuff + (0.1f * this.speedBuffLevel);
                break;
            case BuffType.DEF:
                this.defenceBuffLevel += 1;
                this.defenceBuff = defenceBuff - (0.1f * this.defenceBuffLevel);
                break;
            case BuffType.Delay:
                this.delayBuffLevel += 1;
                this.delayBuff = delayBuff * (1.0f - (0.1f * this.delayBuffLevel));
                break;

        }
    }

    private void UpdateHP()
    {
        maxHp = (uint)(maxHp * hpBuff);
        currentHp = (uint)(currentHp * hpBuff);
    }

    public void ReStart()
    {
        instance = this;

        level = 1;
        exp = 0;
        maxExp = 100 + (level * 50);
        score = 0;

        atk = 1;
        maxHp = 100;
        currentHp = (uint)(maxHp * (0.1f * level));
        speed = 10;
        fireInterval = 0.2f;

        expBuff = 1.0f;
        atkBuff = 1.0f;
        hpBuff = 1.0f;
        speedBuff = 1.0f;
        defenceBuff = 1.0f;

        expBuffLevel = 0;
        atkBuffLevel = 0;
        hpBuffLevel = 0;
        speedBuffLevel = 0;
        defenceBuffLevel = 0;
        touch = true;
    }
}
