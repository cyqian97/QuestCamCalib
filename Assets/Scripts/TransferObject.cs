using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Vuforia;


public class TransferObject : MonoBehaviour
{
    public GameObject model;
    public GameObject detection;
    public float position_scale;
    public float rotation_scale;
    public Vector3 position_offset;
    public Quaternion rotation_offset;
    // Start is called before the first frame update
    void Start()
    {
        // rotation_offset = Quaternion.identity;

        // model.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            position_offset.z -= 0.002f;
        }
        if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            position_offset.z += 0.002f;
        }
        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp))
        {
            position_offset.y += 0.002f;
        }
        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown))
        {
            position_offset.y -= 0.002f;
        }
        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickRight))
        {
            position_offset.x += 0.002f;
        }
        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft))
        {
            position_offset.x -= 0.002f;
        }


        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            rotation_offset *= Quaternion.AngleAxis(-0.2f, Vector3.forward);
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            rotation_offset *= Quaternion.AngleAxis(0.2f, Vector3.forward);
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp))
        {
            rotation_offset *= Quaternion.AngleAxis(-0.2f, Vector3.right);
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown))
        {
            rotation_offset *= Quaternion.AngleAxis(0.2f, Vector3.right);
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight))
        {
            rotation_offset *= Quaternion.AngleAxis(-0.2f, Vector3.up);
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft))
        {
            rotation_offset *= Quaternion.AngleAxis(0.2f, Vector3.up);
        }


        // if (OVRInput.Get(OVRInput.Button.One))
        // {
        //     position_scale *= 1.01f;
        // }
        // if (OVRInput.Get(OVRInput.Button.Two))
        // {
        //     position_scale /= 1.01f;
        // }


        // if (OVRInput.Get(OVRInput.Button.Three))
        // {
        //     rotation_scale *= 1.01f;
        // }
        // if (OVRInput.Get(OVRInput.Button.Four))
        // {
        //     rotation_scale /= 1.01f;
        // }

        if (detection.GetComponent<ObserverBehaviour>().TargetStatus.Status.ToString().Equals("TRACKED"))
        {
            model.SetActive(true);

            Debug.Log(string.Format("position_scale: {0}", position_scale));
            Debug.Log(string.Format("pos_offset: {0}, {1}, {2}", position_offset.x, position_offset.y, position_offset.z));
            Debug.Log(string.Format("rot_offset: {0}, {1}, {2}, {3}", rotation_offset.x, rotation_offset.y, rotation_offset.z, rotation_offset.w));
            Debug.Log(string.Format("Detect pos: {0}, {1}, {2}", detection.transform.position.x, detection.transform.position.y, detection.transform.position.z));
            Debug.Log(string.Format("Detect rot: {0}, {1}, {2}, {3}", detection.transform.rotation.x, detection.transform.rotation.y, detection.transform.rotation.z, detection.transform.rotation.w));

            model.transform.localPosition = position_scale * (rotation_offset * detection.transform.position) + position_offset;


            // float angle = 0.0f;
            // Vector3 axis = Vector3.zero;
            // detection.transform.rotation.ToAngleAxis(out angle, out axis);
            // angle *= rotation_scale;
            // model.transform.localRotation = Quaternion.AngleAxis(angle, axis) * rotation_offset;
            model.transform.localRotation = rotation_offset*detection.transform.rotation;
        }
        else
        {
            model.SetActive(false);
        }
    }
}
