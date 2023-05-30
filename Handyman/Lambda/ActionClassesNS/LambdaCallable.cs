using AuditDAONS;
using ExceptionNS;
using PipelineExecutorNS;
using Serilog;

public class LambdaCallable
{
    private readonly LContext lContext;
    private readonly CountdownEvent countDownLatch;

    public LambdaCallable(LContext lContext)
    {
        this.lContext = lContext;
    }

    public LambdaCallable(LContext lContext, CountdownEvent countDownLatch)
    {
        this.lContext = lContext;
        this.countDownLatch = countDownLatch;
    }

    public PipelineExecutionAudit Call()
    {
        try
        {
            return LambdaEngine.Start(lContext);
        }
        catch (Exception e)
        {
            Log.Debug(e, e.Message);
            throw new HandymanException("Lambda call Failed to execute child", e);
        }
        finally
        {
            countDownLatch?.Signal();
        }
    }
}
