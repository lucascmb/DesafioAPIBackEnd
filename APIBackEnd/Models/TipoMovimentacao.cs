using System.ComponentModel;

namespace APIBackEnd.Models
{
    public enum TipoMovimentacao : byte
    {
        [Description("AP")]
        Aplicacao = 1,

        [Description("RE")]
        Resgate = 2
    }
}
