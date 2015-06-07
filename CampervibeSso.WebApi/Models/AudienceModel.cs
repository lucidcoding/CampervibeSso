﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CampervibeSso.WebApi.Models
{
    public class AudienceModel
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
    }
}