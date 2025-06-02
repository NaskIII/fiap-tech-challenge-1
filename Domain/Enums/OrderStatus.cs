using System.ComponentModel;

namespace Domain.Enums
{
    public enum OrderStatus
    {
        [Description("Recebido")]
        Received,

        [Description("Em preparação")]
        InPreparation,

        [Description("Pronto")]
        Ready,

        [Description("Finalizado")]
        Completed
    }
}
