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
        m_Camera = Camera.main;//���� ī�޶� ��������
        m_Construction = false;

        HideUi();//��ư�� ������ ������ Ui ������
    }

    private void Update()
    {
        if (Input.GetKeyUp(m_BuildButton)) //��ư B�� ������ ��,
        {
            m_Construction = false;//���� �ʱ�ȭ

            if (m_Object != null)//���� �Ǽ����̿��� ��� �Ǽ��Ϸ��� ������Ʈ �ı�
            {
                Destroy(m_Object);
            }

            m_Object = null;
            ToggleUi();//UI �¿���
        }

        if (m_Construction)//�Ǽ� ����� ���
        {
            m_rotate += Input.mouseScrollDelta.y * 10.0f;

            if (m_Object == null)
            {
                m_Object = Instantiate(m_BuildingInfo.m_Prefab);//���๰ ����

            }
            else
            {
                m_Object.transform.Rotate(0, m_rotate, 0);//�� ���� �������� ȸ��
                m_rotate = 0.0f;
                m_HorizontalRay = m_Camera.ScreenPointToRay(Input.mousePosition);//ī�޶� ���� ������ ���� ���� ����
                m_VerticalRay = new Ray(m_HorizontalRay.origin + m_HorizontalRay.direction * 5, new Vector3(0, -1, 0));//���� ���� �� �κп��� ���� ���� ���� 

                RaycastHit hit;

                if (Physics.Raycast(m_VerticalRay, out hit, 10.0f))//���̿� �浹�� ������Ʈ�� ����
                {
                    if (hit.transform.tag == "Structure" || hit.transform.tag == "Terrain")//�����̳� �ǹ��� ���
                    {
                        m_Object.transform.position = m_VerticalRay.origin + new Vector3(0, -hit.distance, 0);//�浹�� ��ġ�� ������Ʈ �̵�
                    }
                }
            }

            if (Input.GetMouseButtonDown(0))//Ŭ�� ��ư�� ������ ���
            {
                //�ش� ��ġ�� ���๰�� ������Ŵ
                m_Object.transform.tag = "Structure";//������ �±׷� ����
                m_Construction = false;//�Ǽ���� ����
                m_Object = null;
            }


        }
    }

    public void ToggleUi()//Ui ���¸� ��ȯ�ϴ� �Լ�
    {
        if (!m_Toggle)
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
