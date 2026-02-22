using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillUI : MonoBehaviour
{
    public Image skillImage;
    public float cooldown = 5.0f;
    private bool isCooldown = false;

    private void Awake()
    {
        // Automatically find the Image component on the same GameObject
        if (skillImage == null)
        {
            skillImage = GetComponent<Image>();
        }

        if (skillImage == null)
        {
            Debug.LogError("SkillUI script on " + gameObject.name + " is missing a reference to an Image component.");
        }
    }

    private void Start()
    {
        if (skillImage != null)
        {
            skillImage.fillAmount = 1; // Start with the skill ready (image full)
        }
    }

    // Call this method to start the skill cooldown
    public void StartCooldown()
    {
        if (skillImage == null) return; // Prevent error if not set up

        if (!isCooldown)
        {
            isCooldown = true;
            StartCoroutine(CooldownRoutine());
        }
    }

    private IEnumerator CooldownRoutine()
    {
        skillImage.fillAmount = 0; // Cooldown starts, image becomes empty
        float timer = 0;

        while (timer < cooldown)
        {
            timer += Time.deltaTime;
            skillImage.fillAmount = timer / cooldown; // Fill up from 0 to 1
            yield return null;
        }

        skillImage.fillAmount = 1; // Cooldown ends, image is full
        isCooldown = false;
    }

    public bool IsOnCooldown()
    {
        return isCooldown;
    }
}
