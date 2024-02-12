using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int health = 100;
    [SerializeField] TextMeshProUGUI healthText;

    private void Start()
    {
        updateHealthText();
    }

    private void Update()
    {
        if (health <= 0)
        {
            healthText.text = "Health: 0";
            SceneManager.LoadScene("LevelSelect");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health -= collision.gameObject.GetComponent<EnemyBrain>().damage;
            updateHealthText();
            Destroy(collision.gameObject);
        }
    }

    void updateHealthText()
    {
        healthText.text = "Health: " + health;
    }
}
