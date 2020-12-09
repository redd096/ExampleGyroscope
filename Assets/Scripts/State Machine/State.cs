namespace redd096
{
    public abstract class State
    {
        protected StateMachine stateMachine;

        public State(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        /// <summary>
        /// Function called when enter in this state (before of Enter). You can use it instead of constructor for example when use serialized states
        /// </summary>
        public virtual void AwakeState(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        /// <summary>
        /// Function called when enter in this state
        /// </summary>
        public virtual void Enter()
        {

        }

        /// <summary>
        /// Function to call in Update()
        /// </summary>
        public virtual void Execution()
        {

        }

        /// <summary>
        /// Function called when exit from this state
        /// </summary>
        public virtual void Exit()
        {

        }
    }
}