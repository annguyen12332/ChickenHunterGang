using UnityEngine;

public class BulletBuffScript : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 2.8f;

    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))   
        {
            ShipScripts ship = other.GetComponent<ShipScripts>();
            if (ship != null)
            {
                ship.UpgradeToA3();
               
            }
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}