using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etiqueta.NET.Core
{
    public class Tag
    {
        public int Contrato { get; set; }
        public string Servico { get; set; }
        public int NF { get; set; }
        public string Pedido { get; set; }
        public string Volume { get; set; }//nao aplicado
        public int Peso { get; set; }
        public string Rastreamento { get; set; }

        public string Destinatario { get; set; }
        public string EnderecoDest { get; set; }
        public int NumeroDest { get; set; }
        public string ComplementoDest { get; set; }
        public string BairroDest { get; set; }
        public string CEPDest { get; set; }
        public string CidadeDest { get; set; }
        public string UFDest { get; set; }

        public string Remetente { get; set; }
        public string EndrerecoRemet { get; set; }
        public int NumeroRemet { get; set; }
        public string ComplementoRemet { get; set; }
        public string BairroRemet { get; set; }
        public string CEPRemet { get; set; }
        public string CidadeRemet { get; set; }
        public string UFRemet { get; set; }
    }
}
