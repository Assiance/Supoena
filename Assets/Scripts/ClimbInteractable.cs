using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbInteractable : XRGrabInteractable
{
    [SerializeField] private ClimbingProvider _climbingProvider;
    
    protected override void Awake()
    {
        base.Awake();
        FindClimbingProvider();
    }

    private void FindClimbingProvider()
    {
        if (_climbingProvider)
            _climbingProvider = FindObjectOfType<ClimbingProvider>();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        TryAdd(args.interactorObject);
    }

    private void TryAdd(IXRSelectInteractor interactor)
    {
        if (interactor.transform.TryGetComponent(out VelocityContainer velocityContainer))
            _climbingProvider.AddProvider(velocityContainer);

    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        TryRemove(args.interactorObject);
    }

    private void TryRemove(IXRSelectInteractor interactor)
    {
        if (interactor.transform.TryGetComponent(out VelocityContainer velocityContainer))
            _climbingProvider.RemoveProvider(velocityContainer);
    }

    public override bool IsHoverableBy(IXRHoverInteractor interactor)
    {
        return base.IsHoverableBy(interactor) && interactor is XRDirectInteractor;
    }

    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        return base.IsSelectableBy(interactor) && interactor is XRDirectInteractor;
    }
}
