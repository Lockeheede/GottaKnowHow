using UnityEngine;
using Unity.Cinemachine;
using StarterAssets;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine.InputSystem;
using Microsoft.Unity.VisualStudio.Editor;
using System.Runtime.CompilerServices;
using System;
public class ThirdPersonCombatController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    // [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;
    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;

    [SerializeField] private float attackRange = 2f;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private int punchDamage = 10;

    [SerializeField] private Animator animator;

    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            // debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

        if(starterAssetsInputs.aim) {
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1, Time.deltaTime * 10f));

          //Shoot while aiming
          if(starterAssetsInputs.shoot) {
            print("Shoot!");
                Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
                Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
                starterAssetsInputs.shoot = false;
            }
        } else {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0, Time.deltaTime * 10f));

            //Attack while not aiming
            if(starterAssetsInputs.attack1) {
                print("Attack!");

                thirdPersonController.Attack1();
                starterAssetsInputs.attack1 = false;

                //Detect enemeies in range
                Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayerMask);

                //Damage each enemy hit
                foreach(Collider enemy in hitEnemies) {
                    Debug.Log("Hit " + enemy.name);
                    enemy.GetComponent<EnemyHealth>().TakeDamage(punchDamage);
                }
            }
        }
        }
    }

    //Visualize attack range
    private void OnGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }


}
