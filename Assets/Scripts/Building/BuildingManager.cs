using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class BuildingManager : MonoBehaviour
{
    static public BuildingManager instance;

    public KeyCode m_BuildButton;//건물 On, Off 버튼

    public GameObject m_NameText;//건물 이름 텍스트
    public GameObject m_DescText;//설명 텍스트
    public Image m_BuildingImage;//선택된 건물 아이콘을 출력할 이미지 오브젝트

    private StructureData m_BuildingInfo;//현재 선택한 건물 정보
    private Camera m_Camera;//메인 카메라
    private Ray m_HorizontalRay;//수평 레이
    private Ray m_VerticalRay;//수직 레이
    private GameObject m_BluePrint;//청사진 프리펩
    private bool m_Construction;//건설 모드 bool
    private bool m_Toggle = false;//Ui On, off 체크
    private float m_rotate = 0.0f;

    private void Awake()
    {
        instance = this;//싱글톤
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

            if (m_BluePrint != null)//아직 건설중이였을 경우 건설하려는 오브젝트 파괴
            {
                Destroy(m_BluePrint);
            }

            m_BluePrint = null;
            ToggleUi();//UI 온오프
        }

        if (m_Construction)//건설 모드일 경우
        {
            m_rotate += Input.mouseScrollDelta.y * 10.0f;

            if (m_BluePrint == null)
            {
                m_BluePrint = Instantiate(m_BuildingInfo.m_Prefab_B);//건축물 청사진 생성
            }
            else
            {
                m_BluePrint.transform.Rotate(0, m_rotate, 0);//휠 돌린 방향으로 회전
                m_rotate = 0.0f;
                m_HorizontalRay = m_Camera.ScreenPointToRay(Input.mousePosition);//카메라 기준 정면으로 레이 생성
                m_VerticalRay = new Ray(m_HorizontalRay.origin + m_HorizontalRay.direction * 5, new Vector3(0, -1, 0));//정면 레이 끝 부분에서 수직 레이 생성 

                RaycastHit hit;

                if(Physics.Raycast(m_HorizontalRay, out hit, 3.0f))
                {
                    if(hit.transform.gameObject.name == "Wall")
                    {
                        Debug.Log("This is Wall");
                    }
                }

                if (Physics.Raycast(m_VerticalRay, out hit, 10.0f))//레이와 충돌한 오브젝트에 접근
                {
                    if (hit.transform.tag == "Structure" || hit.transform.tag == "Terrain")//지형이나 건물일 경우
                    {
                        m_BluePrint.transform.position = m_VerticalRay.origin + new Vector3(0, -hit.distance, 0);//충돌한 위치에 오브젝트 이동
                    }
                }
            }

            if (Input.GetMouseButtonDown(0))//클릭 버튼을 눌렀을 경우
            {
                //해당 위치에 건축물을 고정시킴
                GameObject temp = Instantiate(m_BuildingInfo.m_Prefab);//실제 건축물 프리펩 생성
                temp.transform.position = m_BluePrint.transform.position;//청사진 위치와 회전값으로 덮어쓰기
                temp.transform.rotation = m_BluePrint.transform.rotation;
                temp.transform.tag = "Structure";//구조물 태그로 변경
                m_Construction = false;//건설모드 해제
                Destroy(m_BluePrint);//청사진 오브젝트 삭제
                m_BluePrint = null;
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
        if(m_BuildingInfo != null)//선택한 건축물이 없을 때는 비활성화
        {
            m_Construction = true;//건설 모드를 키고 UI 숨기기
            ToggleUi();
        }
    }

    public bool RecipeCheck()
    {
        foreach(StructureRecipe item in m_BuildingInfo.items)
        {
            bool result;
            result = Inventory.instance.HasItems(item.index, item.count);//충분한 아이템이 있을 경우, 참

            if(!result)
            {
                return false;
            }
        }

        return true;
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
