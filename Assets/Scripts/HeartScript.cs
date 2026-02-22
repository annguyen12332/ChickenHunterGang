using UnityEngine;

public class HeartScript : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 2.5f;

    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("❤️ HEART VA CHẠM: " + collision.gameObject.name
                  + " | Tag của vật va chạm: " + collision.tag
                  + " | Layer: " + collision.gameObject.layer);

        if (collision.CompareTag("Player"))
        {
            Debug.Log("✅ ĂN THÀNH CÔNG HEART! +1 mạng");
            GameManager.Instance.AddLife();
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}