using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Vuforia;


public class TransferObject : MonoBehaviour
{
    public GameObject model;
    public GameObject detection;
    public class CalibData
    {
        public float z_scale;
        // public float rotation_scale;
        public Vector3 position_offset;
        public Quaternion direction_offset;
        public Quaternion rotation_offset;
    }

    public CalibData cdata;
    // Start is called before the first frame update
    void Start()
    {
        cdata = LoadCalibData("CameraCalib.json");
        // cdata.direction_offset = Quaternion.identity;

        // model.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            cdata.position_offset.z -= 0.002f;
        }
        if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            cdata.position_offset.z += 0.002f;
        }
        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp))
        {
            cdata.position_offset.y += 0.002f;
        }
        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown))
        {
            cdata.position_offset.y -= 0.002f;
        }
        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickRight))
        {
            cdata.position_offset.x += 0.002f;
        }
        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft))
        {
            cdata.position_offset.x -= 0.002f;
        }


        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            cdata.rotation_offset *= Quaternion.AngleAxis(-0.2f, Vector3.forward);
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            cdata.rotation_offset *= Quaternion.AngleAxis(0.2f, Vector3.forward);
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp))
        {
            cdata.rotation_offset *= Quaternion.AngleAxis(-0.2f, Vector3.right);
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown))
        {
            cdata.rotation_offset *= Quaternion.AngleAxis(0.2f, Vector3.right);
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight))
        {
            cdata.rotation_offset *= Quaternion.AngleAxis(-0.2f, Vector3.up);
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft))
        {
            cdata.rotation_offset *= Quaternion.AngleAxis(0.2f, Vector3.up);
        }


        if (OVRInput.Get(OVRInput.Button.One))
        {
            cdata.z_scale *= 1.01f;
        }
        if (OVRInput.Get(OVRInput.Button.Two))
        {
            cdata.z_scale /= 1.01f;
        }


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

            Debug.Log(string.Format("cdata.z_scale: {0}", cdata.z_scale));
            Debug.Log(string.Format("pos_offset: {0}, {1}, {2}", cdata.position_offset.x, cdata.position_offset.y, cdata.position_offset.z));
            Debug.Log(string.Format("dir_offset: {0}, {1}, {2}, {3}", cdata.direction_offset.x, cdata.direction_offset.y, cdata.direction_offset.z, cdata.direction_offset.w));
            Debug.Log(string.Format("rot_offset: {0}, {1}, {2}, {3}", cdata.rotation_offset.x, cdata.rotation_offset.y, cdata.rotation_offset.z, cdata.rotation_offset.w));
            Debug.Log(string.Format("Detect pos: {0}, {1}, {2}", detection.transform.position.x, detection.transform.position.y, detection.transform.position.z));
            // Debug.Log(string.Format("Detect rot: {0}, {1}, {2}, {3}", detection.transform.rotation.x, detection.transform.rotation.y, detection.transform.rotation.z, detection.transform.rotation.w));

            var _pos = detection.transform.position;
            _pos.z *= cdata.z_scale;
            model.transform.localPosition = (cdata.direction_offset * _pos) + cdata.position_offset;


            // float angle = 0.0f;
            // Vector3 axis = Vector3.zero;
            // detection.transform.rotation.ToAngleAxis(out angle, out axis);
            // angle *= rotation_scale;
            // model.transform.localRotation = Quaternion.AngleAxis(angle, axis) * cdata.direction_offset;
            model.transform.localRotation = cdata.direction_offset * detection.transform.rotation * cdata.rotation_offset;
        }
        else
        {
            model.SetActive(false);
        }
    }

    void OnApplicationQuit()
    {
        SaveCalibData(cdata,"CameraCalib.json");
    }

    public static CalibData LoadCalibData(string filePath)
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            return JsonUtility.FromJson<CalibData>(jsonData);
        }
        return new CalibData(); 
    }

    public static void SaveCalibData(CalibData data, string filePath)
    {
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, jsonData);
    }
}
