using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Quests;
using Assets.Scripts.Quests.QuestBlocks;
public class BaseQuest : MonoBehaviour
{

    public string QuestName;
    public string QuestDescription;
    public List<Item> Prizes = new List<Item>();
   // public GameObject questInitiator;
    public event System.Action<BaseQuest> OnStart;
    public event System.Action<BaseQuest> OnStop;
    public event System.Action<BaseQuest> OnEnd;
   // IQuestInitiator initiator;


   
    protected List<QuestBlock> questBlocks = new List<QuestBlock>();

    int currentQuestBlock = 0;
    protected bool added = true;
    protected virtual void Start()
    {
     //   initiator = questInitiator.GetComponent<IQuestInitiator>();
        //initiator.OnQuestStart += AddQuest;
    }
    private void Update()
    {
        if (currentQuestBlock != -1)
        {
            bool q = questBlocks[currentQuestBlock].TryPerfom();
            if (q)
            {
                if (currentQuestBlock == 0)
                {
                    AddQuest();
                }

                QuestBlock NewBlock = questBlocks[currentQuestBlock].Next;
                int index = questBlocks.IndexOf(NewBlock);
                if(index != -1)
                {
                    currentQuestBlock = index;
                    NewBlock.Activate();
                }
                else
                {
                   
                    EndQuest();
                }

            }



        }




    }
    public virtual void AddQuest()
    {
       // added = false;
        OnStart?.Invoke(this);

    }
    public virtual void EndQuest()
    {
       
            currentQuestBlock = -1;
            questBlocks.Clear();
            OnEnded();
        

    }
    protected virtual void OnEnded()
    {
        OnEnd?.Invoke(this);



        Debug.Log("QuestEnded");
    }
}
