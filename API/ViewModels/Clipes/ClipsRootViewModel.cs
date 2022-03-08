using System.Collections.Generic;

namespace API.ViewModels
{
    public class ClipsRootViewModel
    {
        public List<ClipsViewModel> Data { get; set; }
        public PaginationViewModel Pagination { get; set; }
    }
}