using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody bulletRidgidbody;

    [SerializeField] private Transform vfxHitNoTarget;
    [SerializeField] private int bulletDamage = 30;

    void Awake()
    {
        bulletRidgidbody = GetComponent<Rigidbody>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is cr
    void Start()
    {
        float moveSpeed = 40f;
        bulletRidgidbody.linearVelocity = transform.forward * moveSpeed;

    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if(other.GetComponent<EnemyHealth>() != null)
        {
            other.GetComponent<EnemyHealth>().TakeDamage(bulletDamage);
        } else {
            Instantiate(vfxHitNoTarget, transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
