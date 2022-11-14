using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.NiceVibrations;

public class ComputerLevelEnd : MonoBehaviour
{
    #region Singleton

    public static ComputerLevelEnd instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    #endregion

    [SerializeField] GameObject computer;
    [SerializeField] Transform pointCamera;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float cameraAdjustTime = 1f;
    [SerializeField] Ease cameraAdjustEase = Ease.Linear;

    // components
    Collider collider;

    // togges
    bool isMoving = false;

    private void Start()
    {
        collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if(isMoving)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
    }

    public void Activate(int moneyAdded)
    {
        // haptic 
        MMVibrationManager.Haptic(HapticTypes.Success);

        // revenue update
        addedRevenue = moneyAdded;

        // camera set parent
        Transform objCamera = Camera.main.transform;
        objCamera.SetParent(pointCamera);

        // activate computer with effect
        computer.SetActive(true);

        // start moving upwards
        isMoving = true;

        // collider
        collider.enabled = true;

        // reposition camera
        objCamera.DOLocalMove(Vector3.zero, cameraAdjustTime);
        objCamera.DOLocalRotate(Vector3.zero, cameraAdjustTime)
            .OnComplete(() => {

             
            });
        
    }

    int addedRevenue = 0;
    private void OnTriggerEnter(Collider other)
    {
        LevelEndCube cube = other.GetComponent<LevelEndCube>();

        if (cube && cube.isStopPoint)
        {
            isMoving = false;

            // go to level end UI
            GameManager.instance.EndGame(2);

            // update revenue UI
            TotalMoneyUI.instance.UpdateRevenue(addedRevenue);

            // confetti
            ConfettiControl.instance.Blast();
        }
            
    }
}
