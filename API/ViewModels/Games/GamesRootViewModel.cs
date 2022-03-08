using System.Collections.Generic;

namespace API.ViewModels.Games
{
    public class GamesRootViewModel
    {
        public List<GameViewModel> Data { get; set; }
        public PaginationViewModel Pagination { get; set; }
    }
}