using UnityEngine;
using UnityEngine.UI;

public class building : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public hpbar hpbar;
    bool exclusive = true;
    public bool destroyed;
    public Transform house, house_des;
    public ParticleSystem particle;
    

    void Start()
    {
        currentHealth = maxHealth;
        hpbar.SetMaxHealth(maxHealth);

        house.gameObject.SetActive(true);
        house_des.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }

        if (currentHealth > 30 ^ exclusive)
        {
            destroyed = true;
            exclusive = false;
        }

        if (destroyed)
        {
            Instantiate(particle, house_des.transform);
            house.gameObject.SetActive(false);
            house_des.gameObject.SetActive(true);
        }

        destroyed = false;
       
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        hpbar.SetHealth(currentHealth);
    }

    private void OnMouseDown()
    {
        TakeDamage(20);
    }


}
