using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoPlaySceneUIManager : MonoBehaviour
{
    public void OnClickBackHomeButton()
    {
        SceneManager.LoadScene("GameHomeScene");
    }
}
