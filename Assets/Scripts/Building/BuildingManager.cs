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
    private Ray m_Ray;

    private GameObject m_Object;
    private bool m_Construction;
    private bool m_Toggle = false;

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
        if (Input.GetKeyUp(m_BuildButton)) //��ư B�� ������ ��,
        {
            m_Construction = false;//���� �ʱ�ȭ
            m_Object = null;
            ToggleUi();//UI �¿���
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
                m_Object.transform.position = m_Ray.origin + m_Ray.direction * 5;//ī�޶� �������� ���� �Ÿ� 10 ������ ������Ʈ ��ġ �̵�
            }

            if (Input.GetMouseButtonDown(0))
            {
                //�ش� ��ġ�� ���๰�� ������Ŵ
                m_Construction = false;
            }
        }
    }

    public void ToggleUi()//Ui ���¸� ��ȯ�ϴ� �Լ�
    {
        if(!m_Toggle)
        {
            Cursor.lockState = CursorLockMode.None;//Ŀ�� ���� ����
            PlayerController.instance.canLook = false;
            ShowUi();

            m_Toggle = !m_Toggle;//��� �ݴ�� ��ȯ
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;//Ŀ�� ����
            PlayerController.instance.canLook = true;
            HideUi();

            m_Toggle = !m_Toggle;//��� �ݴ�� ��ȯ
        }
    }

    public void SelectBuilding(ref StructureData info)//��ũ�� �信�� �ǹ� ��ư�� ������ ȣ��
    {
        this.m_BuildingInfo = info;//�ǹ� ������ �����´�.

        UpdateDesc();//�̸��� ����, �������� ������ �ǹ� ������ ������Ʈ
    }

    public void ConstructionMode()
    {
        m_Construction = true;//�Ǽ� ��带 Ű�� UI �����
        ToggleUi();
    }

    private void UpdateDesc()//������ �ǹ� ������ ���缭 ���� �̹����� �̸�, ������ ���
    {
        m_NameText.GetComponent<TextMeshProUGUI>().text = m_BuildingInfo.m_StructureName;
        m_DescText.GetComponent<TextMeshProUGUI>().text = m_BuildingInfo.m_Desc;
        m_BuildingImage.sprite = m_BuildingInfo.m_Icon;

    }

    private void ShowUi()//UIâ ���̱�
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    private void HideUi()//UIâ �����
    {
        gameObject.transform.localScale = Vector3.zero;
    }
}
