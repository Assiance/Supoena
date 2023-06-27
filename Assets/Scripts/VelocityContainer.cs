using UnityEngine;
using UnityEngine.InputSystem;

public class VelocityContainer : MonoBehaviour
{
    [SerializeField] private InputActionProperty _velocityInput;

    public Vector3 Velocity => _velocityInput.action.ReadValue<Vector3>();
}
