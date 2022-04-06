using System.Collections.Generic;
using FlightManagement.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlightManagement.ViewModels
{
    public class FlightMV
    {
        public Flight Flight { get; set; }
        public IEnumerable<SelectListItem> PlaneList { get; set; }
    }
}