using System;
using System.ComponentModel;
using System.Reflection;

namespace Persistence.Enums
{
    public static class Enumerations
    {
        public enum StatusOfRequest 
        {
            [Description("Pending")]
            Pending = 0,
            [Description("Processed")]
            Processed = 1,
            [Description("Closed")]
            Closed = 2 
        };
        public enum RequestReason {
            [Description("")]
            ValidRequest = 0,
            [Description("Insufficient funds")]
            InsufficientFunds = 1,
            [Description("Payment request processed, cannot cancel")]
            InvalidCancelRequestProcessed = 2,
            [Description("Payment request closed, cannot cancel")]
            InvalidCancelRequestClosed = 3,
            [Description("Payment request processed, cannot process")]
            InvalidProcessRequestProcessed = 4,
            [Description("Payment request closed, cannot process")]
            InvalidProcessRequestClosed = 5

        };

        public static string GetDescription(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null) return null;
            var attribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute.Description;
        }
    }
}
