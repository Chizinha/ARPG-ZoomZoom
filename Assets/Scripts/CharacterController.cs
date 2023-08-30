using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent player;
    public GameObject targetDest;
    //public GameObject aimPoint;

    [SerializeField]
    private GameObject pfProjectile;
    [SerializeField]
    private float attackSpeed;

    private float attackTimer = -9999f;

    private ProjectilePool projectilePool;

    private void Awake()
    {
        projectilePool = GetComponent<ProjectilePool>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                targetDest.transform.position = raycastHit.point;
                player.SetDestination(raycastHit.point);
            }
        }

        if (Input.GetMouseButton(1))
        {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                player.ResetPath();
                transform.LookAt(new Vector3(raycastHit.point.x, transform.position.y, raycastHit.point.z));
                
                if (Time.time > attackTimer + attackSpeed)
                {
                    //GameObject projectile = Instantiate(pfProjectile, aimPoint.transform.position, Quaternion.identity);
                    //Vector3 projDirection = aimPoint.transform.up;
                    //projectile.GetComponent<Projectile>().Setup(projDirection);
                    projectilePool.projPool.Get();

                    attackTimer = Time.time;
                }
            }
            
        }
    }
}
