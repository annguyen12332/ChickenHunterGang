using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ShipControllerScript : MonoBehaviour
{
    public static ShipControllerScript Instance;
    [SerializeField] private GameObject ShipPrefabs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SpawnShip()
    {
        Vector3 spawnPos = Camera.main.ViewportToWorldPoint(
            new Vector3(0.5f, 0.1f, Camera.main.nearClipPlane)
        );

        spawnPos.z = 0f;

        var newShip = Instantiate(ShipPrefabs, spawnPos, Quaternion.identity);

        Vector3 targetPos = Camera.main.ViewportToWorldPoint(
            new Vector3(0.5f, 0.25f, Camera.main.nearClipPlane)
        );

        targetPos.z = 0f;

        StartCoroutine(MoveShipToPoint(newShip, targetPos));
    }


    IEnumerator MoveShipToPoint(GameObject ship, Vector3 target)
    {
        float duration = 1f;
        float time = 0f;

        Vector3 startPos = ship.transform.position;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            ship.transform.position = Vector3.Lerp(startPos, target, t);
            yield return null;
        }

        ship.transform.position = target;
    }
}
