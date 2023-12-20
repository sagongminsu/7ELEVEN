using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class BuildingManager : MonoBehaviour
{
    static public BuildingManager instance;

    public KeyCode m_BuildButton;

    public GameObject m_NameText;
    public GameObject m_DescText;
    public Image m_BuildingImage;

    private StructureData m_BuildingInfo;
    private Camera m_Camera;
    private Ray m_HorizontalRay;
    private Ray m_VerticalRay;
    private GameObject m_Object;
    private bool m_Construction;
    private bool m_Toggle = false;
    private float m_rotate = 0.0f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        m_Camera = Camera.main;//메인 카메라 가져오기
        m_Construction = false;

        HideUi();//버튼을 누르기 전까진 Ui 가리기
    }

    private void Update()
    {
        if (Input.GetKeyUp(m_BuildButton)) //버튼 B를 눌렀을 때,
        {
            m_Construction = false;//변수 초기화

            if (m_Object != null)//아직 건설중이였을 경우 건설하려는 오브젝트 파괴
            {
                Destroy(m_Object);
            }

            m_Object = null;
            ToggleUi();//UI 온오프
        }

        if (m_Construction)//건설 모드일 경우
        {
            m_rotate += Input.mouseScrollDelta.y * 10.0f;

            if (m_Object == null)
            {
                m_Object = Instantiate(m_BuildingInfo.m_Prefab);//건축물 생성

            }
            else
            {
                m_Object.transform.Rotate(0, m_rotate, 0);//휠 돌린 방향으로 회전
                m_rotate = 0.0f;
                m_HorizontalRay = m_Camera.ScreenPointToRay(Input.mousePosition);//카메라 기준 앞으로 수평 레이 생성
                m_VerticalRay = new Ray(m_HorizontalRay.origin + m_HorizontalRay.direction * 5, new Vector3(0, -1, 0));//수평 레이 끝 부분에서 수직 레이 생성 

                RaycastHit hit;

                if (Physics.Raycast(m_VerticalRay, out hit, 10.0f))//레이와 충돌한 오브젝트에 접근
                {
                    if (hit.transform.tag == "Structure" || hit.transform.tag == "Terrain")//지형이나 건물일 경우
                    {
                        m_Object.transform.position = m_VerticalRay.origin + new Vector3(0, -hit.distance, 0);//충돌한 위치에 오브젝트 이동
                    }
                }
            }

            if (Input.GetMouseButtonDown(0))//클릭 버튼을 눌렀을 경우
            {
                //해당 위치에 건축물을 고정시킴
                m_Object.transform.tag = "Structure";//구조물 태그로 변경
                m_Construction = false;//건설모드 해제
                m_Object = null;
            }


        }
    }

    public void ToggleUi()//Ui 상태를 전환하는 함수
    {
        if (!m_Toggle)
        {
            Cursor.lockState = CursorLockMode.None;//커서 고정 해제
            PlayerController.instance.canLook = false;
            ShowUi();

            m_Toggle = !m_Toggle;//토글 반대로 전환
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;//커서 고정
            PlayerController.instance.canLook = true;
            HideUi();

            m_Toggle = !m_Toggle;//토글 반대로 전환
        }
    }

    public void SelectBuilding(ref StructureData info)//스크롤 뷰에서 건물 버튼을 누르면 호출
    {
        this.m_BuildingInfo = info;//건물 정보를 가져온다.

        UpdateDesc();//이름과 설명, 아이콘을 가져온 건물 정보로 업데이트
    }

    public void ConstructionMode()
    {
        m_Construction = true;//건설 모드를 키고 UI 숨기기
        ToggleUi();
    }

    private void UpdateDesc()//가져온 건물 정보에 맞춰서 예시 이미지와 이름, 설명을 출력
    {
        m_NameText.GetComponent<TextMeshProUGUI>().text = m_BuildingInfo.m_StructureName;
        m_DescText.GetComponent<TextMeshProUGUI>().text = m_BuildingInfo.m_Desc;
        m_BuildingImage.sprite = m_BuildingInfo.m_Icon;

    }

    private void ShowUi()//UI창 보이기
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    private void HideUi()//UI창 숨기기
    {
        gameObject.transform.localScale = Vector3.zero;
    }
}
