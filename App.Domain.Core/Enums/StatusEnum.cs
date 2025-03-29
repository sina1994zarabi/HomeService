using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Enums
{
    public enum StatusEnum
    {
        AwaitingOffers = 1,           // Customer submitted request, waiting for expert offers
        PendingClientConfirmation = 2, // Offers received, customer needs to confirm one
        PendingProviderConfirmation = 3, // Customer confirmed, expert needs to confirm
        InProgress = 4,               // Both confirmed, service is being performed
        Completed = 5,                // Service is done
        AwaitingPayment = 6,          // Service completed, payment pending
        AwaitingAdminApprovalForTransaction = 7, // Withrawed From Client Account Into Admin Account
        Paid = 8,                     // Payment received
        Cancelled = 9                 // Request or service cancelled   
    }
}
