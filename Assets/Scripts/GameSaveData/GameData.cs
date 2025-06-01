using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class GameData
{
    public string slotName;
    public bool isSaved;
    public string lastSavedTime;
    public string playerName;
    public int level {get; private set;}
    public int gold {get; private set; }
    public int maxHP {get; private set;}
    public int attackDamage {get; private set;}
    public float attackSpeed {get; private set;}
    public int curHP {get; private set;}

    public void InitializeNewGame(int slotIndex, string nameInput) // nameInput 파라미터 추가
    {
        isSaved = true;
        slotName = $"슬롯 {slotIndex + 1}";
        level = 1;
        lastSavedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        playerName = string.IsNullOrEmpty(nameInput) ? $"모험가 {slotIndex + 1}" : nameInput; // 입력된 이름이 없으면 기본값 사용
        gold = 100;
        maxHP = 100;
        attackDamage = 10;
        attackSpeed = 1f;
    }
    public void SaveData()
    {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        isSaved = true;
        lastSavedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        level = player.level;
        gold = player.gold;
        maxHP = player.maxHP;
        attackDamage = player.attackDamage;
        attackSpeed = player.attackSpeed;
        curHP = player.curHP;
        playerName = player.name;

    }
    public void LoadData()
    {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.level = level;
        player.gold = gold;
        player.maxHP = maxHP;
        player.attackDamage = attackDamage;
        player.attackSpeed = attackSpeed;
        player.curHP = curHP;
        player.name = playerName;
    }
}
