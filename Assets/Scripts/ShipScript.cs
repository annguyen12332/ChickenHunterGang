using System.Collections;
using UnityEngine;

public class ShipScripts : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;

    [Header("Shooting")]
    [SerializeField] private GameObject[] BulletList;
    [SerializeField] private int CurrentTierBullet = 0;

    [Header("Effects")]
    [SerializeField] private GameObject VFX;
    [SerializeField] private GameObject Shield;

    [Header("Bomb Skill")]
    public SkillUI bombSkillUI;
    public GameObject bombExplosionPrefab;

    private Camera mainCam;
    private Vector3 screenLimit;

    void Start()
    {
        mainCam = Camera.main;
        screenLimit = mainCam.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, 0));

        StartCoroutine(DisableShield());

        // Unity mới
        if (bombSkillUI == null)
        {
            bombSkillUI = FindFirstObjectByType<SkillUI>();
        }
    }

    void Update()
    {
        Move();
        Fire();
        HandleBombSkill();
    }

    // ================= MOVE =================
    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(x, y, 0);
        transform.position += movement * speed * Time.deltaTime;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -screenLimit.x, screenLimit.x),
            Mathf.Clamp(transform.position.y, -screenLimit.y, screenLimit.y),
            0
        );
    }

    // ================= FIRE =================
    void Fire()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            && BulletList != null
            && BulletList.Length > CurrentTierBullet
            && BulletList[CurrentTierBullet] != null)
        {
            Instantiate(BulletList[CurrentTierBullet],
                        transform.position,
                        Quaternion.identity);
        }
    }

    // ================= BOMB =================
    void HandleBombSkill()
    {
        if (Input.GetKeyDown(KeyCode.E)
            && bombSkillUI != null
            && !bombSkillUI.IsOnCooldown())
        {
            UseBomb();
        }
    }

    void UseBomb()
    {
        bombSkillUI.StartCooldown();

        // Xóa gà
        ChickenScript[] chickens =
            FindObjectsByType<ChickenScript>(FindObjectsSortMode.None);

        foreach (ChickenScript chicken in chickens)
        {
            if (bombExplosionPrefab != null)
                Instantiate(bombExplosionPrefab,
                            chicken.transform.position,
                            Quaternion.identity);

            ScoreController.Instance?.GetScore(10);
            Destroy(chicken.gameObject);
        }

        // Xóa trứng
        GameObject[] eggs = GameObject.FindGameObjectsWithTag("egg");

        foreach (GameObject egg in eggs)
        {
            if (bombExplosionPrefab != null)
                Instantiate(bombExplosionPrefab,
                            egg.transform.position,
                            Quaternion.identity);

            Destroy(egg);
        }

    }

    // ================= COLLISION =================
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("egg") ||
            collision.CompareTag("chicken") ||
            collision.CompareTag("boss"))
        {
            if (Shield == null || !Shield.activeSelf)
            {
                GameManager.Instance?.LoseLife();

                if (VFX != null)
                {
                    var vfx = Instantiate(VFX,
                                          transform.position,
                                          Quaternion.identity);
                    Destroy(vfx, 1f);
                }

                Destroy(gameObject);
            }
        }
        else if (collision.CompareTag("chicken leg"))
        {
            Destroy(collision.gameObject);
            ScoreController.Instance?.GetScore(10);
        }
        else if (collision.CompareTag("heart"))
        {
            GameManager.Instance?.AddLife();
            Destroy(collision.gameObject);
        }
    }

    // ================= SHIELD =================
    IEnumerator DisableShield()
    {
        yield return new WaitForSeconds(5f);

        if (Shield != null)
            Shield.SetActive(false);
    }

    // ================= RESPAWN =================
    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.IsRestarting) return;

        if (GameManager.Instance.lives > 0)
        {
            ShipControllerScript.Instance?.SpawnShip();
        }
    }

    // ================= UPGRADE =================
    public void UpgradeToA3()
    {
        CurrentTierBullet = 2;
       
    }
}