namespace Telecom.Domain.Interfaces
{
    public interface IPaymentService
    {
        Task<Tuple<bool, string, string>> Charge(long amount);
    }
}
