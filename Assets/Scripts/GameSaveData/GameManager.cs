using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerData CurrentGameData { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SaveCurrentGame()
    {
        if (CurrentGameData != null && CurrentGameData.isSaved)
        {
            CurrentGameData.lastSavedTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            
            SaveGameDataToFile(CurrentGameData.slotIndex, CurrentGameData); // GameManager가 직접 파일 저장
            Debug.Log($"게임 데이터 저장됨: 슬롯 {CurrentGameData.slotIndex + 1}");
        }
    }
    public void SetGameDataFromSlotUI(PlayerData data)
    {
        CurrentGameData = data;
        Debug.Log($"GameManager에 현재 게임 데이터 설정 완료: 슬롯 {CurrentGameData.slotIndex + 1}, 플레이어: {CurrentGameData.playerName}");
    }
    public PlayerData LoadGameDataFromFile(int slotIndex)
    {
        string filePath = Application.persistentDataPath + "/gamesave_" + slotIndex + ".json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            try
            {
                return JsonUtility.FromJson<PlayerData>(json);
            }
            catch (Exception e)
            {
                Debug.LogError($"슬롯 {slotIndex + 1} 데이터 로드 실패 (JSON 파싱 오류): {e.Message}. 파일을 삭제하고 새로운 데이터를 생성합니다.");
                File.Delete(filePath);
                return new PlayerData();
            }
        }
        return new PlayerData();
    }

    public void SaveGameDataToFile(int slotIndex, PlayerData dataToSave)
    {
        string jsonString = JsonUtility.ToJson(dataToSave, true);
        string filePath = Application.persistentDataPath + "/gamesave_" + slotIndex + ".json";
        File.WriteAllText(filePath, jsonString);
        Debug.Log($"슬롯 {slotIndex + 1} 데이터 파일 저장 완료!");
    }
}
