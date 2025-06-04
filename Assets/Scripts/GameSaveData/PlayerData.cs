using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerData
{
    public string slotName;
    public int slotIndex;
    public bool isSaved;
    public string lastSavedTime;
    public string playerName;
    public int level {get; private set;}
    public int gold {get; private set; }
    public int maxHP {get; private set;}
    public int attackDamage {get; private set;}
    public float attackSpeed {get; private set;}
    public int curHP {get; private set;}
    public PlayerData()
    {
        isSaved = false;
        slotName = "새 게임";
        lastSavedTime = "데이터 없음";
        playerName = "없음";
        level = 0;
        gold = 0;
        maxHP = 0;
        attackDamage = 0;
        attackSpeed = 0f;
        curHP = 0;
        slotIndex = -1;
    }

    public void InitializeNewGame(int slotIndex, string nameInput) // nameInput 파라미터 추가
    {
        isSaved = true;
        slotName = $"슬롯 {slotIndex + 1}";
        level = 1;
        lastSavedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        playerName = string.IsNullOrEmpty(nameInput) ? $"모험가 {slotIndex + 1}" : nameInput;
        gold = 100;
        maxHP = 100;
        curHP = maxHP;
        attackDamage = 10;
        attackSpeed = 1f;
    }
    public void UpdateDataFromPlayer(int playerLevel, int playerGold, int playerMaxHP, int playerCurHP, int playerAttackDamage, float playerAttackSpeed, string currentPlayerName)
    {
        level = playerLevel;
        gold = playerGold;
        maxHP = playerMaxHP;
        attackDamage = playerAttackDamage;
        attackSpeed = playerAttackSpeed;
        curHP = playerCurHP;
        playerName = currentPlayerName;

        isSaved = true;
        lastSavedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
