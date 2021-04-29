using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Quests
{

    [System.Serializable]
   public class QuestBlock
    {

      
        //public event System.Action OnComplited;
        public QuestBlock Next;

        public virtual void Activate()
        { 
        
        
        }
        public virtual bool TryPerfom()
        {
            return false;
        }


    }
}  

       

       

       
