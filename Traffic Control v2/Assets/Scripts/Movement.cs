using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public Transform startMarker;
	public Transform endMarker;
    public Transform originalEnd;
	public float speed = 1.0F;
    public enum End { Destroy, Stop };
    public End onEndReached;
    private float startTime;

	private float journeyLength;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }

    void DestroyGameObject(){
		Destroy(gameObject);
	}

	void DestroyScriptInstance(){
		Destroy(this); 
	}

	void DestroyComponent()
	{
		// Removes the rigidbody from the game object
		Destroy(GetComponent<Rigidbody>());
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "End")
        {
            if(onEndReached == End.Destroy)
            {
                Destroy(GetComponent<SphereCollider>());
                Destroy(gameObject);
            }else if(onEndReached == End.Stop)
            {
                //originalEnd = endMarker;
                //endMarker = other.transform;
                //tag = "Stop";
            }
            
        }
    }

    public void RemoveObject()
    {
        Destroy(GetComponent<SphereCollider>());
        Destroy(gameObject);
    }
    public void SetEndMarker(Transform marker)
    {
        endMarker = marker;
    }

    // Update is called once per frame
    void Update()
    {
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
    }
}
