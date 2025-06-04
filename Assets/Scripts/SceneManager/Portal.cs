using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("플레이어 포탈");
                SceneManager.LoadScene("AutoPlayScene");
            }
            else
            {
                Debug.LogWarning("포탈에 충돌한 객체가 플레이어가 아닙니다.");
            }
        }
    }
}
