using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Quests.QuestBlocks;
public class QuestBringBottle : BaseQuest, IQuestInitiator
{
    public event System.Action OnQuestStart;
    public GameObject questInitiator;
    public GameObject Obj;
    public Item item;
    protected override void Start()
    {
        base.Start();
        QuestBlockEvent Starter = new QuestBlockEvent();
        Starter.SubscribeToEvent(questInitiator.GetComponent<IQuestInitiator>());
        QuestBlockAction SpawnCheast = new QuestBlockAction();
        SpawnCheast.someAction += Spawn;
        QuestBlockCondition IsHave = new QuestBlockCondition();
        IsHave.condition += IsContein;
        QuestBlockAction StartNewQuest = new QuestBlockAction();
        StartNewQuest.someAction += StartsNQuest;
        Starter.Next = SpawnCheast;
        SpawnCheast.Next = IsHave;
        IsHave.Next = StartNewQuest;
        questBlocks.Add(Starter);
        questBlocks.Add(SpawnCheast);
        questBlocks.Add(IsHave);
        questBlocks.Add(StartNewQuest);



        Starter.Activate();
    }

    void Spawn()
    {
        Obj.SetActive(true);
        Obj.layer  = LayerMask.NameToLayer("LightingObj");
    }
    bool IsContein()
    {
        
        ChestInventory chestInventory = Obj.GetComponent<ChestInventory>();
        return chestInventory.items.Contains(item);
    }
    void StartsNQuest()
    {

        Obj.layer = LayerMask.NameToLayer("InteractionObj");
        OnQuestStart?.Invoke();
    } 


}
