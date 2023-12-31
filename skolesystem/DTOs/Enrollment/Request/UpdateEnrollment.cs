﻿using System;
using System.ComponentModel.DataAnnotations;

namespace skolesystem.DTOs.Enrollment.Request
{
	public class UpdateEnrollment
	{
        [Required]
        [Range(1, int.MaxValue)]
        public int EnrollmentId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int ClasseId { get; set; }
    }
}

