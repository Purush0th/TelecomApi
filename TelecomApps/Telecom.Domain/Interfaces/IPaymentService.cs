namespace Telecom.Domain.Interfaces
{
    public interface IPaymentService
    {
        Task<Tuple<bool, string, string, long>> Charge(long amount);
    }
}
