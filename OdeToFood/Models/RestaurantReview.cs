﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OdeToFood.Models {

    public class MaxWordsAttribute : ValidationAttribute {

        private readonly int _maxWords;

        public MaxWordsAttribute(int maxWords) : base("{0} has too many words.") {
            _maxWords = maxWords;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {

            if (value != null) {

                var valueAsString = value.ToString();

                if (valueAsString.Split(' ').Length > _maxWords) {

                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);

                    return new ValidationResult(errorMessage);

                }

            }

            return ValidationResult.Success;

        }

    }

    public class RestaurantReview {

        public int Id { get; set; }

        [Range(1, 10)]
        [Required]
        public int Rating { get; set; }

        [Required]
        [StringLength(1024)]
        public string Body { get; set; }

        public int RestaurantId { get; set; }

        [Display(Name = "User Name")]
        [DisplayFormat(NullDisplayText = "anonymous")]
        public string ReviewerName { get; set; }

    }

}