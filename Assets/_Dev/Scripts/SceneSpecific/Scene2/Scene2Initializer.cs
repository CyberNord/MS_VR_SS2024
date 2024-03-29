using _Dev.Scripts.db;
using _Dev.Scripts.ObjectBehaviour;
using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Scene2Initializer : MonoBehaviour
{
    private LearnObjectManager _lm;

    public GameObject[] loPositions;

    public GameObject canvasPrefab;

    void Start()
    {
        _lm = new LearnObjectManager();
        new LearnObjectInitializer(_lm).InitializeDefaultLearnObjects();


        // Instantiate from "Resource" folder "GetAllLearnObjects" method 
        List<LearnObject> allLearnObjects = _lm.GetAllLearnObjects();
        Debug.Log($"LearnObject count: {allLearnObjects.Count}");
        foreach (var lo in allLearnObjects)
        {
            Debug.Log(lo.ToString() + ", Asset: " + (lo.Asset != null ? lo.Asset.name : "NULL"));
        }

        string[] objectDescriptions = { "Cube", "Sphere", "Capsule", "Cube", "Sphere", "Capsule", "Cube", "Sphere", "Capsule", "Cube" };
        float[] rotationAngles = { 270f, 270f, 270f, 0f, 0f, 0f, 0f, 90f, 90f, 90f };

        for (int i = 0; i < objectDescriptions.Length; i++)
        {
            LearnObject learnObject = allLearnObjects.Find(lo => string.Equals(lo.DescEnglish, objectDescriptions[i], StringComparison.OrdinalIgnoreCase));
            if (learnObject != null && learnObject.Asset != null)
            {
                InstantiateObjectWithCanvas(learnObject, loPositions[i], rotationAngles[i]);
            }
        }
    }

    void InstantiateObjectWithCanvas(LearnObject lo, GameObject lmPosition, float rotationAngle)
    {
        GameObject obj = Instantiate(lo.Asset, lmPosition.transform.position, Quaternion.identity);
        ConvertMaterialToTransparent(obj);

        obj.AddComponent<DestroyObject>();
        //AddComponentsToLearnObject(obj);

        // Adjust position and instantiate the canvas
        Vector3 canvasPosition = lmPosition.transform.position + new Vector3(0, 0.5f, 0.5f);
        GameObject canvas = Instantiate(canvasPrefab, canvasPosition, Quaternion.identity);

        obj.transform.Rotate(Vector3.up, rotationAngle);
        canvas.transform.Rotate(Vector3.up, rotationAngle);

        if (rotationAngle == 270)
            canvas.transform.position = lmPosition.transform.position + new Vector3(-0.4f, 0.5f, 0);
        else if (rotationAngle == 90)
            canvas.transform.position = lmPosition.transform.position + new Vector3(0.4f, 0.5f, 0);


        TextMeshProUGUI englishTextComponent = canvas.transform.Find("English_Text").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI germanTextComponent = canvas.transform.Find("German_Text").GetComponent<TextMeshProUGUI>();

        if (englishTextComponent != null)
        {
            englishTextComponent.text = lo.DescEnglish;
        }

        if (germanTextComponent != null)
        {
            germanTextComponent.text = lo.DescGerman;
        }

        EventTrigger trigger = canvas.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = canvas.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => { PlayAudio(lo.AudioClipEnglish); });

        trigger.triggers.Add(entry);
    }

    private void ConvertMaterialToTransparent(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();

        // Change material rendering mode to Transparent
        if (renderer != null && renderer.material != null)
        {
            renderer.material.SetFloat("_Surface", 1); // 1 is for Transparent mode
        }
    }

    //private void AddComponentsToLearnObject(GameObject obj)
    //{
    //    obj.AddComponent<DestroyObject>();

    //    Rigidbody rb = obj.GetComponent<Rigidbody>();
    //    if (rb == null)
    //    {
    //        rb = obj.AddComponent<Rigidbody>();
    //    }

    //    Grabbable grabbable = obj.GetComponent<Grabbable>();
    //    if (grabbable == null)
    //    {
    //        obj.AddComponent<Grabbable>().enabled = true;
    //    }
    //    else if (!grabbable.enabled)
    //    {
    //        grabbable.enabled = true;
    //    }

    //    GrabInteractable grabInteractable = obj.GetComponent<GrabInteractable>();
    //    if (grabInteractable == null)
    //    {
    //        obj.AddComponent<GrabInteractable>().enabled = true;
    //    }
    //    else if (!grabInteractable.enabled)
    //    {
    //        grabInteractable.enabled = true;
    //    }

    //    PhysicsGrabbable physicsGrabbable = obj.GetComponent<PhysicsGrabbable>();
    //    if (physicsGrabbable == null)
    //    {
    //        obj.AddComponent<PhysicsGrabbable>().enabled = true;
    //    }
    //    else if (!physicsGrabbable.enabled)
    //    {
    //        physicsGrabbable.enabled = true;
    //    }
    //}

    void PlayAudio(AudioClip clip)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        }
    }
}