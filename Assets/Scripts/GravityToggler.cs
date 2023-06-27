using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GravityToggler : MonoBehaviour
{
    private ClimbingProvider climbingProvider;
    private ContinuousMoveProviderBase moveProvider;
    private GrabMoveProvider[] grabMoveProviders;

    private void Awake()
    {
        climbingProvider = GetComponentInParent<ClimbingProvider>();
        moveProvider = GetComponentInParent<ContinuousMoveProviderBase>();
        grabMoveProviders = GetComponentsInChildren<GrabMoveProvider>();
    }

    private void OnEnable()
    {
        climbingProvider.beginLocomotion += DisableGravity;
        climbingProvider.endLocomotion += EnableGravity;

        foreach (var grabMoveProvider in grabMoveProviders)
        {
            grabMoveProvider.beginLocomotion += DisableGravity;
            grabMoveProvider.endLocomotion += EnableGravity;
        }
    }

    private void OnDisable()
    {
        climbingProvider.beginLocomotion -= DisableGravity;
        climbingProvider.endLocomotion -= EnableGravity;

        foreach (var grabMoveProvider in grabMoveProviders)
        {
            grabMoveProvider.beginLocomotion -= DisableGravity;
            grabMoveProvider.endLocomotion -= EnableGravity;
        }
    }

    private void EnableGravity(LocomotionSystem _) => ToggleGravity(true);

    private void DisableGravity(LocomotionSystem _) => ToggleGravity(false);

    private void ToggleGravity(bool value)
    {
        moveProvider.useGravity = value;
        foreach (var grabMoveProvider in grabMoveProviders)
        {
            grabMoveProvider.useGravity = value;
        }
    }
}
