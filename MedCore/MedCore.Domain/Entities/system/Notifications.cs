﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Identity.Client;


namespace MedCore.Domain.Entities.system
{
    public sealed class Notifications : Base.BaseEntity<int>
    {

        [Column("NotificationID")]
        [Key]
        public override int Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }


    }
}