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
    void Update()
    {
        
    }
    private void Init()
    {
        PlayerData loadedData = GameManager.Instance.CurrentGameData;
        if (!loadedData.isSaved)
        {
            loadedData.InitializeNewGame(loadedData.slotIndex, loadedData.playerName); // PlayerData의 정보를 Player에 덮어씌우기. (PlayerData => Player 갱신)
        }
        else InitializePlayer(loadedData);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateCurrentGameData(level, gold, maxHP, curHP, attackDamage, attackSpeed, playerName); // 업데이트 된 Player의 정보를 PlayerData에 덮어 씌우기. (갱신된 Player => PlayerData)
        }
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
        if (GameManager.Instance != null && GameManager.Instance.CurrentGameData != null)
        {
            GameManager.Instance.UpdateCurrentGameData(level, gold, maxHP, curHP, attackDamage, attackSpeed, playerName);
        }
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
