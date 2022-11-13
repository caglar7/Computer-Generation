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

    [Header("Pools")]
    [HideInInspector] public PoolingPattern moneyPool;
    [HideInInspector] public PoolingPattern moneyBlastPool;

    void StartCreation()
    {
        moneyPool = new PoolingPattern(money);
        moneyPool.FillPool(20);

        moneyBlastPool = new PoolingPattern(moneyBlast);
        moneyBlastPool.FillPool(10);
    }
}
