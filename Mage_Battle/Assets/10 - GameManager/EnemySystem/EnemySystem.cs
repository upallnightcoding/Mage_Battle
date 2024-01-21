using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private Transform player;

    private GameObject enemyPreFab;

    // Start is called before the first frame update
    void Start()
    {
        enemyPreFab = gameData.seleton1Bejar;

        CreateEnemy();
    }

    private void CreateEnemy()
    {
        Vector3 position = new Vector3(6.0f, 0.0f, 0.0f);

        GameObject seleton = Instantiate(enemyPreFab, position, Quaternion.identity);
        seleton.GetComponent<SkeletonCntrl>().Player = player;
    }
}
