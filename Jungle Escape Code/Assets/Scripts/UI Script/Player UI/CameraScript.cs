using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    #region variables
    [SerializeField]
    private Transform followObject;
    private Vector3 tempPos;
    [SerializeField]
    private float posXLimit;
    [SerializeField]
    private float negXLimit;
    [SerializeField]
    private float posYLimit;
    [SerializeField]
    private float negYLimit;
    #endregion

    private void Start() 
    {
        tempPos.x = negXLimit;
        tempPos.y = 0;
        tempPos.z = -1;
        transform.position = tempPos;
    }

    private void LateUpdate() 
    {
        if(followObject.transform.position.x < posXLimit && followObject.transform.position.x > negXLimit)
        {
            tempPos = transform.position;
            tempPos.x = followObject.position.x;
            if(followObject.transform.position.y < posYLimit && followObject.transform.position.y > negYLimit)
            {
                tempPos.y = followObject.position.y;
            }

            transform.position = tempPos;
        }
    }
}
