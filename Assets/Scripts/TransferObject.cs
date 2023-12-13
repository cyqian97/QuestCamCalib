using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Vuforia;


public class TransferObject : MonoBehaviour
{
    public GameObject model;
    public GameObject detection;
    // public TMP_Text debug_text;
    public GameObject debugger;
    public Vector3 position_offset;
    public Quaternion rotation_offset = Quaternion.identity;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            position_offset.z -= 0.01f;
        }
        if(detection.GetComponent<ObserverBehaviour>().TargetStatus.Status.ToString().Equals("TRACKED"))
        {
            // debug_text.text = "Model traget tracked";
            debugger.SetActive(true);
            model.SetActive(true);
            model.transform.position = detection.transform.position + position_offset;
            model.transform.rotation = detection.transform.rotation * rotation_offset;
        }
        else
        {
            model.SetActive(false);
            debugger.SetActive(false);
        }
    }
}
