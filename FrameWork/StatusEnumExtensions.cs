using App.Domain.Core.Enums;

namespace FrameWork
{
    public static class StatusEnumExtensions
    {
        private static readonly Dictionary<StatusEnum, string> FarsiEqui = new Dictionary<StatusEnum, string>
        {
            { StatusEnum.AwaitingOffers, "در انتظار دریافت پیشنهاد" },
            { StatusEnum.PendingClientConfirmation, "منتظر تایید مشتری" },
            { StatusEnum.PendingProviderConfirmation, "منتظر تایید کارشناس" },
            { StatusEnum.Completed, "انجام شده" },
            { StatusEnum.Cancelled, "لغو شده" },
            { StatusEnum.AwaitingPayment, "پرداخت نشده" },
            { StatusEnum.AwaitingAdminApprovalForTransaction, "در انتظار تایید ادمین برای پرداخت" },
            { StatusEnum.Paid, "پرداخت شده" },
            { StatusEnum.InProgress, "در حال انجام" }
        };

        public static string ToFarsiStatus(this StatusEnum status)
        {
            return FarsiEqui.TryGetValue(status, out var farsiEqui) ? farsiEqui : "نامشخص";
        }
    }
}