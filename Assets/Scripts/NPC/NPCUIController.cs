using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCUIController : MonoBehaviour
{
    [SerializeField] private GameObject npcUI;

    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Z) && npcUI.activeSelf == false)
            {
                npcUI.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.Z) && npcUI.activeSelf == true)
            {
                npcUI.SetActive(false);
            }
        }
    }
    public void OnClickCloseButton()
    {
        npcUI.SetActive(false);
    }
}
