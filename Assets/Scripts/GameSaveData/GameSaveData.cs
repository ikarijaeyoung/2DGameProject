using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class GameSaveData
{
    public string slotName;
    public bool isSaved;
    public string lastSavedTime;
    public string playerName;
    public int level;
    public int gold;
    public int health;
    public int attackPower;

    public void InitializeNewGame(int slotIndex, string nameInput) // nameInput 파라미터 추가
    {
        isSaved = true;
        slotName = $"슬롯 {slotIndex + 1}";
        level = 1;
        lastSavedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        playerName = string.IsNullOrEmpty(nameInput) ? $"모험가 {slotIndex + 1}" : nameInput; // 입력된 이름이 없으면 기본값 사용
        gold = 100;
        health = 100;
        attackPower = 10;
    }
}
