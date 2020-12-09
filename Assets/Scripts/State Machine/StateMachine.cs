namespace redd096
{
    using UnityEngine;

    public class StateMachine : MonoBehaviour
    {
        protected State state;

        /// <summary>
        /// Call it to change state
        /// </summary>
        public void SetState(State stateToSet)
        {
            State previousState = state;

            //set new one
            state = stateToSet;

            //exit from previous
            if (previousState != null)
                previousState.Exit();

            //enter in new one
            if (state != null)
            {
                state.AwakeState(this);
                state.Enter();
            }
        }

        protected virtual void Update()
        {
            //state execution
            state?.Execution();
        }
    }
}