﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MedCore.Domain.Entities.system
{
    public sealed class Roles : Base.BaseEntity<int>
    {

        [Column("RoleID")]
        [Key]

        public override int Id { get; set; }
        public string RoleName { get; set; }
    }
}