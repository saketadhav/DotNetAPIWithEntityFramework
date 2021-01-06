﻿using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Fund : AuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
