namespace SOSInventory.Models
{
    public class Item
    {
        public int ItemId { get; set; }

        public string? ItemName { get; set; }
    }

    public class ItemReceipt
    {
        public int ItemReceiptId { get; set; }

        public string? ReceiptReferenceNumber { get; set; }


        public int ItemId { get; set; }

        public int QuantityReceived { get; set; }
    }

    public class Shipment
    {
        public int ShipmentId { get; set; }

        public string? ShipmentRefNumber { get; set; }

        public int ItemId { get; set; }

        public int QuantityShipped { get; set; }
    }

    public class Adjustment
    {
        public int AdjustmentId { get; set; }

        public string? AdjustmentReferenceNumber { get; set; }

        public int ItemId { get; set; }

        public int QuantityAdjusted { get; set; }
    }
}
