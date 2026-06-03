using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour //Tom 
{

    public float maxHealth = 100f;
    public float currentHealth;
    public Slider healthBarSlider;

    public Transform vampireTransform;

    public float attackDistance = 0.5f; //te alcanza aunque haya un poquito de distancia para un poco más de dificultad

    public float damagePerSecond = 15f;
    public bool IsMaxHealth => currentHealth >= maxHealth; //Función pal Garlic


    public float cooldownTime = 1f; // El segundo entero de cooldown obligatorio
    private float cooldownTimer = 0f;

    public GameObject deathPopUp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        deathPopUp.SetActive(false);

        if (healthBarSlider != null)
        {
            healthBarSlider.maxValue = maxHealth;
            healthBarSlider.value = currentHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (vampireTransform == null)
        {
            GameObject vampireObject = GameObject.Find("Vampire");

            if (vampireObject != null)
            {
                vampireTransform = vampireObject.transform;
            }
        }
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (vampireTransform != null)
        {
            float distance = Vector2.Distance(transform.position, vampireTransform.position); //Hace daño un segundo, hace cooldown otro segundo

            if (distance <= attackDistance && cooldownTimer <= 0f)
            {
                TakeDamage(damagePerSecond); 
                cooldownTimer = cooldownTime; 
            }
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage; 

        if (currentHealth < 0f) //Para no dejar al player estar en números negativos 
        {
            currentHealth = 0f;
        }

        if (healthBarSlider != null)
        {
            healthBarSlider.value = currentHealth;
        }

        if (currentHealth == 0f)
        {
            Debug.Log("U died.");  //Zeeroo
            Death();
        }
    }
    public void Heal(float healing) //Garlic
    {
        currentHealth += healing;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        Debug.Log("Healed.");
    }

    void Death() //Zoe García
    {
        Time.timeScale = 0f;
        deathPopUp.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

