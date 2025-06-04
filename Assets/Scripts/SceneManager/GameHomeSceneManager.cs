using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHomeSceneManager : MonoBehaviour
{
    public void OnClickTitleButton()
    {
        Player player = FindObjectOfType<Player>();
        GameManager.Instance.UpdateCurrentGameData(player.level, player.gold, player.maxHP, player.curHP, player.attackDamage, player.attackSpeed, player.playerName);
        GameManager.Instance.SaveCurrentGame();
        SceneManager.LoadScene("TitleScene");
    }
}
