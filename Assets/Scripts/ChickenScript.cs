using System.Collections;
using UnityEngine;

public class ChickenScript : MonoBehaviour
{
    [SerializeField] private GameObject EggPrefabs;
    [SerializeField] private int score;
    [SerializeField] private GameObject ChickenLegPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        StartCoroutine(SpawmEgg());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            ScoreController.Instance.GetScore(score);
            Instantiate(ChickenLegPrefab, transform.position, Quaternion.identity);
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
