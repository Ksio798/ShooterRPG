using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    [Action("Animation/SetTriggerParameter")]
    [Help("Plays an animation in the game object")]
    public class SetAnimationParameter : GOAction
    {
        [InParam("animator")]
        [Help("The clip that must be played")]
        public Animator animator;

        [InParam("TriggerParameterName")]
        [Help("The clip that must be played")]
        public string stateName;
        ///<value>Period of time to fade this animation in and fade other animations out.</value>
        [InParam("crossFadeTime", DefaultValue = 0.25f)]
        [Help("Period of time to fade this animation in and fade other animations out")]
        public float crossFadeTime = 0.25f;

        private float elapsedTime;
        bool isTriggerSet = false;
        /// <summary>Method of Update of PlayAnimation.</summary>
        /// <remarks>Increase the elapsed time and check if the animation is over.</remarks>
        public override TaskStatus OnUpdate()
        {
            if (!isTriggerSet)
            {
                isTriggerSet = true;
                animator.SetTrigger(stateName);
            }
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= crossFadeTime)
            {
                isTriggerSet = false;
                return TaskStatus.COMPLETED;
            }
            return TaskStatus.RUNNING;
        }
    }

}