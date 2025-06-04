using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class SlotUI : MonoBehaviour
{
    [SerializeField] public GameObject slotPanel;
    public List<Button> slotButtons;
    public List<TMP_Text> slotTexts;
    public List<TMP_InputField> slotInputFields;
    public List<CanvasGroup> slotCanvasGroups;
    public int numberOfSlots = 3;
    public PlayerData[] DataSlots;
    [SerializeField] public GameObject slotOptionPanel;
    public int currentlySelectedSlotIndex = -1;

    private void Start()
    {
        slotPanel.SetActive(false);
        slotOptionPanel.SetActive(false);
        DataSlots = new PlayerData[numberOfSlots];
        LoadAllGameData();
    }
    public void OpenSlotPanel()
    {
        slotPanel.SetActive(true);
        LoadAllGameData();
    }
    public void CloseSlotPanel()
    {
        slotPanel.SetActive(false);
    }
    public void CloseSlotOptionPanel()
    {
        slotOptionPanel.SetActive(false);
    }
    public void LoadAllGameData()
    {
        for (int i = 0; i < numberOfSlots; i++)
        {
            DataSlots[i] = LoadGameData(i);
            UpdateSlotUI(i);
        }
    }
    public void UpdateSlotUI(int slotIndex)
    {
        PlayerData data = DataSlots[slotIndex];

        if (data.isSaved)
        {
            slotTexts[slotIndex].text = $"이름: {data.playerName}\t" +
                                        $"레벨: {data.level}\t" +
                                        $"골드: {data.gold}\n" +
                                        $"마지막 저장: {data.lastSavedTime}";

            slotInputFields[slotIndex].text = data.playerName;
            slotInputFields[slotIndex].interactable = false;

            slotCanvasGroups[slotIndex].alpha = 1f;
            slotCanvasGroups[slotIndex].interactable = true;
            slotCanvasGroups[slotIndex].blocksRaycasts = true;
        }
        else
        {
            slotTexts[slotIndex].text = $"슬롯 {slotIndex + 1} (빈 슬롯)\n새 게임 시작";
            slotInputFields[slotIndex].text = "";
            slotInputFields[slotIndex].interactable = true;

            slotCanvasGroups[slotIndex].alpha = 0.5f;
            slotCanvasGroups[slotIndex].interactable = true;
            slotCanvasGroups[slotIndex].blocksRaycasts = true;
        }
    }
    public void HandleSlotButtonClick(int slotIndex)
    {
        currentlySelectedSlotIndex = slotIndex;

        slotOptionPanel.SetActive(true);
        
        PlayerData selectedData = DataSlots[slotIndex];

        Transform loadButtonTransform = slotOptionPanel.transform.Find("Play");
        Button loadButton = null;

        if (loadButtonTransform != null)
        {
            loadButton = loadButtonTransform.GetComponent<Button>();
        }

        if (loadButton != null)
        {
            TMP_Text loadButtonText = loadButton.GetComponentInChildren<TMP_Text>();
            loadButtonText.text = selectedData.isSaved ? "게임 불러오기" : "새 게임 시작";
            
        }
    }
    public void LoadSelectedGame()
    {
        PlayerData dataToLoad = DataSlots[currentlySelectedSlotIndex];
        string inputName = "";

        inputName = slotInputFields[currentlySelectedSlotIndex].text;
        
        if (!dataToLoad.isSaved)
        {
            Debug.Log($"슬롯 {currentlySelectedSlotIndex + 1} 새 게임 시작.");
            dataToLoad.InitializeNewGame(currentlySelectedSlotIndex, inputName);
            SaveGameData(currentlySelectedSlotIndex, dataToLoad);
            DataSlots[currentlySelectedSlotIndex] = dataToLoad;
        }
        else
        {
            Debug.Log($"슬롯 {currentlySelectedSlotIndex + 1} 게임 불러오기.");
        }

        GameManager.Instance.SetGameDataFromSlotUI(dataToLoad);
    
        CloseSlotPanel();
        CloseSlotOptionPanel();

        SceneManager.LoadScene("GameHomeScene");
    }

    public void DeleteSelectedSlot()
    {
        DeleteSlotData(currentlySelectedSlotIndex);
        Debug.Log($"슬롯 {currentlySelectedSlotIndex + 1} 데이터 삭제 완료!");

        CloseSlotOptionPanel();
    }

    public void CancelSlotOption()
    {
        CloseSlotOptionPanel();
    }

    public void SaveGameData(int slotIndex, PlayerData dataToSave)
    {
        GameManager.Instance.SaveGameDataToFile(slotIndex, dataToSave);
        Debug.Log($"슬롯 {slotIndex + 1} 데이터 저장 요청 완료! (SlotUI -> GameManager)");
        UpdateSlotUI(slotIndex);
    }
    public PlayerData LoadGameData(int slotIndex)
    {
        return GameManager.Instance.LoadGameDataFromFile(slotIndex);
    }
    public void DeleteSlotData(int slotIndex)
    {
        string filePath = Application.persistentDataPath + "/gamesave_" + slotIndex + ".json";
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            DataSlots[slotIndex] = new PlayerData();
            UpdateSlotUI(slotIndex); 
            Debug.Log($"슬롯 {slotIndex + 1} 데이터 삭제 완료! (JSON)");
        }
    }
}