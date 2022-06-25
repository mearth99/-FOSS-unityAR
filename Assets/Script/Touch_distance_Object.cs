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
        // 구의 색상을 설정합니다.
        temp.GetComponent<Renderer>().material.color = Color.white;
        // GUI를 이용하여 화면에 출력할 문구를 설정합니다.
        text = "준비중";

        //선을 그릴 준비를 합니다.
        lr = GetComponent<LineRenderer>();
        lr.startWidth=0.01f;
        lr.endWidth=0.01f;
        lr.material.color = Color.black;
    }

    private void OnGUI() 
    {
        // 이 함수를 이용하여 text 변수를 화면에 출력합니다.
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
            //ray를 터치한 위치로 날려서 충돌을 감지합니다.
            if(Physics.Raycast(ray,out hitobject))
            {   
                //구체를 터치했다면
                if(hitobject.collider.name.Contains("Sphere"))
                {
                    //터치한 구체를 변수로 관리합니다.
                    touchobject = hitobject.collider.gameObject;
                    //터치 중 신호입니다.
                    checktouch = true;
                    //같은 구체를 두번 터치하면 삭제합니다.
                    if(touchobject == deleteobject)
                    {
                        Destroy(deleteobject);
                        checktouch = false;
                    }
                    else
                    {
                        //새로 선택한 구체는 색이 빨간색으로 변합니다.
                        touchobject.GetComponent<Renderer>().material.color = Color.red;
                        if(deleteobject != null)
                        {
                            /*
                            이전에 선택한 구체는 색이 하얀색으로 돌아오며, 하얀 구체와 빨간 구체 사이의 거리를 계산하여 
                            m 단위를 구하고 *100으로 cm로 환산합니다.
                            lr의 위치를 지정하여 직선을 그리게 합니다.
                            text 문구를 업데이트하여 화면에 나타냅니다.
                            */
                            deleteobject.GetComponent<Renderer>().material.color = Color.white;
                            distance = Vector3.Distance(touchobject.transform.position,deleteobject.transform.position)*100;
                            lr.SetPosition(0, deleteobject.transform.position);
                            lr.SetPosition(1, touchobject.transform.position);
                            text = "길이: " + ((int)distance).ToString() + " cm";
                        }
                        //터치했던 구체를 추적하기 위해 delete를 업데이트합니다.
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
            // 폴라곤을 터치하고 있고, 터치 중이라면 타깃 Object의 위치를 변경한다.
            if(checktouch)
                touchobject.transform.position = hits[0].pose.position;
        }
    }
}

