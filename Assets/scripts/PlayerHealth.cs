using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public bool isInvisible = false;
    public float invisibilityTimeAfterDamage;
    public float invisibleDelay = 0.2f;


    public SpriteRenderer spriteRenderer;
    public HealthBar healthBar;

    
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isInvisible)
        {
            healthBar.SetHealth(currentHealth -= damage);
            isInvisible = true;
            StartCoroutine(InvisibilityFlash());
            StartCoroutine(HandleInvisibilityDelay());
        }
    }

    public IEnumerator InvisibilityFlash()
    {
        while (isInvisible)
        {
            spriteRenderer.color = new Color(1f,1f,1f,0f);
            yield return new WaitForSeconds(invisibleDelay);
            spriteRenderer.color = new Color(1f,1f,1f,1f);
            yield return new WaitForSeconds(invisibleDelay);
        }
    }

    public IEnumerator HandleInvisibilityDelay()
    {
        yield return new WaitForSeconds(invisibilityTimeAfterDamage);
        isInvisible = false;
    }
}
