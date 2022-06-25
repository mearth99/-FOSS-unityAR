using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
public class Touch_distance_Object : MonoBehaviour
{
    private string text;
    private float distance;
    private GameObject temp;
    private ARRaycastManager raycastmanage;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private bool checktouch = false;
    private GameObject touchobject;
    private GameObject deleteobject = null;
    [SerializeField] private Camera arcamera;

    public LineRenderer lr;
    private float width;
    private float height;

    private void Awake() 
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;
    }
    void Start()
    {
        raycastmanage = GetComponent<ARRaycastManager>();
        // 이제 temp는 3D 구가 됩니다.
        temp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        // 구의 크기를 설정합니다.
        temp.transform.localScale = Vector3.one * 0.05f;
        temp.GetComponent<Renderer>().material.color = Color.white;
        text = "준비중";

        lr = GetComponent<LineRenderer>();
        lr.startWidth=0.01f;
        lr.endWidth=0.01f;
        lr.material.color = Color.black;
    }

    private void OnGUI() 
    {
        GUI.skin.label.fontSize = (int)(Screen.width / 20.0f);
        GUI.Label(new Rect(20,20, width, height * 0.25f), text);
    }
    // Update is called once per frame
    void Update()
    {
        UnityEngine.Touch touch = Input.GetTouch(0);
        if(Input.touchCount == 0)
            return;
        if(touch.phase == TouchPhase.Began)
        {
            Ray ray;
            RaycastHit hitobject;

            ray = arcamera.ScreenPointToRay(touch.position);
            if(Physics.Raycast(ray,out hitobject))
            {
                if(hitobject.collider.name.Contains("Sphere"))
                {
                    touchobject = hitobject.collider.gameObject;
                    checktouch = true;
                    if(touchobject == deleteobject)
                    {
                        Destroy(deleteobject);
                        checktouch = false;
                    }
                    else
                    {
                        touchobject.GetComponent<Renderer>().material.color = Color.red;
                        if(deleteobject != null)
                        {
                            deleteobject.GetComponent<Renderer>().material.color = Color.white;
                            distance = Vector3.Distance(touchobject.transform.position,deleteobject.transform.position)*100;
                            lr.SetPosition(0, deleteobject.transform.position);
                            lr.SetPosition(1, touchobject.transform.position);
                            text = "길이: " + ((int)distance).ToString() + " cm";
                        }
                        deleteobject = touchobject;
                    }
                }
                else
                {
                    //이미 있는 Object를 선택하지 않는 경우
                    if(raycastmanage.Raycast(touch.position,hits,TrackableType.PlaneWithinPolygon))
                    {
                        //폴라곤이 생성된 ( AR이 감지한 곳만 터치한 경우)
                        Instantiate(temp,hits[0].pose.position,hits[0].pose.rotation);
                    }
                }
            }
        }
        if(touch.phase == TouchPhase.Ended)
        {
            checktouch = false;
        }
        if(raycastmanage.Raycast(touch.position,hits,TrackableType.PlaneWithinPolygon))
        {
            if(checktouch)
                touchobject.transform.position = hits[0].pose.position;
        }
    }
}

