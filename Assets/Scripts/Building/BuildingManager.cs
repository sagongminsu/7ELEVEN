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
        m_Camera = Camera.main;//���� ī�޶� ��������
        m_Construction = false;
        HideUi();//��ư�� ������ ������ Ui ������
    }

    private void Update()
    {
        if (Input.GetKeyUp(m_BuildButton)) 
        {
            Cursor.lockState = CursorLockMode.None;
            ShowUi();
            PlayerController.instance.canLook = false;
        }



        if (m_Construction)//�Ǽ� ����� ���
        {
            if (m_Object == null)
            {
                m_Object = Instantiate(m_BuildingInfo.m_Prefab);//���๰ ����
            }
            else
            {
                m_Ray = m_Camera.ScreenPointToRay(Input.mousePosition);//ī�޶� �������� ���� ����
                m_Object.transform.position = m_Ray.origin + m_Ray.direction * 5;//ī�޶� �������� ���� �Ÿ� 10 ������ ������Ʈ ��ġ ���� 
            }

            if (Input.GetMouseButtonUp(0))
            {
                //�ش� ��ġ�� ���๰�� ������Ű�� ������ ����

                m_Construction = false;
                m_Object = null;
            }
        }
    }

    public void ShowUi()//UIâ ���̱�
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    public void HideUi()//UIâ �����
    {
        gameObject.transform.localScale = Vector3.zero;
    }

    public void SelectBuilding(ref BuildingInfo info)//��ũ�� �信�� �ǹ� ��ư�� ������ ȣ��
    {
        this.m_BuildingInfo = info;//�ǹ� ������ �����´�.

        UpdateDesc();//�̸��� ����, �������� ������ �ǹ� ������ ������Ʈ
    }

    public void ConstructionMode()
    {
        m_Construction = true;//�����带 Ű�� UI �����
        HideUi();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UpdateDesc()//������ �ǹ� ������ ���缭 ���� �̹����� �̸�, ������ ���
    {
        m_NameText.GetComponent<TextMeshProUGUI>().text = m_BuildingInfo.m_BuildingName;
        m_DescText.GetComponent<TextMeshProUGUI>().text = m_BuildingInfo.m_Desc;
        m_BuildingImage.sprite = m_BuildingInfo.m_Icon;

    }

}
