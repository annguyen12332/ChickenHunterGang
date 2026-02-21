using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    private float gridSize = 1;
    private Vector3 spawnPos;
    [SerializeField] private GameObject chickenPrefab;
    [SerializeField] private Transform gridChicken;
    [SerializeField] private GameObject Boss;
    private int chickenCurrrent;

    public static SpawnScript Instance;
    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height;
        spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        spawnPos.x += ((gridSize / 2 + (width / 4)));
        spawnPos.y -= gridSize;
        spawnPos.z = 0;
        SpawnChicken(Mathf.FloorToInt(height / 2 / gridSize), Mathf.FloorToInt(width / gridSize / 1.5f));

    }

    void SpawnChicken(int row, int numberChicken)
    {
        float x = spawnPos.x;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < numberChicken; j++)
            {
                spawnPos.x = spawnPos.x + gridSize;
                GameObject chicken = Instantiate(chickenPrefab, spawnPos, Quaternion.identity);
                chicken.transform.parent = gridChicken;
                chickenCurrrent++;

            }
            spawnPos.x = x;
            spawnPos.y -= gridSize;
        }
    }

    public void DecreaseChicken()
    {
        chickenCurrrent--;
        if (chickenCurrrent <= 0)
            Boss.gameObject.SetActive(true);
    }
}
