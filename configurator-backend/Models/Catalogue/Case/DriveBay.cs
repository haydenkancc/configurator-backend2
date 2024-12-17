﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Case
{
    public class DriveBayDbo
    {
        [Required]
        public required int FormFactorID { get; set; }
        [Required]
        public required int DriveCount { get; set; }
    }

    [PrimaryKey(nameof(StorageAreaID), nameof(FormFactorID))]
    public class DriveBay
    {
        public int StorageAreaID { get; set; }
        public required int FormFactorID { get; set; }
        public required int DriveCount { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(StorageAreaID))]
        public StorageArea StorageArea { get; set; } = null!;
        public Storage.FormFactor FormFactor { get; set; } = null!;
    }
}