using UnityStandardAssets.Vehicles.Car;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CarWithLightController : CarUserControl {
    public GameObject lights;

    override protected void FixedUpdate()
    {
        base.FixedUpdate();
        if (Input.GetKeyDown(KeyCode.F))
            lights.SetActive(!lights.activeInHierarchy);
    }
}
