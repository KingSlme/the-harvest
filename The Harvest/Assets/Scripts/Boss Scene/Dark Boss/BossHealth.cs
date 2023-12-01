using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    float bossMaxHealth = 30f;
    float bossCurrentHealth = 30f;
    FireProjectile fireProjectile;
    [SerializeField] GameObject healthBar;
    TextMeshPro textMesh;

    void OnMouseOver() {
        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            fireProjectile.Fire();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        // For Seed Impact
        if(other.tag == "SeedProjectile") {
            bossCurrentHealth--;
            Destroy(other.gameObject);
        }
    }

    void Start()
    {
        fireProjectile = FindObjectOfType<FireProjectile>().GetComponent<FireProjectile>();
        textMesh = healthBar.GetComponent<TextMeshPro>();
    }

    void Update()
    {
        textMesh.text = bossCurrentHealth + "/" + bossMaxHealth;
        if(bossCurrentHealth <= 0) {
            SceneManager.LoadScene("YouWin");
        }
    }
}
