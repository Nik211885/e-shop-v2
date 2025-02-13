using Application.Interface;

namespace Infrastructure.Services.Payment
{
    public class MomoPay : IPayment
    {
        public Task PayAsync()
        {
            return Task.CompletedTask;
        }
    }
}