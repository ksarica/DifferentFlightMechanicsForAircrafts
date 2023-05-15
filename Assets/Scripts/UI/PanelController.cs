using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PanelController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyGuideText;
    [SerializeField] private TextMeshProUGUI shipInfoText;

    [SerializeField] private GameObject monitoredShip;
    
    private ShipControllerPhysics shipControllerPhysics;

    void Start()
    {
        shipControllerPhysics = monitoredShip.GetComponent<ShipControllerPhysics>();
    }

    void Update()
    {
        shipInfoText.text = "Throttle: " + shipControllerPhysics.GetThrottle().ToString("F2") + " \n" + " Cruise Speed: " + shipControllerPhysics.GetCruiseSpeed().ToString("F2") + " \n";

        if (Input.GetKeyDown(KeyCode.Z))
        {
            keyGuideText.enabled = !keyGuideText.enabled;
        }
    }
}