using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private float fadeOutTime = 2f;
    private SpriteRenderer spriteRenderer;
    [SerializeField] public float attackSpeed = 3f;
    public int maxHP = 10;
    public int curHP;
    public int attackDamage = 1;
    private Spawner spawner;
    private void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        curHP = maxHP;
    }
    public void TakeDamage(int damage)
    {
        curHP -= damage;
        if (curHP <= 0)
        {
            Die();
            Player player = FindObjectOfType<Player>();
            player.gold += 10;
            GameManager.Instance.UpdateCurrentGameData(player.level, player.gold, player.maxHP, player.curHP, player.attackDamage, player.attackSpeed, player.playerName);
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
        spawner.OnMonsterDied();
    }
}
