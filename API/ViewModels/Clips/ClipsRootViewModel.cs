using System.Collections.Generic;

namespace API.ViewModels.Clips
{
    public class ClipsRootViewModel
    {
        public List<ClipsViewModel> data { get; set; }
        public PaginationViewModel pagination { get; set; }
    }
}