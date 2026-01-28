using Blue.Domain.Common;

namespace Blue.Domain.Subscriptions;

public class Subscription : BaseEntity<Guid>
{
    
    //{ get; private set; } Protege el estado: solo la entidad puede modificarlo
    public Guid UserId { get; private set; }
    public Subscription Plan { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public bool IsActive { get; private set; }

    private Subscription() { }

    public Subscription(Guid userId, Subscription plan, DateTime start, DateTime end)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Plan = plan;
        StartDate = start;
        EndDate = end;
        IsActive = true;
    }

    public void Cancel()
    {
        IsActive = false;
    }
}