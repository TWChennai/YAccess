using System;

namespace AccessLog.Domain
{
    public class Transaction
    {
        public string ControllerId { get; set; }

        public string GateNumber { get; set; }

        public string EmployeeId { get; set; }

        public string CardId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}