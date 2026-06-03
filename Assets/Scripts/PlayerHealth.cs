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
    public AudioClip ouch;
    public AudioClip health;
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

        if (ouch != null)
        {
            AudioSource.PlayClipAtPoint(ouch, transform.position);
        }

        if (currentHealth == 0f)
        {
            Debug.Log("U died.");  //Zeeroo
            Death();
        }
    }
    public void Heal(float healingPercentage) //Garlic
    {
        float healthPoints = maxHealth * (healingPercentage / 100f); //Curar un porcentaje, no un número exacto

        currentHealth += healthPoints;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (healthBarSlider != null) //que se vea en la barra de vida tmb
        {
            healthBarSlider.value = currentHealth;
        }

        if (health != null)
        {
            AudioSource.PlayClipAtPoint(health, transform.position);
        }

        Debug.Log("Healed. Current Health: " + currentHealth);
    }

    void Death() //Zoe García
    {
        Time.timeScale = 0f;
        deathPopUp.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

