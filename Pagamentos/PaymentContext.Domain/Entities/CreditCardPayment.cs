﻿using Pagamentos.PaymentContext.Domain.ValueObjects;

namespace Pagamentos.PaymentContext.Domain.Entities;

public class CreditCardPayment : Payment
{
    public CreditCardPayment(string carHolderName, string carNumber, string lastTransactionNumber, DateTime paidDate,
        DateTime expireDate,
        decimal total,
        decimal totalPaid,
        string payer,
        Document document,
        Address address,
        Email email) : base(
            paidDate, 
            expireDate, 
            total, 
            totalPaid, 
            payer, 
            document, 
            address, 
            email)
    {
        CarHolderName = carHolderName;
        CarNumber = carNumber;
        LastTransactionNumber = lastTransactionNumber;
    }

    public string CarHolderName { get; private set; }
    public string CarNumber { get; private set; }
    public string LastTransactionNumber { get; private set; }
}