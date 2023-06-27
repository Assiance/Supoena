using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbingProvider : LocomotionProvider
{
    [SerializeField] private CharacterController _characterController;

    private bool _isClimbing = false;
    private List<VelocityContainer> _activeVelocities = new List<VelocityContainer>();

    protected override void Awake()
    {
        base.Awake();
        FindCharacterController();
    }

    private void FindCharacterController()
    {
        if (_characterController)
            _characterController = system.xrOrigin.GetComponent<CharacterController>();
    }

    public void AddProvider(VelocityContainer velocityContainer)
    {
        if (!_activeVelocities.Contains(velocityContainer))
            _activeVelocities.Add(velocityContainer);
    }

    public void RemoveProvider(VelocityContainer velocityContainer)
    {
        if (_activeVelocities.Contains(velocityContainer))
            _activeVelocities.Remove(velocityContainer);
    }

    private void Update()
    {
        TryBeginClimb();

        if (_isClimbing)
            ApplyVelocity();

        TryEndClimb();
    }

    private void TryBeginClimb()
    {
        Debug.Log("Begin Climb: " + CanClimb());
        if (CanClimb() && BeginLocomotion())
            _isClimbing = true;
    }

    private void TryEndClimb()
    {
        Debug.Log("End Climb: " + CanClimb());
        if (!CanClimb() && EndLocomotion())
            _isClimbing = false;
    }

    private bool CanClimb()
    {
        return _activeVelocities.Count > 0;
    }

    private void ApplyVelocity()
    {
        Vector3 velocity = CollectControllerVelocity();
        Transform origin = system.xrOrigin.transform;

        velocity = origin.TransformDirection(velocity);
        velocity *= Time.deltaTime;

        if (_characterController)
        {
            _characterController.Move(-velocity);
        }

        else
            origin.position -= velocity;
    }

    private Vector3 CollectControllerVelocity()
    {
        var totalVelocity = Vector3.zero;

        foreach (var container in _activeVelocities)
            totalVelocity += container.Velocity;

        return totalVelocity;
    }
}
