using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Floor : MonoBehaviour
{
    public InputActionReference leftTriggerButton;
    public InputActionReference rightTriggerButton;
    public XRRayInteractor leftRay;
    public XRRayInteractor rightRay;
    public GameObject crosshair;
    public GameObject rightCrosshair;

    
    public GameObject voxelFactory;
    public int voxelPoolSize = 20;
    public static List<GameObject> voxelPool = new List<GameObject>();
    private bool isLeftTriggerHeld = false;
    private bool isRightTriggerHeld = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftTriggerButton.action.started += OnLeftTriggerPressed;
        rightTriggerButton.action.started += OnRightTriggerPressed;
        leftTriggerButton.action.canceled += OnLeftTriggerReleased;
        rightTriggerButton.action.canceled += OnRightTriggerReleased;

        for (int i = 0; i < voxelPoolSize; i++)
        {
            GameObject voxel = Instantiate(voxelFactory);
            voxel.SetActive(false);
            voxelPool.Add(voxel);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ¿Þ¼Õ¿ë Crosshair ÃßÀû
        if (isLeftTriggerHeld)
        {
            Vector3 hitPosition, hitNormal;
            int hitIndex;
            bool isValid;

            if (leftRay.TryGetHitInfo(out hitPosition, out hitNormal, out hitIndex, out isValid) && isValid)
            {
                if (crosshair != null)
                {
                    crosshair.transform.position = hitPosition + hitNormal * 0.001f;
                    crosshair.transform.rotation = Quaternion.LookRotation(-hitNormal);
                    crosshair.SetActive(true);
                }
            }
            else
            {
                if (crosshair != null)
                    crosshair.SetActive(false);
            }
        }

       
        if (isRightTriggerHeld)
        {
            Vector3 hitPosition, hitNormal;
            int hitIndex;
            bool isValid;

            if (rightRay.TryGetHitInfo(out hitPosition, out hitNormal, out hitIndex, out isValid) && isValid)
            {
                if (rightCrosshair != null)
                {
                    rightCrosshair.transform.position = hitPosition + hitNormal * 0.001f;
                    rightCrosshair.transform.rotation = Quaternion.LookRotation(-hitNormal);
                    rightCrosshair.SetActive(true);
                }
            }
            else
            {
                if (rightCrosshair != null)
                    rightCrosshair.SetActive(false);
            }
        }
    }


    void OnLeftTriggerPressed(InputAction.CallbackContext context)
    {
        isLeftTriggerHeld = true;
        Debug.Log("Left Trigger Button Pressed!!!");

        Vector3 hitPosition, hitNormal;
        int hitLinePos;
        bool isValid;

        if (leftRay.TryGetHitInfo(out hitPosition, out hitNormal, out hitLinePos, out isValid) && isValid)
        {
            Debug.Log("Left hit info = " + hitPosition.ToString());

            if (crosshair != null)
            {
               
                crosshair.transform.position = hitPosition + hitNormal * 0.001f;

               
                crosshair.transform.rotation = Quaternion.LookRotation(-hitNormal);

               
                crosshair.SetActive(true);
            }
        }
    }


    void OnRightTriggerPressed(InputAction.CallbackContext context)
    {
        isRightTriggerHeld = true;
        Debug.Log("Right Trigger Button Pressed!!!");

        Vector3 hitPosition, hitNormal;
        int hitLinePos;
        bool isValid;
        if (rightRay.TryGetHitInfo(out hitPosition, out hitNormal, out hitLinePos, out isValid))
        {
            Debug.Log("Right hit info = " + hitPosition.ToString());
        }
    }

    void OnLeftTriggerReleased(InputAction.CallbackContext context)
    {
        isLeftTriggerHeld = false;
        Debug.Log("Left Trigger Button Released!!!");

        Vector3 hitPosition, hitNormal;
        int hitLinePos;
        bool isValid;
        if (leftRay.TryGetHitInfo(out hitPosition, out hitNormal, out hitLinePos, out isValid))
        {
            Debug.Log("Left hit info = " + hitPosition.ToString());

            if (voxelPool.Count > 0)
            {
                GameObject voxel = voxelPool[0];
                voxel.SetActive(true);
                voxel.transform.position = hitPosition + new Vector3(0, 0.15f, 0);
                voxelPool.RemoveAt(0);
            }

            // Crosshair ¼û±â±â
            if (crosshair != null)
            {
                crosshair.SetActive(false);
            }
        }
    }


    void OnRightTriggerReleased(InputAction.CallbackContext context)
    {
        isRightTriggerHeld = false;
        Debug.Log("Right Trigger Button Released!!!");

        Vector3 hitPosition, hitNormal;
        int hitLinePos;
        bool isValid;
        if (rightRay.TryGetHitInfo(out hitPosition, out hitNormal, out hitLinePos, out isValid))
        {
            Debug.Log("Right hit info = " + hitPosition.ToString());

            if (voxelPool.Count > 0)
            {
                GameObject voxel = voxelPool[0];
                voxel.SetActive(true);
                voxel.transform.position = hitPosition + new Vector3(0, 0.15f, 0);
                voxelPool.RemoveAt(0);
            }

           
            if (rightCrosshair != null)
            {
                rightCrosshair.SetActive(false);
            }
        }
    }

}
