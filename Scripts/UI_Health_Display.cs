using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Health_Display : MonoBehaviour
{
    public static UI_Health_Display instance;
    public List<GameObject> health;
    public GameObject heartPrefab;

    public GameObject hurtEffect;

    private Animator anim;
    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        anim = hurtEffect.GetComponent<Animator>();
    }
    public void UpdateHealth(float value)
    {
        while(health.Count < value)
        {
            AddHeart();
        }
        while(health.Count > value)
        {
            BreakHeart();
        }
    }

    public IEnumerator Hurt()
    {
        hurtEffect.SetActive(true);
        anim.Play("hurt", -1, 0);

        yield return new WaitForSeconds(1f);

        hurtEffect.SetActive(false);
    }

    public void AddHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab, transform.position, heartPrefab.transform.rotation);
        newHeart.transform.SetParent(transform);
        health.Add(newHeart);
    }

    public void BreakHeart()
    {
        GameObject brokenHeart = health[health.Count - 1];
        health.Remove(brokenHeart);

        brokenHeart.GetComponent<Animator>().Play("heart-break");

        StartCoroutine("Hurt");
    }
}
