using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 1.5f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private InteractionPromptUI _interactionPromptUI;
    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;

    private IInteractable _interactable;

    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        if (_numFound > 0)
        {
            _interactable = _colliders[0].GetComponent<IInteractable>();
            if (_interactable != null)
            {
                if (!_interactionPromptUI.IsDisplayed)
                {
                    _interactionPromptUI.SetUp(_interactable.InteractionPrompt);
                }

                if (Input.GetKeyDown(KeyCode.X))
                {
                    _interactable.Interact(this);
                }
            }
            else
            {
                _interactable = null;
                if (_interactionPromptUI.IsDisplayed)
                {
                    _interactionPromptUI.Close();
                }
            }
        }
        else
        {
            if (_interactable != null)
            {
                _interactable = null;
            }
            if (_interactionPromptUI.IsDisplayed)
            {
                _interactionPromptUI.Close();
            }
        }
    }
}
