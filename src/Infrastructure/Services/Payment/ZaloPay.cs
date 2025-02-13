using Application.Interface;

namespace Infrastructure.Services.Payment
{
    public class ZaloPay : IPayment
    {
        public Task PayAsync()
        {
            return Task.CompletedTask;
        }
    }
}