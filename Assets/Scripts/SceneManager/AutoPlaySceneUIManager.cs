using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoPlaySceneUIManager : MonoBehaviour
{
    private void OnClickBackHomeButton()
    {
        // 현재 정보를 저장 하고,
        SceneManager.LoadScene("GameHomeScene");
    }
}
