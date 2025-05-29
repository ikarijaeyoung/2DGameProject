using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private int spawnCount = 3;
    private int spawnedMonsterCount = 0;

    private void Start()
    {
        SpawnMonster();
    }
    public void OnMonsterDied()
    {
        if (spawnedMonsterCount >= spawnCount) return;
        SpawnMonster();
    }
    public void SpawnMonster()
    {
        GameObject monster = Instantiate(monsterPrefab, transform.position, Quaternion.identity);
        spawnedMonsterCount++;
    }
    
    // 이미 같은 몬스터가 죽어서 비활성화가 되어있고, 다시 스폰(활성화)를 위한 함수. 아직 어떻게 해야할지 고민중.
    // public void RecycleMonster(GameObject monster)
    // {
    //     monster.transform.position = transform.position;
    //     monster.SetActive(true);
    //     spawnedMonsterCount++;
    // }
}
