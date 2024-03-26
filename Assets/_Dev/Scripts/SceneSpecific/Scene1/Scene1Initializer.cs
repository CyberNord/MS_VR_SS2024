using _Dev.Scripts.db;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Scene1Initializer : MonoBehaviour
{
    private LearnObjectManager _lm;

    public GameObject lmPos;
    public GameObject lmPos2;
    public GameObject lmPos3;

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

        //Objects are placed by their english name
        LearnObject cubeLearnObject = allLearnObjects.Find(lo => string.Equals(lo.DescEnglish, "Cube", StringComparison.OrdinalIgnoreCase));
        if (cubeLearnObject != null && cubeLearnObject.Asset != null)
        {
            InstantiateObjectWithCanvas(cubeLearnObject, lmPos, false, 0); //front side of the table
        }

        LearnObject sphereLearnObject = allLearnObjects.Find(lo => string.Equals(lo.DescEnglish, "Sphere", StringComparison.OrdinalIgnoreCase));
        if (sphereLearnObject != null && sphereLearnObject.Asset != null)
        {
            InstantiateObjectWithCanvas(sphereLearnObject, lmPos2, true, 90); //right side of the table
        }

        LearnObject capsuleLearnObject = allLearnObjects.Find(lo => string.Equals(lo.DescEnglish, "Capsule", StringComparison.OrdinalIgnoreCase));
        if (capsuleLearnObject != null && capsuleLearnObject.Asset != null)
        {
            InstantiateObjectWithCanvas(capsuleLearnObject, lmPos3, true, 270); //left side of the table
        }
    }

    void InstantiateObjectWithCanvas(LearnObject lo, GameObject lmPosition, bool rotateObject, float rotationAngle)
    {
        GameObject obj = Instantiate(lo.Asset, lmPosition.transform.position, Quaternion.identity);

        // Adjust position and instantiate the canvas
        Vector3 canvasPosition = lmPosition.transform.position + new Vector3(0, 0.5f, 0.5f);
        GameObject canvas = Instantiate(canvasPrefab, canvasPosition, Quaternion.identity);
        canvas.transform.SetParent(obj.transform);

        //Rotate the object
        if (rotateObject)
        {
            obj.transform.Rotate(Vector3.up, rotationAngle);
        }

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

        Button audioButton = canvas.GetComponentInChildren<Button>();
        if (audioButton != null)
        {
            audioButton.onClick.AddListener(() => PlayAudio(lo.AudioClipEnglish));
        }
    }

    void PlayAudio(AudioClip clip)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        }
    }
}