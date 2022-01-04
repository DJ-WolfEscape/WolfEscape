using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public List<GameObject> roads;
    private float offset = 30f;

    void Start()
    {
        if (roads != null && roads.Count > 0)
        {
            roads = roads.OrderBy(r => r.transform.position.z).ToList();
        }

    }
    public void MoveRoads()
    {
        GameObject moveroad = roads[0];
        GameObject road = roads[0].gameObject;
        Destroy(road);
        roads.Remove(moveroad);
        GameObject x = Instantiate(moveroad);

        float newZ = roads[roads.Count - 1].transform.position.z + offset;
        x.transform.position = new Vector3(0, 0, newZ);
        roads.Add(x);
    }
}
