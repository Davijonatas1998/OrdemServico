using OrdemServico.Models.ServicoOrdem;

namespace OrdemServico.Repository.Interfaces
{
    public interface IPayment
    {
        public Task<PaymentSystem> GetPayment(string Id, string Metadata);

        public Task<PaymentSystem> CreatePayment(string Metadata, string id);

        public Task<PaymentSystem> UpdatePayment(PaymentSystem payment, string Metadata, string id);

        public Task<PaymentSystem> DeletePayment(string Id, string Metadata);
    }
}
