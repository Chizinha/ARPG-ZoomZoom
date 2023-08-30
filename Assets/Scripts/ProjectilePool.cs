using UnityEngine;
using UnityEngine.Pool;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField]
    private Projectile Prefab;
    [SerializeField]
    private GameObject aimPoint;
    
    public float projSpeed;
    public float projDuration;

    public ObjectPool<Projectile> projPool;

    private void Awake()
    {
        projPool = new ObjectPool<Projectile>(CreatePooledObject, OnTakeFromPool, OnReturnToPool, OnDestroyObject, false, 40, 500);
    }

    private Projectile CreatePooledObject()
    {
        Projectile instance = Instantiate(Prefab, Vector3.zero, Quaternion.identity);
        instance.Disable += ReturnObjectToPool;
        instance.gameObject.SetActive(false);

        return instance;
    }

    private void ReturnObjectToPool(Projectile Instance)
    {
        projPool.Release(Instance);
    }

    private void OnTakeFromPool(Projectile Instance)
    {
        Instance.gameObject.SetActive(true);
        SpawnProjectile(Instance);
    }

    private void OnReturnToPool(Projectile Instance)
    {
        Instance.gameObject.SetActive(false);
    }

    private void OnDestroyObject(Projectile Instance)
    {
        Destroy(Instance.gameObject);
    }

    private void SpawnProjectile(Projectile Instance)
    {
        
        Instance.transform.position = aimPoint.transform.position;

        Instance.Setup(aimPoint.transform.up, projSpeed, projDuration);
    }

}