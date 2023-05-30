using Akka.Actor;
using AuditDAONS;
using RepoConfigNS;

namespace ActorOperationNS
{

    public static class HandymanActorSystemAccess
    {
        static ActorSystem system = ActorSystem.Create("HandymanActorSystemAccess");
        static IActorRef auditInsertActor = system.ActorOf<AuditInsertActor>("AuditInsertActor");

        static IActorRef auditUpdateActor = system.ActorOf<AuditUpdateActor>("AuditUpdateActor");

        public static void Insert(PipelineExecutionAudit pipelineExecutionAudit)
        {
            auditInsertActor.Tell(pipelineExecutionAudit);
        }

        public static void Insert(ActionExecutionAudit actionExecutionAudit)
        {
            auditInsertActor.Tell(actionExecutionAudit);
        }

        public static void Insert(StatementExecutionAudit statementExecutionAudit)
        {
            auditInsertActor.Tell(statementExecutionAudit);
        }

        public static void Insert(PipelineExecutionStatusAudit pipelineExecutionStatusAudit)
        {
            auditInsertActor.Tell(pipelineExecutionStatusAudit);
        }

        public static void Insert(ActionExecutionStatusAudit actionExecutionStatusAudit)
        {
            auditInsertActor.Tell(actionExecutionStatusAudit);
        }

        public static void Update(PipelineExecutionAudit pipelineExecutionAudit)
        {
            auditUpdateActor.Tell(pipelineExecutionAudit);
        }

        public static void Update(ActionExecutionAudit actionExecutionAudit)
        {
            auditUpdateActor.Tell(actionExecutionAudit);
        }

    }

    public class AuditUpdateActor : ReceiveActor
    {
        private readonly HandymanRepo _repo = new HandymanRepoImpl();

        public AuditUpdateActor()
        {
            Receive<PipelineExecutionAudit>(audit =>
            {
                _repo.Update(audit);
            });

            Receive<ActionExecutionAudit>(audit =>
            {
                _repo.Update(audit);
            });
        }
    }

    public class AuditInsertActor : ReceiveActor
    {
        private readonly HandymanRepo _repo = new HandymanRepoImpl();

        public AuditInsertActor()
        {
            Receive<PipelineExecutionAudit>(audit =>
            {
                _repo.InsertPipeline(audit);
            });

            Receive<ActionExecutionAudit>(audit =>
            {
                _repo.InsertAction(audit);
            });

            Receive<StatementExecutionAudit>(audit =>
            {
                _repo.InsertStatement(audit);
            });

            Receive<PipelineExecutionStatusAudit>(audit =>
            {
                _repo.Save(audit);
            });

            Receive<ActionExecutionStatusAudit>(audit =>
            {
                _repo.Save(audit);
            });
        }
    }

}