using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShipScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject[] BulletList;
    [SerializeField] private int CurrentTierBullet;
    [SerializeField] private GameObject VFX;
    [SerializeField] private GameObject Shield;

    public SkillUI bombSkillUI;
    public GameObject bombExplosionPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DisableShield());

        // Automatically find the SkillUI if it's not set
        if (bombSkillUI == null)
        {
            bombSkillUI = FindObjectOfType<SkillUI>();
            if (bombSkillUI == null)
            {
                Debug.LogError("ShipScript could not find a SkillUI object in the scene.");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        HandleBombSkill();
    }

    void HandleBombSkill()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (bombSkillUI != null && !bombSkillUI.IsOnCooldown())
            {
                UseBomb();
            }
        }
    }

    void UseBomb()
    {
        bombSkillUI.StartCooldown();

        // Find all chicken scripts
        ChickenScript[] chickens = FindObjectsOfType<ChickenScript>();
        foreach (ChickenScript chicken in chickens)
        {
            if (bombExplosionPrefab != null)
            {
                Instantiate(bombExplosionPrefab, chicken.transform.position, Quaternion.identity);
            }
            ScoreController.Instance.GetScore(10); // Assuming 10 points per chicken
            Destroy(chicken.gameObject);
        }

        // Find all egg game objects
        GameObject[] eggs = GameObject.FindGameObjectsWithTag("egg");
        foreach (GameObject egg in eggs)
        {
            if (bombExplosionPrefab != null)
            {
                Instantiate(bombExplosionPrefab, egg.transform.position, Quaternion.identity);
            }
            Destroy(egg);
        }
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(x, y, 0);
        transform.position = transform.position + movement * speed * Time.deltaTime;

        Vector3 TopLeftPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, TopLeftPoint.x * -1, TopLeftPoint.x),
            Mathf.Clamp(transform.position.y, TopLeftPoint.y * -1, TopLeftPoint.y)
        );
    }

    void Fire()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Fire Input Detected!");
            if (BulletList != null && BulletList.Length > CurrentTierBullet && BulletList[CurrentTierBullet] != null)
            {
                Instantiate(BulletList[CurrentTierBullet], transform.position, Quaternion.identity);
                Debug.Log("Bullet Instantiated!");
            }
            else
            {
                Debug.LogError("Ship cannot fire: BulletList is empty or Bullet Prefab is missing!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Shield.activeSelf && (collision.CompareTag("egg") || collision.CompareTag("chicken")))
            Destroy(this.gameObject);
        else if(collision.CompareTag("chicken leg"))
        {
            Destroy(collision.gameObject);
            ScoreController.Instance.GetScore(10);
        }
    }

    IEnumerator DisableShield()
    {
        yield return new WaitForSeconds(5);
        Shield.SetActive(false);
    }

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;

        if (GameManager.Instance != null && GameManager.Instance.IsRestarting)
            return;

        var vfx = Instantiate(VFX, transform.position, Quaternion.identity);
        Destroy(vfx, 1f);

        GameManager.Instance.DecrementLife();
    }
}
