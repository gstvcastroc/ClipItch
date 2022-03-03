using System.Collections.Generic;

namespace ClipItch.API.ViewModels.Games
{
    public class GamesRootViewModel
    {
        public List<GameViewModel> data { get; set; }
        public PaginationViewModel pagination { get; set; }
    }
}