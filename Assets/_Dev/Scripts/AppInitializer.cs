using System.Collections;
using System.Collections.Generic;
using _Dev.Scripts;
using UnityEngine;

public class AppInitializer : MonoBehaviour
{
    private LearnObjectManager lm;
    public GameObject go;

    public GameObject lm_pos;
    public GameObject go_pos;
    
    
    // Start is called before the first frame update
    void Start()
    {

        lm = new LearnObjectManager(); 
        new LearnObjectInitializer(lm).InitializeDefaultLearnObjects();
        Instantiate(lm.GetLearnObjectGroups(1)[0][0].Asset,lm_pos.transform.position,Quaternion.identity);
        Instantiate(go,go_pos.transform.position,Quaternion.identity); 

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
