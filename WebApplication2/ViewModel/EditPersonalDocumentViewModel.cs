﻿using System.Text.Json.Serialization;
using WebApplication2.Models;

namespace WebApplication2.ViewModel
{
    public class EditPersonalDocumentViewModel
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PlacementDocument PlacementDocument { get; set; }

        public string Series { get; set; }

        public string Number { get; set; }

        public DateTime? DateOfIssue { get; set; }

        public string Issue { get; set; }

        public DateTime? ValidityTime { get; set; }

    }
}
