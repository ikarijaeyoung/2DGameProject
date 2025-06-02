using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHomeSceneManager : MonoBehaviour
{
    public void OnClickTitleButton()
    {
        GameManager.Instance.SaveCurrentGame();
        SceneManager.LoadScene("TitleScene");
    }
}
