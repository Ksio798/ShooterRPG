using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Quests;
using Assets.Scripts.Quests.QuestBlocks;
public class FirstQuest : BaseQuest
{
    public ChestInventory chestInventory;
    public GameObject questInitiator;
    protected override void Start()
    {
        base.Start();

        QuestBlockEvent questBlockEventStarted = new QuestBlockEvent();
        questBlockEventStarted.SubscribeToEvent(questInitiator.GetComponent<IQuestInitiator>());
        QuestBlockEvent questBlockEventAddItem = new QuestBlockEvent();
        questBlockEventStarted.Next = questBlockEventAddItem;
        questBlockEventAddItem.SubscribeToEvent(chestInventory.GetComponent<IQuestInitiator>());

        questBlocks.Add(questBlockEventStarted);
        questBlocks.Add(questBlockEventAddItem);

        questBlockEventStarted.Activate();

       // chestInventory.OnAddItem += EndQuest;
    }
    //protected override void OnEnded()
    //{
    //    base.OnEnded();
    //    chestInventory.OnAddItem -= EndQuest;
    //}
}



// блоки с условием, блоки с действием,, блок с ожиданием события
