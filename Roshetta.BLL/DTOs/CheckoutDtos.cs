using System.ComponentModel.DataAnnotations;

namespace Roshetta.BLL.DTOs
{
    public class CheckoutDto
    {
        [Required(ErrorMessage = "Full Name is required.")]
        [Display(Name = "Full Name")]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone Number is required.")]
        [Display(Name = "Phone Number")]
        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Governorate is required.")]
        public string Governorate { get; set; } = "Cairo";

        [Required(ErrorMessage = "City / District is required.")]
        [Display(Name = "City / District")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Street address is required.")]
        public string Street { get; set; } = string.Empty;

        [Required(ErrorMessage = "Building / Apartment is required.")]
        [Display(Name = "Building / Apt / Floor")]
        public string Building { get; set; } = string.Empty;

        [Display(Name = "Delivery Notes (optional)")]
        public string? DeliveryNotes { get; set; }

        [Required]
        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; } = "Cash on Delivery"; // Egyptian e-pharmacy standard

        public string? PromoCode { get; set; }

        public CartDto? Cart { get; set; }
    }

    public class OrderConfirmationDto
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string DeliveryAddress { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public string EstimatedDelivery { get; set; } = string.Empty;
        public bool RequiresRxReview { get; set; }
        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
    }
}
