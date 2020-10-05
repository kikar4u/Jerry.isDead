using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class SpaceWheel : MonoBehaviour
{
    private static SpaceWheel thisOne;

    public static SpaceWheel Instance
    {
        get
        {
            if(thisOne == null)
            {
                thisOne = FindObjectOfType<SpaceWheel>();
            }
            return thisOne;
        }
    }

    [Header("Refs Prefab")]
    [SerializeField] private GameObject prefabSection;
    [Header("Refs obj utilitaires")]
    [SerializeField] private Transform spawnSection;
    [SerializeField] private Transform pivot;

    [Header("Préset Nivo")]
    public Campagne campagneToLoad;
    public bool launchOnPlay;
    public int indexCampagne = 0;
    [HideInInspector] public ScriptableLevel levelToLoad;


    private bool isLaunch;

    private int indexSection;
    [Header("Rotation")]
    public int nbrSectionOnLoad = 8;
    public float nbrSectionInCircle = 50;
    public float rotationDuration = 1;
    [HideInInspector] public bool breakRotation = false;

    private bool isRotating = false;

    [HideInInspector] public UnityEvent eventSequenceEnds = new UnityEvent();
    [HideInInspector] public UnityEvent eventSequenceBegins = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //pivot.eulerAngles = customPivotRotation;
    }

    public void Init()
    {
        ClearNivo();
        isRotating = false;
        levelToLoad = campagneToLoad.listLevels[indexCampagne];
        indexSection = 0;
        LaunchLevel();
    }

    private void LaunchLevel()
    {
        if (launchOnPlay && levelToLoad)
        {
            StartCoroutine(MainRoutine());
        }
    }

    private void LoadLevel()
    {
        for (int i = 0; i < nbrSectionOnLoad; i++)
        {
            SpawnSectionToRotate(levelToLoad.listSections[i], true);
            indexSection++;
        }
    }

    private void SpawnSectionToRotate(ScriptableSection scriptSection, bool instant)
    {
        GameObject newObjSection = Instantiate(scriptSection.prefabSection, pivot);

        newObjSection.transform.position += spawnSection.transform.localPosition;
        newObjSection.transform.rotation = spawnSection.transform.localRotation;

        Section newSection;
        newObjSection.TryGetComponent(out newSection);

        newSection.InitializeSection(scriptSection);
        newSection.LoadObstacles();

        if (instant)
            RotatePivot();
        else
            RotatePivot(rotationDuration);
    }

    

    private IEnumerator MainRoutine()
    {
        LoadLevel();

        yield return new WaitForSeconds(0.5f);

        for (int i = indexSection; i < levelToLoad.listSections.Count; i++)
        {
            eventSequenceBegins.Invoke();

            SpawnSectionToRotate(levelToLoad.listSections[i], false);

            yield return new WaitWhile(() => isRotating);

            eventSequenceEnds.Invoke();

            if (breakRotation)
            {
                break;
            }

            yield return new WaitForSeconds(0.5f);
        }

        for (int i = 0; i < nbrSectionOnLoad - 1; i++)
        {
            eventSequenceBegins.Invoke();

            RotatePivot(rotationDuration);

            yield return new WaitWhile(() => isRotating);

            eventSequenceEnds.Invoke();

            if (breakRotation)
            {
                break;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    private void RotatePivot()
    {
        isRotating = true;
        pivot.Rotate(new Vector3(0,0, -360 / nbrSectionInCircle));

        isRotating = false;
    }

    private void RotatePivot(float duration)
    {
        Vector3 targetRotation = pivot.eulerAngles;

        targetRotation.z -= 360 / nbrSectionInCircle;
        isRotating = true;
        pivot.DORotate(targetRotation, duration).OnComplete(() => isRotating = false);
    }


    private void ClearNivo()
    {
        Section[] sectionsToDelet = pivot.GetComponentsInChildren<Section>();

        foreach (Section section in sectionsToDelet)
        {
            Destroy(section.gameObject);
        }
    }
}
