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

    [Header("Pools")]
    [HideInInspector] public PoolingPattern moneyPool;

    void StartCreation()
    {
        moneyPool = new PoolingPattern(money);
        moneyPool.FillPool(20);
    }
}
