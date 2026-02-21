using System.Collections;
using UnityEngine;

public class EggScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator aim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(CheckEggPosition());
    }

    IEnumerator CheckEggPosition()
    {
        while (true)
        {
            Vector3 vr3= Camera.main.WorldToViewportPoint(transform.position);
             if (vr3.y < 0.05)
            {
                aim.SetTrigger("break");
                rb.bodyType = RigidbodyType2D.Static;
                Destroy(gameObject, 1);
                yield break;
            }
           
            yield return null;
        }
    }

}
