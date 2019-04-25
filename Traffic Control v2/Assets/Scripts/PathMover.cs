using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PathMover : MonoBehaviour
{
    private Queue<Vector3> pathPoints = new Queue<Vector3>();
    private NavMeshAgent agent;
    private bool loaded;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        loaded = false;
    }

    private void SetPoints(IEnumerable<Vector3> points)
    {
        pathPoints = new Queue<Vector3>(points);
        
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<Bezier>().isReady && !loaded)
        {
            SetPoints(FindObjectOfType<Bezier>().positions);
            loaded = true;
        }
        UpdatePathing();
    }

    private void UpdatePathing()
    {
        if (ShouldSetDestination())
        {
            Vector3 point = pathPoints.Dequeue();
            agent.SetDestination(point);
        }
    }

    private bool ShouldSetDestination()
    {
        if(pathPoints.Count == 0)
        {
            return false;
        }

        if(agent.hasPath == false || agent.remainingDistance < 0.5f)
        {
            return true;
        }
        return false;
    }
}
