using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class SimonSaysButton : XRBaseInteractable
{
    public UnityEvent OnPress = null;

    public Material selectMaterial = null;

    private MeshRenderer meshRenderer = null;
    private XRBaseInteractable interactable = null;
    private Material originalMaterial = null;

    private float yMin = 0.0f;
    private float yMax = 0.0f;
    private bool previousPress = false;


    private float previousHandHeight = 0.0f;
    private XRBaseInteractor hoverInteractor = null;

    protected override void Awake()
    {
        base.Awake();
        onHoverEntered.AddListener(StartPress);
        onHoverExited.AddListener(EndPress);
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.material;

        interactable = GetComponent<XRBaseInteractable>();
        interactable.onHoverEntered.AddListener(SetSelectMaterial);
        interactable.onHoverExited.AddListener(SetOriginalMaterial);
    }

    private void OnDestroy()
    {
        onHoverEntered.RemoveListener(StartPress);
        onHoverExited.RemoveListener(EndPress);
        interactable.onHoverEntered.RemoveListener(SetSelectMaterial);
        interactable.onHoverExited.RemoveListener(SetOriginalMaterial);
    }

    private void StartPress(XRBaseInteractor interactor)
    {
        hoverInteractor = interactor;
        previousHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
    }

    private void EndPress(XRBaseInteractor interactor)
    {
        hoverInteractor = null;
        previousHandHeight = 0.0f;

        previousPress = false;
        SetYPosition(yMax);
    }

    private void Start()
    {
        SetMinMax();
    }

    private void SetMinMax()
    {
        Collider collider = GetComponent<Collider>();

        yMin = transform.localPosition.y - (collider.bounds.size.y * 0.5f);
        yMax = transform.localPosition.y;

    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {

        if (hoverInteractor)
        {
            float newHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
            float handDiffenerce = previousHandHeight - newHandHeight;
            previousHandHeight = newHandHeight;

            float newPosition = transform.localPosition.y - handDiffenerce;
            SetYPosition(newPosition);

            CheckPress();
        }
    }

    private float GetLocalYPosition(Vector3 position)
    {
        Vector3 localPosition = transform.root.InverseTransformPoint(position);

        return localPosition.y;
    }

    private void SetYPosition(float position)
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.y = Mathf.Clamp(position, yMin, yMax);
        transform.localPosition = newPosition;
    }

    private void CheckPress()
    {
        bool inPosition = InPosition();

        if (inPosition && inPosition != previousPress)
        {
            OnPress.Invoke();

        }
        previousPress = inPosition;
    }

    private bool InPosition()
    {
        float inRange = Mathf.Clamp(transform.localPosition.y, yMin, yMin + 0.01f);

        return transform.localPosition.y == inRange;
    }





    private void SetSelectMaterial(XRBaseInteractor interactor)
    {
        meshRenderer.material = selectMaterial;
    }

    private void SetOriginalMaterial(XRBaseInteractor interactor)
    {
        meshRenderer.material = originalMaterial;
    }

    public void SetSelectMat()
    {
        meshRenderer.material = selectMaterial;
    }

    public void SetOrigtMat()
    {
        meshRenderer.material = originalMaterial;
    }
    //private IEnumerator ColourPick()
    //{
    //    // doing something
    //    meshRenderer.material = selectMaterial;
    //    // waits 5 seconds
    //    yield return new WaitForSeconds(0.3f);

    //    // do something else
    //    meshRenderer.material = originalMaterial;

    //}

    //public void Pick()
    //{
    //    StartCoroutine(ColourPick());
    //}
}
