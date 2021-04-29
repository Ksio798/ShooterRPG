using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Quests.QuestBlocks
{
    public class QuestBlockAction : QuestBlock
    {
        public System.Action someAction;
        public override bool TryPerfom()
        {
            someAction?.Invoke();
            return true;
        }
    }
}
