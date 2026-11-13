using System.ComponentModel.DataAnnotations;

namespace Orders.Api.models.dtos
{
    public sealed class CreateOrderDto
    {
        [StringLength(20)]
        public required string OrderNumber { get; set; }

        [Range(1, int.MaxValue)]
        public int CustomerId { get; set; }

        public int? VehicleId { get; set; }

        [StringLength(80)]
        public required string OriginCity { get; set; }

        [StringLength(80)]
        public required string DestinationCity { get; set; }

        [StringLength(200)]
        public required string CargoDescription { get; set; }

        [Range(typeof(decimal), "0.01", "99999999.99")]
        public decimal WeightKg { get; set; }

        [Range(typeof(decimal), "0", "9999999999.99")]
        public decimal FreightAmount { get; set; }

        [RegularExpression("^(MXN|USD)$")]
        public string CurrencyCode { get; set; } = "MXN";

        [RegularExpression("^(Created|Assigned|InTransit|Delivered|Cancelled)$")]
        public required string Status { get; set; }

        public DateTime PickupDate { get; set; }
        public DateTime? DeliveryDate { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }
    }
}
