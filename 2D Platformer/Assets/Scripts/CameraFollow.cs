using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float cameraRange;
    public float cameraSpeed;
    public float smoothSpeed = 0.125f;
    public List<Transform> targets;
    Camera cam;
    
	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void FixedUpdate() {
        Follow();
        Zoom();
	}

    public void Follow()
    {
        if(targets.Count > 0)
        {
            //if(targets.Count == 1)
            {
                Vector3 targetPostion = targets[0].position;
                float nX = transform.position.x, nY = transform.position.y;

                //Target is to left of camera space
                if (targetPostion.x < (transform.position.x - cameraRange))
                    nX = transform.position.x + (targetPostion.x - (transform.position.x - cameraRange));

                //Target is to right of camera space
                else if (targetPostion.x > (transform.position.x + cameraRange))
                    nX = transform.position.x + (targetPostion.x - (transform.position.x + cameraRange));

                //Target is below camera space
                if (targetPostion.y < (transform.position.y - cameraRange))
                    nY = transform.position.y + (targetPostion.y - (transform.position.y - cameraRange));

                //Target is above camera space
                else if (targetPostion.y > (transform.position.y + cameraRange))
                    nY = transform.position.y + (targetPostion.y - (transform.position.y + cameraRange));

                
                //Multiple ways of doing camera follow.
                //transform.position = new Vector3(nX, nY, -10);
                Vector3 velocity = Vector3.zero;
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(nX, nY, -10), ref velocity, smoothSpeed, Mathf.Infinity, Time.smoothDeltaTime);
                //transform.position = Vector3.MoveTowards(transform.position, new Vector3(nX, nY, -10), cameraSpeed * Time.smoothDeltaTime);
                //transform.position = Vector3.Lerp(transform.position, new Vector3(nX, nY, -10), smoothSpeed);
            }
        }
    }

    void Zoom()
    {
        if(targets.Count > 1)
        {
            int maxTarget = 0, minTarget = 1;

            for(int i = 0; i < targets.Count; i++)
            {
                if (targets[maxTarget].transform.position.x < targets[i].transform.position.x)
                    maxTarget = i;

                if(targets[minTarget].transform.position.x > targets[i].transform.position.x)
                    minTarget = i;
            }

            cam.orthographicSize = 10 + ((targets[maxTarget].transform.position.x - targets[minTarget].transform.position.x) / 15);
        }
    }
}
