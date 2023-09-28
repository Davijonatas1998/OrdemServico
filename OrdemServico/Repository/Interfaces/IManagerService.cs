using OrdemServico.Models.ServicoOrdem;

namespace OrdemServico.Repository.Interfaces
{
    public interface IManagerService
    {
        public IQueryable<ClassOrdemServico> QuerybleServico();

        public Task<List<ClassOrdemServico>> ListServico();

        public Task<ClassOrdemServico> CreateServico(ClassOrdemServico Servico);

        public Task<ClassOrdemServico> UpdateServico(ClassOrdemServico Servico);

        public Task<ClassOrdemServico> GetServico(int? id);

        public Task<ClassOrdemServico> DetailsServico(int? id);

        public Task<ClassOrdemServico> DeleteServico(int? id);
    }
}
