public interface IState
{
    /// <summary>
   	/// Enter is called on enter of state
	/// </summary>
    void Enter();

	/// <summary>
	/// Execute is called once per frame
	/// </summary>
	void Execute();

    /// <summary>
    /// Exit is called on exit of state
	/// </summary>
	void Exit();
}
