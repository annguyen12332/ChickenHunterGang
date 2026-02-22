using System.Collections;
using UnityEngine;

public class ChickenScript : MonoBehaviour
{
    [SerializeField] private GameObject EggPrefabs;
    [SerializeField] private int score;
    [SerializeField] private GameObject ChickenLegPrefab;
    [SerializeField] private GameObject HeartPrefab;        // ← KÉO HEART PREFAB VÀO ĐÂY
    [SerializeField] private float heartDropChance = 0.20f; // 20% = rất ít
    [SerializeField] private GameObject bulletBuffPrefab;           // ← KÉO PREFAB VÀO
    [SerializeField] private float bulletBuffChance = 0.075f;       // 7.5% - rất hiếm

    private void Awake()
    {
        StartCoroutine(SpawmEgg());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            ScoreController.Instance.GetScore(score);

            Instantiate(ChickenLegPrefab, transform.position, Quaternion.identity);

            // 20% chance nhả Heart
            if (Random.value < heartDropChance)
            {
                Instantiate(HeartPrefab, transform.position, Quaternion.identity);
            }
            // === THÊM ĐOẠN NÀY ===
            if (Random.value < bulletBuffChance)
            {
                Instantiate(bulletBuffPrefab, transform.position, Quaternion.identity);
                Debug.Log("🐔 GÀ DROP BUFF ĐẠN A3!");
            }

            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }

    IEnumerator SpawmEgg()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(4, 20));
            Instantiate(EggPrefabs, transform.position, Quaternion.identity);
        }
    }

    private void OnDestroy()
    {
        SpawnScript.Instance.DecreaseChicken();
    }
}