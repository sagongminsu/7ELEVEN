using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    static public BuildingManager instance;
    
    public GameObject m_NameText;
    public GameObject m_DescText;
    public Image m_BuildingImage;

    private BuildingInfo m_BuildingInfo;
    private Camera m_Camera;
    private Ray m_Ray;

    private GameObject m_Object;
    private bool m_Construction;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        m_Camera = Camera.main;
        m_Construction = false;
        //HideUi();//버튼을 누르기 전까진 Ui 가리기
    }

    private void Update()
    {
        if(m_Construction)//건설 모드일 경우
        {
            if(m_Object == null)
            {
                m_Object = Instantiate(m_BuildingInfo.m_Prefab);
            }else
            {
                //m_Object.transform.position = 
            }

            if (Input.GetMouseButtonUp(0))
            {

                //해당 위치에 건축물을 고정시키고 반투명 해제

                m_Construction = false;
                m_Object = null;
            }
        }
    }

    public void ShowUi()//UI창 보이기
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    public void HideUi()//UI창 숨기기
    {
        gameObject.transform.localScale = Vector3.zero;
    }

    public void SelectBuilding(ref BuildingInfo info)//스크롤 뷰에서 건물 버튼을 누르면 호출
    {
        this.m_BuildingInfo = info;//건물 정보를 가져온다.

        UpdateDesc();
    }

    public void ConstructionMode()
    {
        

        m_Construction = true;
        //
    }

    private void UpdateDesc()//가져온 건물 정보에 맞춰서 예시 이미지와 이름, 설명을 출력
    {
        m_NameText.GetComponent<TextMeshProUGUI>().text = m_BuildingInfo.m_BuildingName;
        m_DescText.GetComponent<TextMeshProUGUI>().text = m_BuildingInfo.m_Desc;
        m_BuildingImage.sprite = m_BuildingInfo.m_Icon;

    }


  

}
