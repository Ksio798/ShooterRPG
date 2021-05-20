using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore;

namespace BBUnity.Conditions
{
    [Condition("Perception/IsTargetActiveAndClose")]
    [Help("Checks whether a target is close depending on a given distance")]
    public class IsTargetActiveAndClose : GOCondition
    {
        ///<value>Input Target Parameter to to check the distance.</value>
        [InParam("target")]
        [Help("Target to check the distance")]
        public GameObject target;

        ///<value>Input maximun distance Parameter to consider that the target is close.</value>
        [InParam("closeDistance")]
        [Help("The maximun distance to consider that the target is close")]
        public float closeDistance;

        /// <summary>
        /// Checks whether a target is close depending on a given distance,
        /// calculates the magnitude between the gameobject and the target and then compares with the given distance.
        /// </summary>
        /// <returns>True if the magnitude between the gameobject and de target is lower that the given distance.</returns>
        public override bool Check()
        {
            
            return target.activeSelf &&(gameObject.transform.position - target.transform.position).sqrMagnitude < closeDistance * closeDistance;
        }
    }
}