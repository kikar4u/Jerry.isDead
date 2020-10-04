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
    public ScriptableLevel levelToLoad;
    public bool launchOnPlay;

    private bool isLaunch;

    private int indexSection;

    public int nbrSectionOnLoad = 8;
    public float nbrSectionInCircle = 50;
    public float rotationDuration = 1;

    private bool isRotating = false;

    [HideInInspector] public UnityEvent eventSequenceEnds = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        LaunchLevel();
        
    }

    // Update is called once per frame
    void Update()
    {
        //pivot.eulerAngles = customPivotRotation;
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
        newObjSection.transform.rotation = spawnSection.transform.rotation;

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

        for (int i = indexSection; i < levelToLoad.listSections.Count; i++)
        {
            SpawnSectionToRotate(levelToLoad.listSections[i], false);

            yield return new WaitWhile(() => isRotating);

            eventSequenceEnds.Invoke();

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
}
