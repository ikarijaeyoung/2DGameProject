using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoldUI : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text goldText;

    void Start()
    {
        UpdateGoldUI();
    }

    public void UpdateGoldUI()
    {
        if (goldText != null)
        {
            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                goldText.text = $"골드: {player.gold}";
            }
            else
            {
                Debug.LogWarning("Player 오브젝트를 찾을 수 없습니다.");
            }
        }
        else
        {
            Debug.LogWarning("goldText가 할당되지 않았습니다.");
        }
    }
}
