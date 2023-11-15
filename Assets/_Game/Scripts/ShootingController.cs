using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Object = UnityEngine.Object;

public class ShootingController : MonoBehaviour
{
 
    public GameObject cursor; //drag n drop UI object in inspector
    public Canvas canvas; //drag n drop canvas in inspector
    float pixelX;
    float pixelY;
    float posX;
    float posY;
    public GameObject obj;
    public GameObject shotTracer;
    public Transform shootingPoint;
    public Transform rangeObject;
    public float rayRange=400;


    private Vector3 sphereCastMidpoint;

    void Start () 
    {
        pixelX = canvas.worldCamera.pixelWidth;
        pixelY = canvas.worldCamera.pixelHeight;
    }

    private float shootRate;
    void Update(){
        RayDetection();
    }
    void RayDetection()
    {
      
        posX = cursor.transform.localPosition.x + pixelX / 2f;
        posY = cursor.transform.localPosition.y + pixelY / 2f;
        Vector3 cursorPos = new Vector3(posX, posY);
        RectTransform objectA = cursor.GetComponent<RectTransform>();
        Vector3 pos = Camera.main.ViewportToWorldPoint(objectA.position);
        obj.transform.position = Camera.main.WorldToViewportPoint(pos);
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(cursorPos);
        Vector3 dir;
        Ray rayToCameraPos = new Ray(obj.transform.position, Camera.main.transform.position - obj.transform.position);
        dir = obj.gameObject.transform.position - Camera.main.transform.position;
        Debug.DrawRay(ray.origin, dir * 10000, Color.black);
        if (Physics.SphereCast(ray.origin, 0.1f, dir, out hit, rayRange))
        {
            
            Vector3 sphereCastMidpoint = ray.origin + (dir * hit.distance);
            Debug.DrawLine(ray.origin, sphereCastMidpoint, Color.yellow);
            if (hit.collider.CompareTag("enemy"))
            {
                cursor.GetComponent<Image>().color=Color.green;
                SpawnBullet(shootingPoint.position,hit.point,0.2f,hit.transform);
            }
            else
            {
                cursor.GetComponent<Image>().color=Color.red;
            }

            sphereCastMidpoint = hit.point;
            rangeObject.transform.position = sphereCastMidpoint;
        }
        else
        {
            cursor.GetComponent<Image>().color=Color.red;
            sphereCastMidpoint = ray.origin + (dir * rayRange);
            rangeObject.transform.position = sphereCastMidpoint;
           
        }
    }

    void SpawnBullet(Vector3 originPos,Vector3 destination,float rate,Transform effectedTransform)
    {
        shootRate += Time.deltaTime;
        if (shootRate >= rate)
        {
            shootRate = rate;
        }
        if (shootRate % rate==0)
        {
            shootRate = 0;
            GameObject instantShot = Instantiate(shotTracer);
            instantShot.SetActive(true);
            instantShot.transform.position = originPos;
            instantShot.transform.rotation = Quaternion.LookRotation(destination - originPos);
            
            effectedTransform.GetComponent<HealthScript>().GetHit();
            // instantShot.transform.parent = shot.transform.parent;
        }
    }

    private void OnDrawGizmos()
    {
        // Gizmos.DrawWireSphere(transform.position, 10);
        posX = cursor.transform.localPosition.x + pixelX / 2f;
        posY = cursor.transform.localPosition.y + pixelY / 2f;
        Vector3 cursorPos = new Vector3(posX, posY);
        RectTransform objectA = cursor.GetComponent<RectTransform>();
        Vector3 pos = Camera.main.ViewportToWorldPoint(objectA.position);
        obj.transform.position = Camera.main.WorldToViewportPoint(pos);
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(cursorPos);
        Vector3 dir;
        Ray rayToCameraPos = new Ray(obj.transform.position, Camera.main.transform.position - obj.transform.position);
        dir = obj.gameObject.transform.position - Camera.main.transform.position;
        Debug.DrawRay(ray.origin, dir * 10000, Color.black);
        
        if (Physics.SphereCast(ray.origin, 0.1f, dir, out hit, 10000))
        {
            // Gizmos.color = Color.green;
            Vector3 sphereCastMidpoint = ray.origin + (dir * hit.distance);
            // Gizmos.DrawWireSphere(sphereCastMidpoint, 0.05f);
            Gizmos.DrawSphere(hit.point, 0.1f);
            Debug.DrawLine(ray.origin, sphereCastMidpoint, Color.yellow);
            if (hit.collider.CompareTag("enemy"))
            {
                cursor.GetComponent<Image>().color=Color.green;
            }
            else
            {
                cursor.GetComponent<Image>().color=Color.red;
            }
        }
        else
        {
            cursor.GetComponent<Image>().color=Color.red;
            // Gizmos.color = Color.red;
            // Vector3 sphereCastMidpoint = transform.position + (raycastOffset)   + (transform.forward * (rayRange-1));
            // Gizmos.DrawWireSphere(sphereCastMidpoint, 0.2f);
            // Debug.DrawLine(transform.position + (raycastOffset) , sphereCastMidpoint, Color.red);
        }
    }

}