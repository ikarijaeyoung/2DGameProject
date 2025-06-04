using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRange : MonoBehaviour
{
    [SerializeField] private GameObject talkUI;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            talkUI.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            talkUI.SetActive(false);
        }
    }
}
