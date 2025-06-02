using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameInitializer : MonoBehaviour
{
    public TMP_Text gameStatusText;
    public Button saveButton;

    void Start()
    {
        // GameManager에서 로드된 데이터를 가져와 UI만 업데이트
        // if (GameManager.Instance != null && GameManager.Instance.CurrentGameData != null)
        // {
        //     PlayerData loadedData = GameManager.Instance.CurrentGameData;

        //     gameStatusText.text = $"게임 {(loadedData.isSaved ? "로드됨" : "시작됨")}!\n" +
        //                           $"이름: {loadedData.playerName}\n" +
        //                           $"레벨: {loadedData.level}\n" +
        //                           $"골드: {loadedData.gold}\n" +
        //                           $"체력: {loadedData.curHP}/{loadedData.maxHP}\n" +
        //                           $"공격력: {loadedData.attackDamage}\n" +
        //                           $"공격 속도: {loadedData.attackSpeed}\n" +
        //                           $"마지막 저장: {loadedData.lastSavedTime}";
        // }

        saveButton.onClick.AddListener(SaveCurrentGame);
        
    }

    public void SaveCurrentGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SaveCurrentGame();
        }
        else
        {
            Debug.Log("GameManager 인스턴스를 찾을 수 없습니다. 게임을 저장할 수 없습니다.");
        }
    }
}
