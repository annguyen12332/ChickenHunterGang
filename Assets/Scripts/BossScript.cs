using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    [SerializeField] private GameObject EggPrefab;
    [SerializeField] private int hp;
    [SerializeField] private GameObject VFX;
    public static BossScript Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        StartCoroutine(SpawnEgg());
        StartCoroutine(MoveBossToRandomPoint());
    }

    public void PutDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Debug.Log("Boss Dead");

            StopAllCoroutines();

            int finalScore = ScoreController.Instance.GetScorePrint();

            Debug.Log("Final Score: " + finalScore);

            GameManager.Instance.WinGame(finalScore);

            var vfx = Instantiate(VFX, transform.position, Quaternion.identity);
            Destroy(vfx, 1);

            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator SpawnEgg()
    {
        while (true)
        {

            Instantiate(EggPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(0.0f, 0.8f));
        }
    }


    IEnumerator MoveBossToRandomPoint()
    {
        Vector3 point = getRandomPoint();
        while (transform.position != point)
        {
            transform.position = Vector3.MoveTowards(transform.position, point, 0.1f);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        StartCoroutine(MoveBossToRandomPoint());
    }

    Vector3 getRandomPoint()
    {
        Vector3 posRandom = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), Random.Range(0.5f, 1f)));
        posRandom.z = 0;
        return posRandom;
    }
}
