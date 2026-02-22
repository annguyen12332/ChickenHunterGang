using UnityEngine;

public class DestroyIfReachDistances : MonoBehaviour
{
    [SerializeField] private float DistanceToDestroy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DistanceToDestroy = 10;
    }

    // Update is called once per frame
    void Update()
    {
        DestroyIfTrue();
    }

    void DestroyIfTrue()
    {
        Vector3 centerScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2), 0);
        Debug.Log(Vector3.Distance(transform.position, centerScreen));
        Debug.Log(DistanceToDestroy);
        if (Vector3.Distance(transform.position, centerScreen) > DistanceToDestroy)
            Destroy(this.gameObject);

    }
}
