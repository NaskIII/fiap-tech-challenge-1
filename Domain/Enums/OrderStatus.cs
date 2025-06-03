using System.ComponentModel;

namespace Domain.Enums
{
    public enum OrderStatus
    {
        [Description("Pendente")]
        Pending,

        [Description("Pago")]
        Paid,

        [Description("Recebido")]
        Received,

        [Description("Em preparação")]
        InPreparation,

        [Description("Pronto")]
        Ready,

        [Description("Finalizado")]
        Completed,

        [Description("Cancelado")]
        Canceled,
    }
}
