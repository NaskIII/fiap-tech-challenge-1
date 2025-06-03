﻿using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.OrderItemDtos.Request
{
    public class OrderItemRequest
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
    }
}
