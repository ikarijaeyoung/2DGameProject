using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float fadeOutTime = 3f;
    private SpriteRenderer spriteRenderer;
    [SerializeField] public float attackSpeed;
    public int maxHP;
    public int curHP;
    public int attackDamage;
    public int level;
    public int gold;
    public string playerName;
    void Awake()
    {
        Init();
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Init()
    {
        PlayerData loadedData = GameManager.Instance.CurrentGameData;
        if (!loadedData.isSaved)
        {
            loadedData.InitializeNewGame(loadedData.slotIndex,loadedData.playerName);
        }
        else InitializePlayer(loadedData);
    }
    // PlayerData 객체에서 플레이어 스탯을 설정하는 새로운 메서드
    public void InitializePlayer(PlayerData data)
    {
        level = data.level;
        gold = data.gold;
        maxHP = data.maxHP;
        curHP = data.curHP;
        attackDamage = data.attackDamage;
        attackSpeed = data.attackSpeed;
        playerName = data.playerName;
    }
    
    public void TakeDamage(int damage)
    {
        curHP -= damage;
        if (curHP <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        StartCoroutine(WaitForDieAnimation());
    }
    private IEnumerator WaitForDieAnimation()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeOutAfterDeath());
    }
    private IEnumerator FadeOutAfterDeath()
    {
        float startAlpha = spriteRenderer.color.a;
        float elapsed = 0f;

        while (elapsed < fadeOutTime)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, 0f, elapsed / fadeOutTime);

            Color c = spriteRenderer.color;
            c.a = newAlpha;
            spriteRenderer.color = c;

            yield return null;
        }
        gameObject.SetActive(false);
    }
}
