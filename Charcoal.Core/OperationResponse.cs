namespace Charcoal.Core
{
	public class OperationResponse
	{
	    public bool HasSucceeded { get; private set; }
	    public string Message { get; private set; }

	    public OperationResponse(bool hasSucceeded, string message="")
	    {
	        HasSucceeded = hasSucceeded;
	        Message = message;
	    }
	}
}
