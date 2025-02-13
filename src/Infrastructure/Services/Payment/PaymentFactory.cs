using Application.Interface;

namespace Infrastructure.Services.Payment
{
    public class PaymentFactory : IFactoryMethod<IPayment>
    {
        public IPayment Create(string key)
        {
            return key switch
            {
                "Zalo" => new ZaloPay(),
                "Momo" => new MomoPay(),
                _ => throw new Exception($"Not support method {key} payment")
            };
        }
    }
}