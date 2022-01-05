using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotSpawner : MonoBehaviour
{

    private int initAmount = 10;
    private float plotSize = 30f;
    private float xPosLeft = -30f;
    private float xPosRight = 30f;
    private float lastZPos = 15f;
    public List<GameObject> plots;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < initAmount; i++)
        {
            SpawnPlots();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SpawnPlots()
    {
        GameObject plotLeft = plots[Random.Range(0, plots.Count - 1)];
        GameObject plotRight = plots[Random.Range(0, plots.Count - 1)];

        float zPos = lastZPos + plotSize;

        Instantiate(plotLeft, new Vector3(xPosLeft, 0.030f, zPos), plotLeft.transform.rotation);
        Instantiate(plotRight, new Vector3(xPosRight,  0.030f, zPos), new Quaternion(0,180,0,0));

        lastZPos += plotSize;
    }
}
