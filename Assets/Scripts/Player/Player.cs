using System.Collections;
using System.Collections.Generic;
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
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Init()
    {
        
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
