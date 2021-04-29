using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Quests.QuestBlocks
{
    [System.Serializable]
    public class QuestBlockEvent: QuestBlock
    {

      
        bool IsCompl = false;
        IQuestInitiator worldEvent;
        public void SubscribeToEvent(IQuestInitiator worldEvent)
        {
            this.worldEvent = worldEvent;
        
        }


        public override void Activate()
        {
            worldEvent.OnQuestStart += OnStart;
        }
        // public void Sub
        public override bool TryPerfom()
        {
            return IsCompl;
        }
        void OnStart()
        {
            IsCompl = true;
        }
    }
}
