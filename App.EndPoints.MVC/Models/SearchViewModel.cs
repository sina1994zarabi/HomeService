using App.Domain.Core.Entities.BaseEntity;
using App.Domain.Core.Entities.Services;

namespace App.EndPoints.MVC.Models
{
    public class SearchViewModel
    {
        public string SearchTerm { get; set; }
        public int SelectedCityId { get; set; }
        public int SelectedCategoryId { get; set; }
        public List<City> Cities { get; set; }
        public List<Category> Categories { get; set; }
    }
}
