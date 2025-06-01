using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SlotUI : MonoBehaviour
{
    [SerializeField] public GameObject slotPanel;
    public List<Button> slotButtons;
    public List<TMP_Text> slotTexts;
    public List<TMP_InputField> slotInputFields;
    public List<CanvasGroup> slotCanvasGroups;
    public int numberOfSlots = 3;
    public GameData[] DataSlots;
    public static GameData currentLoadGameData;
    [SerializeField] public GameObject slotOptionPanel;
    public int currentlySelectedSlotIndex = -1;

    private void Start()
    {
        slotPanel.SetActive(false);
        DataSlots = new GameData[numberOfSlots];
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
    public GameData LoadGameData(int slotIndex)
    {
        string filePath = Application.persistentDataPath + "/gamesave_" + slotIndex + ".json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<GameData>(json);
        }
        return new GameData();
    }
    public void UpdateSlotUI(int slotIndex)
    {
        if (slotIndex < slotTexts.Count && slotButtons[slotIndex] != null)
        {
            GameData data = DataSlots[slotIndex];

            if (DataSlots[slotIndex].isSaved)
            {
                slotTexts[slotIndex].text = $"슬롯 {slotIndex + 1} (저장됨)\n" +
                                            $"이름: {data.playerName}\n" +
                                            $"레벨: {data.level}\n" +
                                            $"골드: {data.gold}\n" +
                                            $"체력: {data.maxHP}\n" +
                                            $"공격력: {data.attackDamage}\n" +
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
    }
    public void HandleSlotButtonClick(int slotIndex)
    {
        if (currentlySelectedSlotIndex == -1) return;

        currentlySelectedSlotIndex = slotIndex;

        GameData selectedData = DataSlots[slotIndex];

        slotOptionPanel.SetActive(true);

        Button deleteButton = slotOptionPanel.transform.Find("OptionPanel/DeleteSlotBtn").GetComponent<Button>();
        if (deleteButton != null)
        {
            deleteButton.interactable = selectedData.isSaved;

            TMP_Text deleteButtonText = deleteButton.GetComponentInChildren<TMP_Text>();
            if (deleteButtonText != null)
            {
                deleteButtonText.text = selectedData.isSaved ? "슬롯 삭제" : "삭제 불가";
            }
        }
    }
    public void LoadSelectedGame()
    {
        GameData dataToLoad = DataSlots[currentlySelectedSlotIndex];
        string inputName = "";

        if (currentlySelectedSlotIndex < slotInputFields.Count && slotInputFields[currentlySelectedSlotIndex] != null)
        {
            inputName = slotInputFields[currentlySelectedSlotIndex].text;
        }

        if (dataToLoad.isSaved)
        {
            Debug.Log($"슬롯 {currentlySelectedSlotIndex + 1} 게임 불러오기.");
            currentLoadGameData = dataToLoad;
        }
        else
        {
            Debug.Log($"슬롯 {currentlySelectedSlotIndex + 1} 새 게임 시작.");
            GameData newGameData = new GameData();
            newGameData.InitializeNewGame(currentlySelectedSlotIndex, inputName);
            SaveGameData(currentlySelectedSlotIndex, newGameData);
            DataSlots[currentlySelectedSlotIndex] = newGameData;
            currentLoadGameData = newGameData;
        }

        CloseSlotPanel();
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameHomeScene");
    }

    public void DeleteSelectedSlot()
    {
        if (currentlySelectedSlotIndex == -1) return;

        DeleteSlotData(currentlySelectedSlotIndex);
        Debug.Log($"슬롯 {currentlySelectedSlotIndex + 1} 데이터 삭제 완료!");

        CloseSlotOptionCanvas();
    }

    public void CancelSlotOption()
    {
        CloseSlotOptionCanvas();
    }
    private void CloseSlotOptionCanvas()
    {
        if (slotOptionPanel != null)
        {
            slotOptionPanel.SetActive(false);
        }
    }
    public void SaveGameData(int slotIndex, GameData dataToSave)
    {
        string jsonString = JsonUtility.ToJson(dataToSave, true);
        string filePath = Application.persistentDataPath + "/gamesave_" + slotIndex + ".json";
        File.WriteAllText(filePath, jsonString);
        Debug.Log($"슬롯 {slotIndex + 1} 데이터 저장 완료! (JSON)");
        UpdateSlotUI(slotIndex);
    }
    public void DeleteSlotData(int slotIndex)
    {
        string filePath = Application.persistentDataPath + "/gamesave_" + slotIndex + ".json";
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            DataSlots[slotIndex] = new GameData();
            UpdateSlotUI(slotIndex);
            Debug.Log($"슬롯 {slotIndex + 1} 데이터 삭제 완료! (JSON)");
        }
    }
    public void ToGameHomeScene()
    {
        // 암전 효과를 위해 잠시 대기하는 코루틴 있어야함.
        // 지금 Player정보를 저장하고 GameHomeScene으로 저장된 정보를 넘겨줘야함.
        
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameHomeScene");
    }

}
