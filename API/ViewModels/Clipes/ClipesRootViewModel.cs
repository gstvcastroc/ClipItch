using System.Collections.Generic;

namespace ClipItch.API.ViewModels
{
    public class ClipesRootViewModel
    {
        public List<ClipesViewModel> data { get; set; }
        public PaginationViewModel pagination { get; set; }
    }
}