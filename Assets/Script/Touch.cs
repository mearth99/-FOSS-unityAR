using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class Touch : MonoBehaviour
{
    private GameObject temp;
    private ARRaycastManager raycastmanage;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

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
            //터치가 시작된 경우 
            if(raycastmanage.Raycast(touch.position,hits,TrackableType.PlaneWithinPolygon))
            {
                //폴라곤이 생성된 ( AR이 감지한 곳만 터치한 경우)
                Instantiate(temp,hits[0].pose.position,hits[0].pose.rotation);
            }
        }

    }
}
