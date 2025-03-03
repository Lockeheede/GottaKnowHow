using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 30;
    [SerializeField] private Transform vfxHitDefault;

    [SerializeField] private Male_NPC male_NPC;
    [SerializeField] private ThirdPersonCombatController player;

    private void Awake()
    {
        male_NPC = GetComponent<Male_NPC>();
    }

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
        Destroy(vfxHitDefault.gameObject, 1f);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if(gameObject.GetComponent<Male_NPC>() != null)
        {
            male_NPC.KnockOut();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
