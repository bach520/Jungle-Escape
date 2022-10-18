using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
    [SerializeField]
    private Transform aimTransfrom;
    [SerializeField]
    float adjustment = 0f;
    
    private void Update()
    {
        Vector3 mouse = GetMousePosition();

        Vector3 aimDirection = (mouse - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransfrom.eulerAngles = new Vector3(0, 0, angle + adjustment);
    }

    #region get mouse world position
    public Vector3 GetMousePosition()
    {
        Vector3 vec = GetMouseWorldWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public Vector3 GetMouseWorldWithZ(Vector3 screenPosition, Camera worldCam)
    {
        Vector3 worldPosition = worldCam.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
    #endregion
}
