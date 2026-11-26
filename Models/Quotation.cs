using System.ComponentModel.DataAnnotations;

namespace AgriConnectApi.Entities;

        public class Quotation
    {
        [Key]
        public int QuotationID { get; set; }

        [Required]
        public int RequestID { get; set; }

        [Required]
        public int SupplierID { get; set; }

        [Required]
        public decimal PricePerUnit { get; set; }

        [Required]
        public int StockQuantity { get; set; }
        

        [Required]
        public DateTime EstimatedDate { get; set; }

        [Required]
        [MaxLength(20)]
        public string VerificationStatus { get; set; } = string.Empty;
        // Pending, Approved, Rejected
    }