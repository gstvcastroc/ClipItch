using System.Collections.Generic;

namespace API.ViewModels.Games
{
    public class GamesRootViewModel
    {
        public List<GameViewModel> data { get; set; }
        public PaginationViewModel pagination { get; set; }
    }
}