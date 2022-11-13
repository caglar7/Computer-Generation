using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    public void Activate()
    {
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

    private void OnTriggerEnter(Collider other)
    {
        // checking for level end boxes
    }
}
