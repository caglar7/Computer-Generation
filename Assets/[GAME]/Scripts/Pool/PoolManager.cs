using UnityEngine;

public class PoolManager : MonoBehaviour
{
    #region Singleton
    public static PoolManager Instance = null;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        StartCreation();
    }
    #endregion

    [Header("Objects For Pooling")]
    public GameObject money;
    public GameObject moneyBlast;
    public GameObject smokeExplode;

    [Header("Pools")]
    [HideInInspector] public PoolingPattern moneyPool;
    [HideInInspector] public PoolingPattern moneyBlastPool;
    [HideInInspector] public PoolingPattern smokeExplodePool;

    void StartCreation()
    {
        moneyPool = new PoolingPattern(money);
        moneyPool.FillPool(20);

        moneyBlastPool = new PoolingPattern(moneyBlast);
        moneyBlastPool.FillPool(10);

        smokeExplodePool = new PoolingPattern(smokeExplode);
        smokeExplodePool.FillPool(7);
    }
}
