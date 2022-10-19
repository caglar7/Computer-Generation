using UnityEngine;
using System.Collections;
using DG.Tweening;
//using DG.Tweening;

namespace Mechanics
{
    public class SwipeMecLast : MonoBehaviour
    {
        #region Singleton
        public static SwipeMecLast instance = null;
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        #endregion

        [Header("Swipe Variables")]
        public bool posSwipe = true; // if the game only use swipe for position set initial value true ,  //for rotation set initial value false    
        public float clampMaxVal; // min value will be minus of max. 
        public float lerpMult = 1;//lerp speed adjuster
        public float mouseDamp = 600; //if you use rotation method set to 1 (suggested)
        public Transform obj; // obj to swipe

        [Header("Others")]
        private float startPosX;
        private float deltaMousePos;
        private float clampedAngle;
      
        bool isTouchScreen;

        private float resetTimer;
        [HideInInspector] public Vector3 desiredPos = Vector3.zero;

        public virtual void Start()
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                isTouchScreen = true;
                Input.multiTouchEnabled = false;
            }
            else
            {
                isTouchScreen = false;
            }

            RotationClampSettings();
        }

        public void VariableAdjust(Transform objTocontrol, float lerpMultiplier, float clampMax, float dampValue, bool posActive) // start ta çağır
        {
            clampMaxVal = clampMax;
            lerpMult = lerpMultiplier;
            posSwipe = posActive;
            obj = objTocontrol;
            mouseDamp = dampValue;         
        }

        void RotationClampSettings()
        {
            clampedAngle = 360 - clampMaxVal; // because of euler angles
        }


        public void Swipe() // her frameda çalışıyor
        {
            if (isTouchScreen)
            {
                TouchControl();
            }
            else
            {
                MouseControl();
            }
        }

        void MouseControl()
        {
            if (Input.GetMouseButtonDown(0))
            {
                ResetValues();
            }
            else if (Input.GetMouseButton(0))
            {
                ControlOnHold();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ControlOnMouseUp();
            }
        }

        void TouchControl()
        {
            switch (Input.touches[0].phase)
            {
                case TouchPhase.Began:
                    ResetValues();
                    break;

                case TouchPhase.Moved:
                    ControlOnHold();
                    break;

                case TouchPhase.Ended:
                    ControlOnMouseUp();
                    break;
            }
        }

        void ControlOnHold()
        {
            deltaMousePos = Input.mousePosition.x - startPosX;// how much mouse dragged

            if (posSwipe)//position swipe
            {
                PositionMethod2();
            }
            else ///rotation swipe
            {
                RotationMethod1();
            }
        }
        void ControlOnMouseUp() // write things u want to do after mouse up or touch ended 
        {
           // TO DO
        }

        public void ResetValues()
        {
            startPosX = Input.mousePosition.x;
        }

        void PositionMethod() // swipe
        {
            float xPos = obj.position.x;
            xPos = Mathf.Lerp(xPos, xPos + (mouseDamp * (deltaMousePos / Screen.width)), Time.deltaTime * lerpMult);
            xPos = Mathf.Clamp(xPos, -clampMaxVal, clampMaxVal);

            obj.position = new Vector3(xPos, obj.position.y, obj.position.z);
        }

        void PositionMethod2() // slide
        {
            float xPos = obj.position.x;
            xPos = Mathf.Lerp(xPos, xPos + (mouseDamp * (deltaMousePos / Screen.width)), Time.deltaTime * lerpMult);
            xPos = Mathf.Clamp(xPos, -clampMaxVal, clampMaxVal);

            obj.position = new Vector3(xPos, obj.position.y, obj.position.z);

            ResetValues();
        }

        void RotationMethod1()
        {
            float zRot = obj.transform.eulerAngles.y;
            zRot = Mathf.Lerp(zRot, zRot + (mouseDamp * deltaMousePos / Screen.width), Time.deltaTime * lerpMult);

            if (zRot > 180 && zRot < clampedAngle)
            {
                zRot = clampedAngle;
            }
            else if (zRot < 180 && zRot >= clampMaxVal)
            {
                zRot = clampMaxVal;
            }

            obj.transform.eulerAngles = new Vector3(0, zRot, 0);

            resetTimer += Time.deltaTime;
            if (resetTimer >= .5f)
            {
                ResetValues();

                #region Rotation
                resetTimer = 0f;
                StartCoroutine(AutoTurnForward());
                #endregion
            }
        }

        void RotationMethod2()
        {
            float zRot = Mathf.Lerp(0, (360 * deltaMousePos / Screen.width), Time.deltaTime * lerpMult);
            zRot = Mathf.Clamp(zRot, -clampMaxVal, clampMaxVal);
            obj.transform.eulerAngles -= new Vector3(0, -zRot, 0);
        }


        IEnumerator AutoTurnForward()
        {
            yield return null;
            obj.DORotate(Vector3.zero, .6f);
        }
    }
}

