using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Gun : MonoBehaviour
{
    public InputActionReference leftTriggerButton;
    public InputActionReference rightTriggerButton;
    public XRRayInteractor leftRay;
    public XRRayInteractor rightRay;

    public GameObject bulletImpact;
    ParticleSystem bulletEffect;
    AudioSource bulletAudio;

    void Start()
    {
        leftTriggerButton.action.performed += OnLeftTriggerPressed;
        rightTriggerButton.action.performed += OnRightTriggerPressed;

        bulletEffect = bulletImpact.GetComponent<ParticleSystem>();
        bulletAudio = bulletImpact.GetComponent<AudioSource>();
    }

    void Update()
    {
    }

    void OnLeftTriggerPressed(InputAction.CallbackContext context)
    {
        bulletAudio.Stop();
        bulletAudio.Play();

        Vector3 hitPosition, hitNormal;
        if (leftRay.TryGetCurrent3DRaycastHit(out RaycastHit hitInfo))
        {
            bulletEffect.Stop();
            bulletImpact.transform.position = hitInfo.point;
            bulletImpact.transform.forward = hitInfo.normal;
            bulletEffect.Play();

            if (hitInfo.transform.name.Contains("Drone"))
            {
                DroneA1 drone = hitInfo.transform.GetComponent<DroneA1>();
                if (drone)
                {
                    drone.OnDamageProcess();
                }
            }
        }
    }

    void OnRightTriggerPressed(InputAction.CallbackContext context)
    {
        bulletAudio.Stop();
        bulletAudio.Play();

        Vector3 hitPosition, hitNormal;
        if (rightRay.TryGetCurrent3DRaycastHit(out RaycastHit hitInfo))
        {
            bulletEffect.Stop();
            bulletImpact.transform.position = hitInfo.point;
            bulletImpact.transform.forward = hitInfo.normal;
            bulletEffect.Play();

            if (hitInfo.transform.name.Contains("Drone"))
            {
                DroneA1 drone = hitInfo.transform.GetComponent<DroneA1>();
                if (drone)
                {
                    drone.OnDamageProcess();
                }
            }
        }
    }
}
