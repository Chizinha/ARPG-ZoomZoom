using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    [SerializeField]
    private Enemy Prefab;

    public ObjectPool<Enemy> enemyPool;

    private void Awake()
    {
        enemyPool = new ObjectPool<Enemy>(CreatePooledObject, OnTakeFromPool, OnReturnToPool, OnDestroyObject, false, 10, 50);
    }

    private Enemy CreatePooledObject()
    {
        Enemy instance = Instantiate(Prefab, Vector3.zero, Quaternion.identity);
        instance.Disable += ReturnObjectToPool;
        instance.gameObject.SetActive(false);

        return instance;
    }

    private void ReturnObjectToPool(Enemy Instance)
    {
        enemyPool.Release(Instance);
    }

    private void OnTakeFromPool(Enemy Instance)
    {

        Instance.gameObject.SetActive(true);
        Instance.Setup();
    }

    private void OnReturnToPool(Enemy Instance)
    {
        Instance.gameObject.SetActive(false);
    }

    private void OnDestroyObject(Enemy Instance)
    {
        Destroy(Instance.gameObject);
    }

    
}