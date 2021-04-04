using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class SimonManager : XRBaseInteractable
{
    public UnityEvent OnPress = null;
    public UnityEvent OnFail = null;
    public GameObject[] buttons = new GameObject[6];

    public int currLevel = 0;
    public int curPos = 0;
    private float yMin = 0.0f;
    private float yMax = 0.0f;
    private bool previousPress = false;

    public int[][] Levels = new int[10][];
    private float previousHandHeight = 0.0f;
    private XRBaseInteractor hoverInteractor = null;

    protected override void Awake()
    {
        base.Awake();
        onHoverEntered.AddListener(StartPress);
        onHoverExited.AddListener(EndPress);
    }

    private void OnDestroy()
    {
        onHoverEntered.RemoveListener(StartPress);
        onHoverExited.RemoveListener(EndPress);
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
        System.Random rnd = new System.Random();
        for (int i = 0; i < 10; i++)
        {
            Levels[i] = new int[i+1];
            for (int j = 0; j < i+1; j++)
            {
                Levels[i][j] = rnd.Next(6);
            }
            Debug.Log(Levels[i]);
        }

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

            StartCoroutine(ColourPick());

            curPos = 0;
        }
        previousPress = inPosition;
    }

    private IEnumerator ColourPick()
    {
        yield return new WaitForSeconds(0.5f);

        foreach (var btn in buttons)
        {
            btn.GetComponent<SimonSaysButton>().SetSelectMat();
        }

        yield return new WaitForSeconds(0.5f);
        foreach (var btn in buttons)
        {
            btn.GetComponent<SimonSaysButton>().SetOrigtMat();
        }
        yield return new WaitForSeconds(0.5f);

        foreach (int i in Levels[currLevel])
        {
            buttons[i].GetComponent<SimonSaysButton>().SetSelectMat();
            yield return new WaitForSeconds(0.6f);
            buttons[i].GetComponent<SimonSaysButton>().SetOrigtMat();
            yield return new WaitForSeconds(0.3f);


        }
        // doing something
        // waits 5 seconds

        // do something else

    }
    public void Move(int idx)
    {
        if ((idx == Levels[currLevel][curPos]) && (curPos+1==Levels[currLevel].Length))
        {
            if (currLevel < 10)
                currLevel++;
            else
            {
                currLevel = 0;
            }

            curPos = 0;
            StartCoroutine(ColourPick());

            return;
        }

        if(idx == Levels[currLevel][curPos])
        {
            curPos++;
            return;
        }

        OnFail.Invoke();
        curPos = 0;
        StartCoroutine(ColourPick());



    }



    private bool InPosition()
    {
        float inRange = Mathf.Clamp(transform.localPosition.y, yMin, yMin + 0.01f);

        return transform.localPosition.y == inRange;
    }
}

