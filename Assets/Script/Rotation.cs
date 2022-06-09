using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public Quaternion startQ;
    public float lerpTime = 1;
    public float RotateAmount = 1;

    public bool rotate;
    public bool rotationCon;

    // Start is called before the first frame update
    void Start()
    {
        startQ = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(rotate){
            transform.rotation = Quaternion.Lerp(transform.rotation, startQ, Time.deltaTime * lerpTime);
        }
        if(rotationCon && !rotate)
        {
            transform.Rotate(Vector3.up * RotateAmount);
        }
    }

    public void snapRotation()
    {
        transform.rotation = startQ;
    }
}