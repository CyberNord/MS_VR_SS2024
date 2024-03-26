using _Dev.Scripts.db;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scene1Test : MonoBehaviour
{
    private LearnObjectManager lm;

    public GameObject lm_pos;
    public GameObject lm_pos2;
    public GameObject lm_pos3;

    public GameObject canvasPrefab;

    void Start()
    {
        lm = new LearnObjectManager();
        new LearnObjectInitializer(lm).InitializeDefaultLearnObjects();

        InstantiateObjectWithCanvas(lm.GetLearnObjectGroups(1)[1][0], lm_pos, false, 0); //front of the table
        InstantiateObjectWithCanvas(lm.GetLearnObjectGroups(1)[0][0], lm_pos2, true, 90); //right side of the table
        InstantiateObjectWithCanvas(lm.GetLearnObjectGroups(1)[2][0], lm_pos3, true, 270); //left side of the table
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
            //canvas.transform.Rotate(Vector3.up, rotationAngle);
            //canvas.transform.position += new Vector3(0, 0, 0f);
        }

        TextMeshProUGUI textComponent = canvas.GetComponentInChildren<TextMeshProUGUI>();
        if (textComponent != null)
        {
            textComponent.text = lo.DescGerman;
        }
    }
}