using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Quests.QuestBlocks
{
    public class QuestBlockCondition: QuestBlock
    {
        protected System.Func<bool> condition;
        public override bool TryPerfom()
        {
            if (condition == null)
            {
                return false;
            }
            else
            {
            return condition.Invoke();
            }

        }




    }







}
