using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 30;
    [SerializeField] private Transform vfxHitDefault;


    public void SetHealth(int newHealth)
    {
        health = newHealth;
    }

    public int GetHealth()
    {
        return health;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        Instantiate(vfxHitDefault, transform.position, Quaternion.identity);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
