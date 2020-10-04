using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceWheel : MonoBehaviour
{
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

    private bool incurringRotation = false;

    // Start is called before the first frame update
    void Start()
    {
        LaunchLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LaunchLevel()
    {
        if (launchOnPlay)
        {
            StartCoroutine(MainRoutine());
        }
    }

    private void LoadLevel()
    {
        for (int i = 0; i < nbrSectionOnLoad; i++)
        {
            SpawnSectionToRotate(levelToLoad.listSections[i]);
            indexSection++;
        }
    }

    private void SpawnSectionToRotate(ScriptableSection scriptSection)
    {
        GameObject newObjSection = Instantiate(prefabSection, pivot);

        newObjSection.transform.position += spawnSection.transform.localPosition;
        newObjSection.transform.rotation = spawnSection.transform.rotation;

        Section newSection;
        newObjSection.TryGetComponent(out newSection);

        newSection.InitializeSection(scriptSection);
        newSection.LoadObstacles();

        RotatePivot();
        
    }

    

    private IEnumerator MainRoutine()
    {
        LoadLevel();

        for (int i = indexSection; i < levelToLoad.listSections.Count; i++)
        {
            SpawnSectionToRotate(levelToLoad.listSections[i]);

            yield return new WaitWhile(() => incurringRotation);

            yield return new WaitForSeconds(levelToLoad.sequenceDuration);
        }

    }

    private void RotatePivot()
    {
        incurringRotation = true;

        pivot.Rotate(new Vector3(360 / nbrSectionInCircle, 0, 0));

        incurringRotation = false;
    }
}
