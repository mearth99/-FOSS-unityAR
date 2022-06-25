using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class Touch_Move_object : MonoBehaviour
{
    private GameObject temp;
    private ARRaycastManager raycastmanage;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private bool checktouch = false;
    private GameObject touchobject;
    [SerializeField] private Camera arcamera;

    // Start is called before the first frame update
    void Start()
    {
        //ARRaycastManager 연결하기
        raycastmanage = GetComponent<ARRaycastManager>();
        // 이제 temp는 3D 구가 됩니다.
        temp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        // 구의 크기를 설정합니다.
        temp.transform.localScale = Vector3.one * 0.1f;
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
