using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CrossHairClamp : MonoBehaviour
{
    // Start is called before the first frame update
    public CanvasScaler canvasScaler;
    public RectTransform crossHair;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //  Debug.Log(Screen.height + "  " + Screen.width);
//        Debug.Log(canvasScaler.referenceResolution.x + "  " + canvasScaler.referenceResolution.y);
       // Debug.Log(crossHair.anchoredPosition);
       
    }

    void LateUpdate()
    {
        crossHair.anchoredPosition= new Vector2(Mathf.Clamp(   crossHair.anchoredPosition.x,-400,400),
            Mathf.Clamp(crossHair.anchoredPosition.y,-900,900));
    }
}