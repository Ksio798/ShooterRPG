using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class QuestController : MonoBehaviour
{
    public List<BaseQuest> Quests = new List<BaseQuest>();
    public OneQuestPanel QuPanelPrefab;
    public Transform QuestListPanel;
    public TextMeshProUGUI DesText;
    public GameObject QuestPanel;
    public Button StartButton;
    public GameObject UiPanel;
    public TextMeshProUGUI UIDesText;
    public TextMeshProUGUI UINameText;
    public InventoryController PlayerInv;
    public GameObject EndQuestText;

    private void Start()
    {
       BaseQuest[] q = FindObjectsOfType<BaseQuest>();
        for (int i = 0; i < q.Length; i++)
        {
            q[i].OnStart += AddQuest;
            q[i].OnStop += OnStopQuest;
            q[i].OnEnd += OnEndQuest;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && StaticVariables.InMeny)
        {
            QuestPanel.SetActive(!QuestPanel.activeSelf);
            Cursor.visible = !Cursor.visible;
          
            AddAllOnPanel();
            
        }
    }
    public void AddQuest(BaseQuest quest)
    {
        UiPanel.SetActive(true);
        UINameText.text = quest.QuestName;
        UIDesText.text = quest.QuestDescription;
        StartCoroutine(WaitToNewQuest(UiPanel));
        Quests.Add(quest);
    }
    void OnStopQuest(BaseQuest quest)
    {

    }
    void OnEndQuest(BaseQuest quest)
    {
        UiPanel.SetActive(false);
        PlayerInv.AddItems(quest.Prizes);
        Quests.Remove(quest);
        EndQuestText.SetActive(true);
        StartCoroutine(WaitToNewQuest(EndQuestText));

    }
    public void AddAllOnPanel()
    {
        DeliteAllPanels();
        for (int i = 0; i < Quests.Count; i++)
        {
        OneQuestPanel gm = Instantiate(QuPanelPrefab);
            gm.GetComponentInChildren<TextMeshProUGUI>().text = Quests[i].QuestName;
            gm.transform.SetParent(QuestListPanel);
            gm.textQuest = Quests[i].QuestDescription;
            gm.text = DesText;
            gm.index = i;
            gm.OnClick += StartQuest;
        }

    }
    void DeliteAllPanels()
    {
        for (int i = 0; i < QuestListPanel.childCount; i++)
        {
            Destroy(QuestListPanel.GetChild(i).gameObject);
        }
        StartButton.gameObject.SetActive(false);
        DesText.text = null;
    }
    void StartQuest(int index)
    {
        // StartButton.onClick.AddListener(() => { Quests[index].StartQuest(); });
        StartButton.gameObject.SetActive(true);
        StartButton.onClick.AddListener(() => { OnStart(index); });
    }
    void OnStart(int index)
    {
        UiPanel.SetActive(true);
        UINameText.text = Quests[index].QuestName;
        UIDesText.text = Quests[index].QuestDescription;
        QuestPanel.SetActive(false);
        Cursor.visible = false;
    }
    IEnumerator WaitToNewQuest(GameObject gm)
    {
        yield return new WaitForSeconds(2);
        gm.SetActive(false);
    }


}
