﻿using SistemaCompra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Persistence.Contratos
{
    public interface ISolicitacaoPersist
    {
        Task<Solicitacao[]> GetAllSolicitacaoAsync();
        Task<Solicitacao> GetAllSolicitacaoByIdAsync(int id);
        Task<SolicitacaoProduto> GetAllSolicitacaoProdByIdAsync(int id);
        Task<Solicitacao[]> GetSolicitacaoByDataSolicitacaoAsync(string Data);
        Task<Solicitacao[]> GetSolicitacaoByPendenteAsync();


    }
}
