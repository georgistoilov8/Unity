using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject[] startingPoints;
    public GameObject[] endPoints;
    private float nextActionTime = 0.0f;
    public float period = 1.0f;
    public float speed = 1.0f;
    public enum End {Destroy, Stop};
    public End onEndReached;
    private int pointsCount;
    // Start is called before the first frame update
    void Start()
    {
        pointsCount = startingPoints.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextActionTime)
        {
            nextActionTime += period;
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        int position = Random.Range(0, pointsCount);
        Transform newObject = Instantiate(objectToSpawn.transform, startingPoints[position].transform.position, Quaternion.identity);
        newObject.GetComponent<Movement>().startMarker = startingPoints[position].transform;
        newObject.GetComponent<Movement>().endMarker = endPoints[position].transform;
        newObject.GetComponent<Movement>().speed = speed;
    }
}
