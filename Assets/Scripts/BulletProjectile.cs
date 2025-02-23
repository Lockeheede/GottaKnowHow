using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody bulletRidgidbody;

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
    }

    // Update is called once per frame
    void Update()
    {

    }
}
